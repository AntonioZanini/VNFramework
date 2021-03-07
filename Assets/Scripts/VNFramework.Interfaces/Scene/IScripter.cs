namespace VNFramework.Interfaces.Scene
{
    public interface IScripter
    {
        ICommandFactory CommandFactory { get; set; }
        void Initialize(IScriptText scriptText);
        void Execute();
    }
}