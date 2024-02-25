
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TeamsCallingBot.Application.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using TeamsCallingBot.Application.Interfaces.MicrosoftGraph;
using TeamsCallingBot.Shared.Extension.MicrosoftGraph;

namespace TeamsCallingBot.Shared.Interfaces.MicrosoftGraph
{
    public class OnlineMeetingService : IOnlineMeetingService
    {
        private readonly GraphServiceClient graphServiceClient;
        private readonly UsersOptions usersOptions;
        private readonly AzureAdOptions azureAdOptions;

        public OnlineMeetingService(
            GraphServiceClient graphServiceClient,
            IOptions<AzureAdOptions> azureADOptions, 
            IOptions<UsersOptions> usersOptions)
        {
            this.graphServiceClient = graphServiceClient;
            this.usersOptions = usersOptions.Value;
            this.azureAdOptions = azureADOptions.Value;
        }

        /// <inheritdoc/>
        public Task<OnlineMeeting> Create(string tenant, string subject, IEnumerable<string> participantsIds)
        {
            var onlineMeeting = new OnlineMeeting
            {
                StartDateTime = DateTime.UtcNow,
                EndDateTime = DateTime.UtcNow.AddMinutes(30),
                Subject = subject,
                Participants = new MeetingParticipants
                {
                    Attendees = participantsIds.Select(p => new MeetingParticipantInfo
                    {
                        Identity = new IdentitySet
                        {
                            User = new Identity
                            {
                                Id = p,
                            }
                        },
                        Role = OnlineMeetingRole.Presenter,
                    })
                }
            };

            var graphServiceClient = MicrosoftGraphExtensions.GetMicrosoftGraphServiceClient(azureAdOptions.ClientId!, azureAdOptions.ClientSecret!, tenant);

            // To call this API the user (UserIdWithAssignedOnlineMeetingPolicy) must have been granted an application access policy
            // https://learn.microsoft.com/en-us/graph/cloud-communication-online-meeting-application-access-policy
            return graphServiceClient.Users[usersOptions.UserIdWithAssignedOnlineMeetingPolicy].OnlineMeetings
                .Request()
                .AddAsync(onlineMeeting);
        }

        /// <inheritdoc/>
        public Task<OnlineMeeting> Get(string meetingId, string tenant)
        {
            var graphServiceClient = MicrosoftGraphExtensions.GetMicrosoftGraphServiceClient(azureAdOptions.ClientId!, azureAdOptions.ClientSecret!, tenant);

            return graphServiceClient.Users[usersOptions.UserIdWithAssignedOnlineMeetingPolicy].OnlineMeetings[meetingId]
                .Request()
                .GetAsync();
        }
    }
}
