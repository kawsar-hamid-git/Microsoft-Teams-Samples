using System;
using Azure.Identity;
using TeamsCallingBot.Application.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using TeamsCallingBot.Application.Interfaces.MicrosoftGraph;
using TeamsCallingBot.Infrastructure.MicrosoftGraph;
using TeamsCallingBot.Shared.Constants.MicrosoftGraph;
using TeamsCallingBot.Shared.Interfaces.MicrosoftGraph;

namespace TeamsCallingBot.Shared.Extension.MicrosoftGraph
{
    public static class MicrosoftGraphExtensions
    {
        /// <summary>
        /// Adds the services that are available in this project to Dependency Injection.
        /// Include this in your Startup.cs ConfigureServices if you need to access these services.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="azureAdOptionsAction">AzureAD Options.</param>
        /// <returns>Service collections.</returns>
        public static IServiceCollection AddMicrosoftGraphServices(this IServiceCollection services, Action<AzureAdOptions> azureAdOptionsAction)
        {
            var options = new AzureAdOptions();
            azureAdOptionsAction(options);

            ClientSecretCredential authenticationProvider = new ClientSecretCredential(options.TenantId, options.ClientId, options.ClientSecret);

            services.AddScoped<GraphServiceClient, GraphServiceClient>(sp =>
            {
                return new GraphServiceClient(authenticationProvider);
            });

            services.AddTransient<ICallService, CallService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IOnlineMeetingService, OnlineMeetingService>();
            services.AddSingleton<AudioRecordingConstants>();
            return services;
        }

        /// <summary>
        /// Create Graph Service Client
        /// </summary>
        /// <param name="azureAdOptionsAction"></param>
        /// <returns></returns>
        public static GraphServiceClient GetMicrosoftGraphServiceClient(string ClientId, string ClientSecret, string TenantId)
        {
            ClientSecretCredential authenticationProvider = new(TenantId, ClientId, ClientSecret);

            return new GraphServiceClient(authenticationProvider);
        }
    }
}