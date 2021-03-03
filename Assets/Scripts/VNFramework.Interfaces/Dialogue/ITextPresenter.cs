namespace VNFramework.Interfaces.Dialogue
{
    public interface ITextPresenter
    {
        bool IsPresenting { get; }
        string CurrentText { get; }
        void Present(); 
        void Skip();
    }
}
