using Azure.Identity;
using Microsoft.Graph;

namespace TeamsCallingBot.Shared.Extension.MicrosoftGraph
{
    public static class MicrosoftGraphServiceClientExtensions
    {
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
