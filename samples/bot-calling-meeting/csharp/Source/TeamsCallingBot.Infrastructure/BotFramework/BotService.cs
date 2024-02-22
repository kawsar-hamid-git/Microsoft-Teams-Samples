
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using TeamsCallingBot.Application.Interfaces.BotFramework;

namespace TeamsCallingBot.Infrastructure.BotFramework
{
    /// <inheritdoc/>
    public class BotService : IBotService
    {
        private readonly IConnectorClientFactory connectorClientFactory;
        private readonly ILogger<BotService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotService"/> class.
        /// </summary>
        /// <param name="connectorClientFactory">Connector client factory</param>
        /// <param name="logger">Logger.</param>
        public BotService(IConnectorClientFactory connectorClientFactory, ILogger<BotService> logger)
        {
            this.connectorClientFactory = connectorClientFactory;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public Task<ResourceResponse> SendToConversation(string message, string conversationId)
        {
            return SendToConversation(MessageFactory.Text(message), conversationId);
        }

        /// <inheritdoc/>
        public Task<ResourceResponse> SendToConversation(Attachment attachment, string conversationId)
        {
            return SendToConversation(MessageFactory.Attachment(attachment), conversationId);
        }

        private async Task<ResourceResponse> SendToConversation(IActivity activity, string conversationId)
        {
            ConnectorClient client = connectorClientFactory.CreateConnectorClient();

            return await client.Conversations.SendToConversationAsync(conversationId, (Activity)activity);
        }
    }
}
