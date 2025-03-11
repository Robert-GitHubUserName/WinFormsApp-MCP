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

            // Try to load directories from settings.json if it exists
            string[] directories = TryGetDirectoriesFromSettingsFile();

            // If no directories were found in settings.json, use default
            if (directories.Length == 0)
            {
                directories = new string[] { "D:\\Downloads" };
            }

            return new LlmSettings
            {
                LlmProvider = Provider.OpenAI,
                OpenAiApiKey = apiKey ?? string.Empty,
                OpenAiModelId = modelId,
                OpenRouterApiKey = string.Empty,
                OpenRouterModelId = "openai/gpt-4o-mini",
                FilesystemDirectories = directories,
                GitLabPersonalAccessToken = gitlabToken ?? string.Empty,
                GitLabApiUrl = gitlabApiUrl ?? "https://gitlab.com/api/v4"
            };
        }

        /// <summary>
        /// Tries to extract just the filesystem directories from settings.json without
        /// deserializing the entire settings object
        /// </summary>
        private static string[] TryGetDirectoriesFromSettingsFile()
        {
            try
            {
                if (!File.Exists(SettingsFilePath))
                    return Array.Empty<string>();

                string json = File.ReadAllText(SettingsFilePath);

                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    if (doc.RootElement.TryGetProperty("FilesystemDirectories", out JsonElement directoriesElement) &&
                        directoriesElement.ValueKind == JsonValueKind.Array)
                    {
                        return directoriesElement.EnumerateArray()
                            .Select(e => e.GetString())
                            .Where(s => !string.IsNullOrEmpty(s))
                            .ToArray();
                    }
                }
            }
            catch
            {
                // Silently fail if parsing fails
            }

            return Array.Empty<string>();
        }

        public static LlmSettings Load()
        {
            try
            {
                if (!File.Exists(SettingsFilePath))
                {
                    return CreateDefault();
                }

                string json = File.ReadAllText(SettingsFilePath);
                var settings = JsonSerializer.Deserialize<LlmSettings>(json);

                // If settings were loaded but no directories were found, use the default
                if (settings != null && (settings.FilesystemDirectories == null || settings.FilesystemDirectories.Length == 0))
                {
                    // Try to extract just the directories first
                    string[] directories = TryGetDirectoriesFromSettingsFile();

                    // If still no directories, use default
                    if (directories.Length == 0)
                    {
                        settings.FilesystemDirectories = new string[] { "D:\\Downloads" };
                    }
                    else
                    {
                        settings.FilesystemDirectories = directories;
                    }
                }

                return settings ?? CreateDefault();
            }
            catch (Exception)
            {
                // If there's any error, return default settings
                return CreateDefault();
            }
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
