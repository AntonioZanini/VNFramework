using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Core.Dialogue
{
    public class Speech : ISpeech
    {
        public ISpeechSettings SpeechSettings { get; set; }
        public string SpeakerName { get; set; }
        public string SpeechText { get; set; }
        public bool AdditiveSpeech { get; set; }
    }
}
