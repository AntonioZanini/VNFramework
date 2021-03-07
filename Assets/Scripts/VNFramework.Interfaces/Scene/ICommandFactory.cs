namespace VNFramework.Interfaces.Scene
{
    public interface ICommandFactory
    {
        ICommand GetCommand(string commandText);
    }
}
