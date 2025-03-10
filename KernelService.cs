// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace WinFormsApp_MCP;

/// <summary>
/// Service responsible for managing and providing access to a Semantic Kernel instance.
/// </summary>
public class KernelService
{
    private readonly Dictionary<string, string> _config;
    private readonly IKernelBuilder _builder;
    private Kernel _kernel; // Removed the readonly modifier
    private LlmSettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="KernelService"/> class.
    /// </summary>
    public KernelService()
    {
        // Load settings
        _settings = LlmSettings.Load();

        // Create configuration dictionary
        _config = new Dictionary<string, string>();

        // Create the kernel builder using the static factory method
        _builder = Kernel.CreateBuilder();

        // Add logging
        _builder.Services.AddSingleton(NullLoggerFactory.Instance);

        // Configure the appropriate LLM provider
        ConfigureLlmProvider();

        // Build the kernel
        _kernel = _builder.Build();
    }

    /// <summary>
    /// Gets the Kernel instance.
    /// </summary>
    public Kernel Kernel => _kernel;

    /// <summary>
    /// Reconfigures the kernel with new LLM settings
    /// </summary>
    public void UpdateSettings(LlmSettings newSettings)
    {
        _settings = newSettings;
        _settings.Save();

        // Create a new builder with the updated settings
        var builder = Kernel.CreateBuilder();
        builder.Services.AddSingleton(NullLoggerFactory.Instance);

        // Configure the LLM provider on the new builder
        ConfigureLlmProviderOnBuilder(builder, newSettings);

        // Build the new kernel and update the reference
        _kernel = builder.Build();
    }

    /// <summary>
    /// Configure the appropriate LLM provider based on settings
    /// </summary>
    private void ConfigureLlmProvider()
    {
        ConfigureLlmProviderOnBuilder(_builder, _settings);
    }

    /// <summary>
    /// Configure the appropriate LLM provider on the specified builder
    /// </summary>
    private void ConfigureLlmProviderOnBuilder(IKernelBuilder builder, LlmSettings settings)
    {
        if (settings.LlmProvider == LlmSettings.Provider.OpenAI)
        {
            ConfigureOpenAI(builder, settings);
        }
        else if (settings.LlmProvider == LlmSettings.Provider.OpenRouter)
        {
            ConfigureOpenRouter(builder, settings);
        }
        else
        {
            // Default to OpenAI if provider is unknown
            ConfigureOpenAI(builder, settings);
        }
    }

    /// <summary>
    /// Configure OpenAI as the LLM provider
    /// </summary>
    private void ConfigureOpenAI(IKernelBuilder builder, LlmSettings settings)
    {
        if (string.IsNullOrEmpty(settings.OpenAiApiKey))
        {
            throw new InvalidOperationException("Please provide a valid OpenAI API key in settings.");
        }

        _config["OpenAI:ApiKey"] = settings.OpenAiApiKey;
        _config["OpenAI:ChatModelId"] = settings.OpenAiModelId;

        builder.AddOpenAIChatCompletion(
            modelId: settings.OpenAiModelId,
            apiKey: settings.OpenAiApiKey);
    }

    /// <summary>
    /// Configure OpenRouter as the LLM provider
    /// </summary>
    // Configure OpenRouter as the LLM provider
    private void ConfigureOpenRouter(IKernelBuilder builder, LlmSettings settings)
    {
        if (string.IsNullOrEmpty(settings.OpenRouterApiKey))
        {
            throw new InvalidOperationException("Please provide a valid OpenRouter API key in settings.");
        }

        _config["OpenRouter:ApiKey"] = settings.OpenRouterApiKey;
        _config["OpenRouter:ModelId"] = settings.OpenRouterModelId;

        // Suppress the experimental warning
#pragma warning disable SKEXP0010
        builder.AddOpenAIChatCompletion(
            modelId: settings.OpenRouterModelId,
            apiKey: settings.OpenRouterApiKey,
            endpoint: new Uri("https://openrouter.ai/api/v1/chat/completions"));
#pragma warning restore SKEXP0010
    }
}
