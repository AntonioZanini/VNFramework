namespace VNFramework.Interfaces.Dialogue
{
    public interface IDialogueSystem
    {
        IDialogueSystemElements Elements { get; set; }
        bool IsSpeaking();
        void Show(ISpeech speech);
        void Say(ISpeech speech);
        void Skip();
        void StopSpeaking();
        void Close();
    }
}
