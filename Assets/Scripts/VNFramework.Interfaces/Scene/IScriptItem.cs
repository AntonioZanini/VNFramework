namespace VNFramework.Interfaces.Scene
{
    interface IScriptItem
    {
        ICommandFactory CommandFactory { get; set; }
        void Execute(string itemText);
    }
}