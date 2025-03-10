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

        // Properties to access the form's settings
        public LlmSettings.Provider Provider { get; private set; } = LlmSettings.Provider.OpenAI;
        public string OpenAiApiKey { get; private set; } = string.Empty;
        public string OpenAiModelId { get; private set; } = "gpt-4o-mini";
        public string OpenRouterApiKey { get; private set; } = string.Empty;
        public string OpenRouterModelId { get; private set; } = "moonshotai/moonlight-16b-a3b-instruct:free";
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
            textBoxGitLabToken.Text = GitLabPersonalAccessToken;
            textBoxGitLabApiUrl.Text = GitLabApiUrl;
            textBoxFilesystemDirectories.Text = string.Join(Environment.NewLine, FilesystemDirectories);
        }

        private void PopulateModelDropdowns()
        {
            // Populate OpenAI models
            comboBoxOpenAIModel.Items.AddRange(new string[] { "gpt-4o-mini", "gpt-4o", "gpt-3.5-turbo", "gpt-3" });
            comboBoxOpenAIModel.SelectedItem = OpenAiModelId;

            // Populate OpenRouter models
            PopulateOpenRouterModels();
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

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // Set the radio buttons based on current provider
            radioButtonOpenAI.Checked = Provider == LlmSettings.Provider.OpenAI;
            radioButtonOpenRouter.Checked = Provider == LlmSettings.Provider.OpenRouter;

            // Set the text boxes based on current settings
            textBoxOpenAIKey.Text = OpenAiApiKey;
            comboBoxOpenAIModel.SelectedItem = OpenAiModelId;
            textBoxOpenRouterKey.Text = OpenRouterApiKey;
            comboBoxOpenRouterModel.SelectedValue = OpenRouterModelId;
            textBoxGitLabToken.Text = GitLabPersonalAccessToken;
            textBoxGitLabApiUrl.Text = GitLabApiUrl;
            textBoxFilesystemDirectories.Text = string.Join(Environment.NewLine, FilesystemDirectories);

            // Update enabled controls based on selection
            UpdateControlStates();
        }

        private void UpdateControlStates()
        {
            // Enable/disable the appropriate controls based on the selected provider
            groupBoxOpenAI.Enabled = radioButtonOpenAI.Checked;
            groupBoxOpenRouter.Enabled = radioButtonOpenRouter.Checked;
        }

        private void radioButtonOpenAI_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControlStates();
        }

        private void radioButtonOpenRouter_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControlStates();
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

            if (radioButtonOpenRouter.Checked && string.IsNullOrWhiteSpace(textBoxOpenRouterKey.Text))
            {
                MessageBox.Show("Please enter an OpenRouter API key.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxOpenRouterKey.Focus();
                return;
            }

            // Save the settings
            Provider = radioButtonOpenAI.Checked ? LlmSettings.Provider.OpenAI : LlmSettings.Provider.OpenRouter;
            OpenAiApiKey = textBoxOpenAIKey.Text;
            OpenAiModelId = comboBoxOpenAIModel.SelectedItem.ToString();
            OpenRouterApiKey = textBoxOpenRouterKey.Text;
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

            if (comboBoxOpenRouterModel.SelectedValue != null)
            {
                OpenRouterModelId = comboBoxOpenRouterModel.SelectedValue.ToString();
            }
            else
            {
                MessageBox.Show("Please select a model for OpenRouter.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save settings to file
            var settings = new LlmSettings
            {
                LlmProvider = Provider,
                OpenAiApiKey = OpenAiApiKey,
                OpenAiModelId = OpenAiModelId,
                OpenRouterApiKey = OpenRouterApiKey,
                OpenRouterModelId = OpenRouterModelId,
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
