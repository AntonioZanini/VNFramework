namespace VNFramework.Interfaces.Scene
{
    public interface IScriptItem
    {
        ICommandFactory CommandFactory { get; set; }
        void Execute(string itemText);
    }
}