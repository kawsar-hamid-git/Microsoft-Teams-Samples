
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace TeamsCallingBot.Application.Interfaces.MicrosoftGraph
{
    public interface IOnlineMeetingService
    {
        /// <summary>
        /// Create a meeting. Tied to the
        /// </summary>
        /// <param name="subject">Subject of the meeting to create</param>
        /// <param name="participantsIds">Array of AAD Ids for participants</param>
        /// <returns></returns>
        Task<OnlineMeeting> Create(string subject, IEnumerable<string> participantsIds);

        /// <summary>
        ///
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        Task<OnlineMeeting> Get(string subject);
    }
}
