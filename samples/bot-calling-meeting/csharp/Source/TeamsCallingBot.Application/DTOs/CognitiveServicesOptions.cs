namespace TeamsCallingBot.Application.DTOs
{
    /// <summary>
    /// The Cognitive Services options class.
    /// </summary>
    public class CognitiveServicesOptions
    {
        /// <summary>
        /// Is the service enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Cognitive Services speech key
        /// </summary>
        public string? SpeechKey { get; set; }

        /// <summary>
        /// Cognitive Services speech region
        /// </summary>
        public string? SpeechRegion { get; set; }

        /// <summary>
        /// The language to use when recognising speech
        /// </summary>
        public string? SpeechRecognitionLanguage { get; set; }
    }
}