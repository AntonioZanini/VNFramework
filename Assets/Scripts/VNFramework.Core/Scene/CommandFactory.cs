using VNFramework.Interfaces.Scene;

namespace VNFramework.Core.Scene
{
    public class CommandFactory : ICommandFactory
    {
        private const string DIALOG_COMMAND = "DLG";
        private const string CHANGE_SPRITE_COMMAND = "CHGSPR";
        private const string HIDE_SPRITE_COMMAND = "HIDSPR";
        public ICommand GetCommand(string commandText)
        {
            string[] commandSegments = commandText.Trim().Split(' ');
            string commandHeader = commandSegments[0].ToUpper();
            switch (commandHeader)
            {
                case DIALOG_COMMAND:            return new ICommand();
                case CHANGE_SPRITE_COMMAND:     return new ICommand();
                case HIDE_SPRITE_COMMAND:       return new ICommand();
                default:                        return null;
            }
        }
    }
}
