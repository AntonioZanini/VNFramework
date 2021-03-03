namespace VNFramework.Interfaces.Dialogue
{
    public interface ISpeech
    {
        ISpeechSettings SpeechSettings { get; set; }
        string SpeakerName { get; set; }
        string SpeechText { get; set; }
        bool AdditiveSpeech { get; set; }
    }
}
