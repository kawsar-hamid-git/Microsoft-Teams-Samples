
using System;
using TeamsCallingBot.Application.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace TeamsCallingBot.Shared.Constants.MicrosoftGraph
{
    public class AudioRecordingConstants
    {
        public AudioRecordingConstants(IOptions<BotOptions> botOptions)
        {
            Speech = new MediaInfo
            {
                Uri = new Uri(botOptions.Value.BotBaseUrl, "audio/speech.wav").ToString(),
                ResourceId = Guid.NewGuid().ToString(),
            };

            PleaseRecordYourMessage = new MediaInfo
            {
                Uri = new Uri(botOptions.Value.BotBaseUrl, "audio/please-record-your-message.wav").ToString(),
                ResourceId = Guid.NewGuid().ToString(),
            };
        }

        public readonly MediaInfo Speech;
        public readonly MediaInfo PleaseRecordYourMessage;
    }
}
