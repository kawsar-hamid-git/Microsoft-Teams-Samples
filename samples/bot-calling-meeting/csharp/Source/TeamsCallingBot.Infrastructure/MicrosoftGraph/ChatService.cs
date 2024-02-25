
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using TeamsCallingBot.Application.DTOs;
using TeamsCallingBot.Application.Interfaces.MicrosoftGraph;
using TeamsCallingBot.Shared.Extension.MicrosoftGraph;

namespace TeamsCallingBot.Infrastructure.MicrosoftGraph
{
    public class ChatService : IChatService
    {
        private readonly GraphServiceClient graphServiceClient;
        private readonly AzureAdOptions azureAdOptions;

        public ChatService(
            GraphServiceClient graphServiceClient,
            IOptions<AzureAdOptions> azureADOptions)
        {
            this.graphServiceClient = graphServiceClient;
            this.azureAdOptions = azureADOptions.Value;
        }

        /// <inheritdoc/>
        public Task<TeamsAppInstallation> InstallApp(string tenant, string chatId, string teamsCatalogAppId)
        {
            var teamsAppInstallation = new TeamsAppInstallation
            {
                AdditionalData = new Dictionary<string, object>()
                {
                    {"teamsApp@odata.bind", $"https://graph.microsoft.com/v1.0/appCatalogs/teamsApps/{teamsCatalogAppId}"}
                }
            };


            var graphServiceClient = MicrosoftGraphServiceClientExtensions
                .GetMicrosoftGraphServiceClient(azureAdOptions.ClientId!, azureAdOptions.ClientSecret!, tenant);

            return graphServiceClient.Chats[chatId].InstalledApps.Request().AddAsync(teamsAppInstallation);
        }
    }
}
