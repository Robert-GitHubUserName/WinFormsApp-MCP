using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp_MCP
{
    public partial class SettingsForm : Form
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private bool _initialLoad = true;

        // Properties to access the form's settings
        public LlmSettings.Provider Provider { get; private set; } = LlmSettings.Provider.OpenAI;
        public string OpenAiApiKey { get; private set; } = string.Empty;
        public string OpenAiModelId { get; private set; } = "gpt-4o-mini";
        public string OpenRouterApiKey { get; private set; } = string.Empty;
        public string OpenRouterModelId { get; private set; } = "moonshotai/moonlight-16b-a3b-instruct:free";
        public string OllamaEndpoint { get; private set; } = "http://localhost:11434";
        public string OllamaModelId { get; private set; } = "llama3.2:3b";
        public string GitLabPersonalAccessToken { get; private set; } = string.Empty;
        public string GitLabApiUrl { get; private set; } = "https://gitlab.com/api/v4";
        public string[] FilesystemDirectories { get; private set; } = Array.Empty<string>();

        public SettingsForm()
        {
            InitializeComponent();

            // Load existing settings
            var settings = LlmSettings.Load();

            Provider = settings.LlmProvider;
            OpenAiApiKey = settings.OpenAiApiKey;
            OpenAiModelId = settings.OpenAiModelId;
            OpenRouterApiKey = settings.OpenRouterApiKey;
            OpenRouterModelId = settings.OpenRouterModelId;
            OllamaEndpoint = settings.OllamaEndpoint;
            OllamaModelId = settings.OllamaModelId;
            GitLabPersonalAccessToken = settings.GitLabPersonalAccessToken;
            GitLabApiUrl = settings.GitLabApiUrl;
            FilesystemDirectories = settings.FilesystemDirectories;

            // Populate the model dropdowns
            PopulateModelDropdowns();

            // Set the text boxes based on current settings
            textBoxOpenAIKey.Text = OpenAiApiKey;
            comboBoxOpenAIModel.SelectedItem = OpenAiModelId;
            textBoxOpenRouterKey.Text = OpenRouterApiKey;
            comboBoxOpenRouterModel.SelectedValue = OpenRouterModelId;
            textBoxOllamaEndpoint.Text = OllamaEndpoint;
            comboBoxOllamaModel.Text = OllamaModelId;
            textBoxGitLabToken.Text = GitLabPersonalAccessToken;
            textBoxGitLabApiUrl.Text = GitLabApiUrl;
            textBoxFilesystemDirectories.Text = string.Join(Environment.NewLine, FilesystemDirectories);

            // Set the selected provider
            radioButtonOpenAI.Checked = Provider == LlmSettings.Provider.OpenAI;
            radioButtonOpenRouter.Checked = Provider == LlmSettings.Provider.OpenRouter;
            radioButtonOllama.Checked = Provider == LlmSettings.Provider.Ollama;
        }

        private void PopulateModelDropdowns()
        {
            // Populate OpenAI models
            comboBoxOpenAIModel.Items.AddRange(new string[] { "gpt-4o-mini", "gpt-4o", "gpt-3.5-turbo", "gpt-3" });
            comboBoxOpenAIModel.SelectedItem = OpenAiModelId;

            // Populate Ollama models with some common options
            comboBoxOllamaModel.Items.AddRange(new string[] { "llama3.2:3b", "llama3:8b", "llama3:70b", "mistral", "codellama", "phi3", "phi3:14b" });
            
            if (!string.IsNullOrEmpty(OllamaModelId))
            {
                if (comboBoxOllamaModel.Items.Contains(OllamaModelId))
                {
                    comboBoxOllamaModel.SelectedItem = OllamaModelId;
                }
                else
                {
                    comboBoxOllamaModel.Text = OllamaModelId;
                }
            }
            else
            {
                comboBoxOllamaModel.SelectedIndex = 0;
            }

            // Don't populate OpenRouter models since it's not functional
            // PopulateOpenRouterModels();
        }

        private void PopulateOpenRouterModels()
        {
            try
            {
                // Correct OpenRouter API endpoint
                string apiUrl = "https://openrouter.ai/api/v1/models";
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {OpenRouterApiKey}");

                var response = httpClient.GetAsync(apiUrl).Result;
                response.EnsureSuccessStatusCode();

                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                // Log the response for debugging
                Console.WriteLine("OpenRouter API Response: " + jsonResponse);

                comboBoxOpenRouterModel.Items.Clear();

                // Create a list for the ComboBox DataSource
                List<Model> modelsList = new List<Model>();

                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    var models = doc.RootElement.GetProperty("data").EnumerateArray()
                        .Select(model => new Model
                        {
                            Id = model.GetProperty("id").GetString(),
                            Name = model.GetProperty("name").GetString()
                        })
                        .ToList();

                    var freeModels = models.Where(model => model.Name.Contains("(free)")).ToList();
                    var paidModels = models.Where(model => !model.Name.Contains("(free)")).ToList();
                    modelsList = freeModels.Concat(paidModels).ToList();
                }

                // Set properties before setting DataSource
                comboBoxOpenRouterModel.DisplayMember = "Name";
                comboBoxOpenRouterModel.ValueMember = "Id";
                comboBoxOpenRouterModel.DataSource = modelsList;

                // Now find and select the model that matches OpenRouterModelId
                for (int i = 0; i < modelsList.Count; i++)
                {
                    if (modelsList[i].Id == OpenRouterModelId)
                    {
                        comboBoxOpenRouterModel.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load OpenRouter models: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FetchOllamaModels()
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxOllamaEndpoint.Text))
                {
                    return;
                }

                string apiUrl = $"{textBoxOllamaEndpoint.Text.TrimEnd('/')}/api/tags";
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    // Check if the "models" property exists
                    if (doc.RootElement.TryGetProperty("models", out var modelsElement))
                    {
                        var currentSelection = comboBoxOllamaModel.Text;
                        comboBoxOllamaModel.Items.Clear();
                        
                        var models = modelsElement.EnumerateArray()
                            .Select(model => model.GetProperty("name").GetString())
                            .Where(name => !string.IsNullOrEmpty(name))
                            .ToList();

                        comboBoxOllamaModel.Items.AddRange(models.ToArray());
                        
                        // Restore selection if possible
                        if (!string.IsNullOrEmpty(currentSelection))
                        {
                            if (comboBoxOllamaModel.Items.Contains(currentSelection))
                            {
                                comboBoxOllamaModel.SelectedItem = currentSelection;
                            }
                            else
                            {
                                comboBoxOllamaModel.Text = currentSelection;
                            }
                        }
                        else if (comboBoxOllamaModel.Items.Count > 0)
                        {
                            comboBoxOllamaModel.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Don't show an error message, just use the default models
                Console.WriteLine($"Failed to load Ollama models: {ex.Message}");
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Set the radio buttons based on the provider
            switch (Provider)
            {
                case LlmSettings.Provider.OpenAI:
                    radioButtonOpenAI.Checked = true;
                    break;
                case LlmSettings.Provider.OpenRouter:
                    radioButtonOpenRouter.Checked = true;
                    break;
                case LlmSettings.Provider.Ollama:
                    radioButtonOllama.Checked = true;
                    break;
            }

            // Set the text boxes based on current settings
            textBoxOpenAIKey.Text = OpenAiApiKey;
            comboBoxOpenAIModel.SelectedItem = OpenAiModelId;
            textBoxOpenRouterKey.Text = OpenRouterApiKey;
            comboBoxOpenRouterModel.SelectedValue = OpenRouterModelId;
            textBoxOllamaEndpoint.Text = OllamaEndpoint;
            comboBoxOllamaModel.Text = OllamaModelId;
            textBoxGitLabToken.Text = GitLabPersonalAccessToken;
            textBoxGitLabApiUrl.Text = GitLabApiUrl;
            textBoxFilesystemDirectories.Text = string.Join(Environment.NewLine, FilesystemDirectories);

            // Update enabled controls based on selection
            UpdateControlStates();

            // Try to fetch Ollama models if Ollama is selected
            if (radioButtonOllama.Checked)
            {
                FetchOllamaModels();
            }

            _initialLoad = false;
        }

        private void UpdateControlStates()
        {
            // Enable/disable the appropriate controls based on the selected provider
            groupBoxOpenAI.Enabled = radioButtonOpenAI.Checked;
            groupBoxOpenRouter.Enabled = radioButtonOpenRouter.Checked;
            groupBoxOllama.Enabled = radioButtonOllama.Checked;
        }

        private void radioButtonOpenAI_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControlStates();
        }

        private void radioButtonOpenRouter_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOpenRouter.Checked && !_initialLoad)
            {
                MessageBox.Show("OpenRouter integration is not functional yet.", "Feature Unavailable",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Switch back to OpenAI
                radioButtonOpenAI.Checked = true;
                radioButtonOpenRouter.Checked = false;
            }

            UpdateControlStates();
        }

        private void radioButtonOllama_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControlStates();
            if (radioButtonOllama.Checked && !_initialLoad)
            {
                FetchOllamaModels();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Validate the selected provider's settings
            if (radioButtonOpenAI.Checked && string.IsNullOrWhiteSpace(textBoxOpenAIKey.Text))
            {
                MessageBox.Show("Please enter an OpenAI API key.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxOpenAIKey.Focus();
                return;
            }

            if (radioButtonOllama.Checked && string.IsNullOrWhiteSpace(textBoxOllamaEndpoint.Text))
            {
                MessageBox.Show("Please enter an Ollama endpoint URL.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxOllamaEndpoint.Focus();
                return;
            }

            if (radioButtonOllama.Checked && string.IsNullOrWhiteSpace(comboBoxOllamaModel.Text))
            {
                MessageBox.Show("Please enter an Ollama model name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxOllamaModel.Focus();
                return;
            }

            // Save the settings
            if (radioButtonOpenAI.Checked)
                Provider = LlmSettings.Provider.OpenAI;
            else if (radioButtonOpenRouter.Checked)
                Provider = LlmSettings.Provider.OpenRouter;
            else if (radioButtonOllama.Checked)
                Provider = LlmSettings.Provider.Ollama;

            OpenAiApiKey = textBoxOpenAIKey.Text;
            OpenAiModelId = comboBoxOpenAIModel.SelectedItem?.ToString() ?? "gpt-4o-mini";
            OpenRouterApiKey = textBoxOpenRouterKey.Text;
            OllamaEndpoint = textBoxOllamaEndpoint.Text;
            OllamaModelId = comboBoxOllamaModel.Text;
            GitLabPersonalAccessToken = textBoxGitLabToken.Text;
            GitLabApiUrl = textBoxGitLabApiUrl.Text;

            if (string.IsNullOrWhiteSpace(GitLabApiUrl))
            {
                GitLabApiUrl = "https://gitlab.com/api/v4";
            }

            // Process filesystem directories
            FilesystemDirectories = textBoxFilesystemDirectories.Text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(dir => dir.Trim())
                .Where(dir => !string.IsNullOrWhiteSpace(dir))
                .ToArray();

            // If no OpenRouter model is selected, use the default
            if (comboBoxOpenRouterModel.SelectedValue == null)
            {
                OpenRouterModelId = "moonshotai/moonlight-16b-a3b-instruct:free";
            }
            else
            {
                OpenRouterModelId = comboBoxOpenRouterModel.SelectedValue.ToString();
            }

            // Save settings to file
            var settings = new LlmSettings
            {
                LlmProvider = Provider,
                OpenAiApiKey = OpenAiApiKey,
                OpenAiModelId = OpenAiModelId,
                OpenRouterApiKey = OpenRouterApiKey,
                OpenRouterModelId = OpenRouterModelId,
                OllamaEndpoint = OllamaEndpoint,
                OllamaModelId = OllamaModelId,
                GitLabPersonalAccessToken = GitLabPersonalAccessToken,
                GitLabApiUrl = GitLabApiUrl,
                FilesystemDirectories = FilesystemDirectories
            };
            settings.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    public class OpenRouterModelsResponse
    {
        public List<Model> Data { get; set; }
    }

    public class Model
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
