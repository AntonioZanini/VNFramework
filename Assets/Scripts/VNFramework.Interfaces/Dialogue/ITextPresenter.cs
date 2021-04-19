namespace VNFramework.Interfaces.Dialogue
{
    public interface ITextPresenter
    {
        bool IsPresenting { get; }
        string CurrentText { get; }
        void Initialize(ISpeech speech, string preppendText);
        void Present(); 
        void Skip();
    }
}
