using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using McpDotNet.Client;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ModelContextProtocol;

namespace WinFormsApp_MCP
{
    public partial class Form1 : Form
    {
        private KernelService _kernelService;
        private IMcpClient _githubClient;
        private IMcpClient _filesystemClient;
        private IMcpClient _gitlabClient;
        private bool _isBusy = false;
        private LlmSettings _settings;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateStatus("Initializing services...");

                // Load settings
                _settings = LlmSettings.Load();

                // Initialize KernelService
                _kernelService = new KernelService();

                // Initialize MCP Clients
                UpdateStatus("Initializing GitHub Tools...");
                _githubClient = await McpDotNetExtensions.GetGitHubToolsAsync();
                
                UpdateStatus("Initializing Filesystem Tools...");
                _filesystemClient = await McpDotNetExtensions.GetFilesystemToolsAsync(
                    _settings.FilesystemDirectories);

                // Initialize GitLab client if token is available
                if (!string.IsNullOrEmpty(_settings.GitLabPersonalAccessToken))
                {
                    UpdateStatus("Initializing GitLab Tools...");
                    try 
                    {
                        _gitlabClient = await McpDotNetExtensions.GetGitLabToolsAsync(
                            _settings.GitLabPersonalAccessToken, 
                            _settings.GitLabApiUrl);
                    }
                    catch (Exception ex)
                    {
                        string errorMessage = $"Failed to initialize GitLab tools: {ex.Message}";
                        AppendToChatHistory("System", errorMessage, Color.Red);
                        // Add a more prominent message to ensure it's seen
                        MessageBox.Show(errorMessage, "GitLab Initialization Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Set the status to indicate the issue
                        UpdateStatus("GitLab tools initialization failed");
                    }
                }
                else
                {
                    AppendToChatHistory("System", "GitLab integration is disabled. To enable it, add a GitLab Personal Access Token in Settings.", Color.DarkOrange);
                }

                // Register functions with the kernel
                await RegisterMcpFunctionsAsync();

                UpdateStatus("Ready");

                // Display welcome message
                AppendToChatHistory("System", "Welcome to the Model Context Protocol Chat.\nType a message to begin.", Color.DarkGreen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing services: {ex.Message}", "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Initialization failed");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Perform cleanup here
            _githubClient?.DisposeAsync().AsTask().Wait();
            _filesystemClient?.DisposeAsync().AsTask().Wait();
            _gitlabClient?.DisposeAsync().AsTask().Wait();
        }

        private async Task RegisterMcpFunctionsAsync()
        {
            try
            {
                UpdateStatus("Retrieving available tools...");
                int totalToolsCount = 0;

                // Map GitHub MCP tools to Kernel functions
                var githubFunctions = await _githubClient.MapToFunctionsAsync();
                var githubPlugin = KernelPluginFactory.CreateFromFunctions("GitHub", "GitHub Tools", githubFunctions);
                _kernelService.Kernel.Plugins.Add(githubPlugin);
                
                // Map Filesystem MCP tools to Kernel functions
                var filesystemFunctions = await _filesystemClient.MapToFunctionsAsync();
                var filesystemPlugin = KernelPluginFactory.CreateFromFunctions("Filesystem", "Filesystem Tools", filesystemFunctions);
                _kernelService.Kernel.Plugins.Add(filesystemPlugin);

                // List available GitHub tools
                var githubToolsResult = await _githubClient.ListToolsAsync();
                totalToolsCount += githubToolsResult.Tools.Count;
                
                // List available Filesystem tools
                var filesystemToolsResult = await _filesystemClient.ListToolsAsync();
                totalToolsCount += filesystemToolsResult.Tools.Count;

                // Display available tools
                AppendToChatHistory("System",
                    $"Available GitHub tools ({githubToolsResult.Tools.Count}):\n" +
                    string.Join("\n", githubToolsResult.Tools.Select(t => $"• {t.Name}: {t.Description}")),
                    Color.DarkBlue);
                
                AppendToChatHistory("System",
                    $"Available Filesystem tools ({filesystemToolsResult.Tools.Count}):\n" +
                    string.Join("\n", filesystemToolsResult.Tools.Select(t => $"• {t.Name}: {t.Description}")),
                    Color.DarkBlue);

                // Map GitLab MCP tools if available
                if (_gitlabClient != null)
                {
                    var gitlabFunctions = await _gitlabClient.MapToFunctionsAsync();
                    var gitlabPlugin = KernelPluginFactory.CreateFromFunctions("GitLab", "GitLab Tools", gitlabFunctions);
                    _kernelService.Kernel.Plugins.Add(gitlabPlugin);

                    // List available GitLab tools
                    var gitlabToolsResult = await _gitlabClient.ListToolsAsync();
                    totalToolsCount += gitlabToolsResult.Tools.Count;

                    AppendToChatHistory("System",
                        $"Available GitLab tools ({gitlabToolsResult.Tools.Count}):\n" +
                        string.Join("\n", gitlabToolsResult.Tools.Select(t => $"• {t.Name}: {t.Description}")),
                        Color.DarkBlue);
                }

                UpdateStatus($"Loaded {totalToolsCount} tools");
            }
            catch (Exception ex)
            {
                AppendToChatHistory("System", $"Error loading tools: {ex.Message}", Color.Red);
                UpdateStatus("Failed to load tools");
            }
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            await SendMessageAsync();
        }

        private async void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Send on Ctrl+Enter
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                e.SuppressKeyPress = true; // Prevent the beep
                await SendMessageAsync();
            }
        }

        private async Task SendMessageAsync()
        {
            string userMessage = userInputTextBox.Text.Trim();

            if (string.IsNullOrEmpty(userMessage) || _isBusy)
                return;

            try
            {
                _isBusy = true;
                UpdateStatus("Processing...");

                // Clear the input text box
                userInputTextBox.Text = string.Empty;

                // Display the user message
                AppendToChatHistory("You", userMessage, Color.Black);

                // Create OpenAI execution settings with function calling behavior
                var executionSettings = new OpenAIPromptExecutionSettings
                {
                    Temperature = 0,
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                };

                // Create arguments for the kernel
                var kernelArguments = new KernelArguments(executionSettings);

                // Get response from the AI
                var result = await _kernelService.Kernel.InvokePromptAsync(userMessage, kernelArguments);

                // Display the AI response
                AppendToChatHistory("Assistant", result.ToString(), Color.DarkBlue);
            }
            catch (Exception ex)
            {
                AppendToChatHistory("System", $"Error: {ex.Message}", Color.Red);
            }
            finally
            {
                _isBusy = false;
                UpdateStatus("Ready");
            }
        }

        private void AppendToChatHistory(string sender, string message, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AppendToChatHistory(sender, message, color)));
                return;
            }

            // Auto-scroll to show the new message
            chatHistoryRichTextBox.SelectionStart = chatHistoryRichTextBox.TextLength;
            chatHistoryRichTextBox.SelectionLength = 0;

            // Add timestamp
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            chatHistoryRichTextBox.SelectionColor = Color.Gray;
            chatHistoryRichTextBox.AppendText($"[{timestamp}] ");

            // Add sender
            chatHistoryRichTextBox.SelectionColor = color;
            chatHistoryRichTextBox.SelectionFont = new Font(chatHistoryRichTextBox.Font, FontStyle.Bold);
            chatHistoryRichTextBox.AppendText($"{sender}: ");

            // Add message
            chatHistoryRichTextBox.SelectionFont = new Font(chatHistoryRichTextBox.Font, FontStyle.Regular);
            chatHistoryRichTextBox.AppendText($"{message}");
            chatHistoryRichTextBox.AppendText(Environment.NewLine + Environment.NewLine);

            // Scroll to the end
            chatHistoryRichTextBox.SelectionStart = chatHistoryRichTextBox.Text.Length;
            chatHistoryRichTextBox.ScrollToCaret();
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatus(message)));
                return;
            }

            statusLabel.Text = message;
            Application.DoEvents();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Create and show the settings form
                var settingsForm = new SettingsForm();

                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Update the kernel service with the new settings
                    UpdateStatus("Reconfiguring services...");

                    // Get the updated settings from the form
                    var newSettings = new LlmSettings
                    {
                        LlmProvider = settingsForm.Provider,
                        OpenAiApiKey = settingsForm.OpenAiApiKey,
                        OpenAiModelId = settingsForm.OpenAiModelId,
                        OpenRouterApiKey = settingsForm.OpenRouterApiKey,
                        OpenRouterModelId = settingsForm.OpenRouterModelId,
                        OllamaEndpoint = settingsForm.OllamaEndpoint,
                        OllamaModelId = settingsForm.OllamaModelId,
                        GitLabPersonalAccessToken = settingsForm.GitLabPersonalAccessToken,
                        GitLabApiUrl = settingsForm.GitLabApiUrl,
                        FilesystemDirectories = settingsForm.FilesystemDirectories
                    };

                    // Update the kernel service
                    bool saveSuccessful = _kernelService.UpdateSettings(newSettings);
                    
                    if (!saveSuccessful)
                    {
                        AppendToChatHistory("System", "Warning: Settings were applied but could not be saved to disk. " +
                            "They will be reset when the application is restarted.", Color.Red);
                    }

                    // Update MCP clients if needed
                    bool restartNeeded = false;
                    
                    // Check if GitLab settings changed
                    if (_settings.GitLabPersonalAccessToken != newSettings.GitLabPersonalAccessToken ||
                        _settings.GitLabApiUrl != newSettings.GitLabApiUrl)
                    {
                        restartNeeded = true;
                    }
                    
                    // Check if Filesystem directories changed
                    if (!_settings.FilesystemDirectories.SequenceEqual(newSettings.FilesystemDirectories))
                    {
                        restartNeeded = true;
                    }
                    
                    // Update current settings
                    _settings = newSettings;

                    // Inform the user about LLM changes
                    string modelInfo = "";
                    
                    switch (newSettings.LlmProvider)
                    {
                        case LlmSettings.Provider.OpenAI:
                            modelInfo = $"Using model: {newSettings.OpenAiModelId}";
                            break;
                        case LlmSettings.Provider.OpenRouter:
                            modelInfo = $"Using model: {newSettings.OpenRouterModelId}";
                            break;
                        case LlmSettings.Provider.Ollama:
                            modelInfo = $"Using model: {newSettings.OllamaModelId} at {newSettings.OllamaEndpoint}";
                            break;
                    }

                    AppendToChatHistory("System", $"LLM provider changed to {newSettings.LlmProvider}. {modelInfo}", Color.DarkGreen);
                    
                    if (restartNeeded)
                    {
                        AppendToChatHistory("System", "Some MCP settings have changed. Please restart the application for the changes to take effect.", Color.DarkOrange);
                    }

                    UpdateStatus("Ready");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating settings: {ex.Message}", "Settings Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Settings update failed");
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Model Context Protocol Chat\n\n" +
                "This application demonstrates the use of the Model Context Protocol (MCP) " +
                "with Semantic Kernel to provide AI chat capabilities enhanced with tool calling.\n\n",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
