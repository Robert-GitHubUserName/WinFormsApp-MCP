using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace WinFormsApp_MCP
{
    /// <summary>
    /// Stores settings for the LLM providers
    /// </summary>
    public class LlmSettings
    {
        public enum Provider
        {
            OpenAI,
            OpenRouter
        }

        public Provider LlmProvider { get; set; } = Provider.OpenAI;
        public string OpenAiApiKey { get; set; } = string.Empty;
        public string OpenAiModelId { get; set; } = "gpt-4o-mini";
        public string OpenRouterApiKey { get; set; } = string.Empty;
        public string OpenRouterModelId { get; set; } = "moonshotai/moonlight-16b-a3b-instruct:free";
        public string[] FilesystemDirectories { get; set; } = Array.Empty<string>();
        public string GitLabPersonalAccessToken { get; set; } = string.Empty;
        public string GitLabApiUrl { get; set; } = "https://gitlab.com/api/v4";

        /// <summary>
        /// Indicates whether valid filesystem directories have been configured
        /// </summary>
        public bool HasConfiguredDirectories =>
            FilesystemDirectories != null &&
            FilesystemDirectories.Length > 0;

        private static readonly string SettingsFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "settings.json");

        /// <summary>
        /// Creates a new settings object with values from environment variables or defaults
        /// </summary>
        public static LlmSettings CreateDefault()
        {
            // Check for environment variables first
            string? apiKey = Environment.GetEnvironmentVariable("OpenAI__ApiKey");
            string? modelId = Environment.GetEnvironmentVariable("OpenAI__ChatModelId") ?? "gpt-4o-mini";
            string? gitlabToken = Environment.GetEnvironmentVariable("GITLAB_PERSONAL_ACCESS_TOKEN");
            string? gitlabApiUrl = Environment.GetEnvironmentVariable("GITLAB_API_URL");

            return new LlmSettings
            {
                LlmProvider = Provider.OpenAI,
                OpenAiApiKey = apiKey ?? string.Empty,
                OpenAiModelId = modelId,
                OpenRouterApiKey = string.Empty,
                OpenRouterModelId = "openai/gpt-4o-mini",
                FilesystemDirectories = new[] { Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) },
                GitLabPersonalAccessToken = gitlabToken ?? string.Empty,
                GitLabApiUrl = gitlabApiUrl ?? "https://gitlab.com/api/v4"
            };
        }

        public static LlmSettings Load()
        {
            LlmSettings settings;

            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    settings = JsonSerializer.Deserialize<LlmSettings>(json) ?? CreateDefault();
                }
                else
                {
                    settings = CreateDefault();
                }
            }
            catch (Exception)
            {
                settings = CreateDefault();
            }

            // Override with environment variables if they are set
            string? apiKey = Environment.GetEnvironmentVariable("OpenAI__ApiKey");
            if (!string.IsNullOrEmpty(apiKey))
            {
                settings.OpenAiApiKey = apiKey;
            }

            string? modelId = Environment.GetEnvironmentVariable("OpenAI__ChatModelId");
            if (!string.IsNullOrEmpty(modelId))
            {
                settings.OpenAiModelId = modelId;
            }

            string? gitlabToken = Environment.GetEnvironmentVariable("GITLAB_PERSONAL_ACCESS_TOKEN");
            if (!string.IsNullOrEmpty(gitlabToken))
            {
                settings.GitLabPersonalAccessToken = gitlabToken;
            }

            string? gitlabApiUrl = Environment.GetEnvironmentVariable("GITLAB_API_URL");
            if (!string.IsNullOrEmpty(gitlabApiUrl))
            {
                settings.GitLabApiUrl = gitlabApiUrl;
            }

            return settings;
        }

        /// <summary>
        /// Saves settings to the settings file
        /// </summary>
        public void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsFilePath, json);
            }
            catch (Exception)
            {
                // Silently fail if we can't save settings
            }
        }
    }
}
