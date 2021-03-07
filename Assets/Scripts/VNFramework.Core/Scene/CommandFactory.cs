using System;
using System.Collections.Generic;
using System.Linq;
using VNFramework.Core.Scene.Commands;
using VNFramework.Interfaces.Scene;

namespace VNFramework.Core.Scene
{
    public class CommandFactory : ICommandFactory
    {
        private Dictionary<string, Func<string[], ICommand>> availableCommands;

        public CommandFactory()
        {
            availableCommands = new Dictionary<string, Func<string[], ICommand>>
            {
                {"DLG", SpeechCommand.New }
            };
        }

        public ICommand GetCommand(string commandText)
        {
            string[] commandSegments = commandText.Trim().Split(' ');
            string commandHeader = commandSegments[0].ToUpper();

            string[] args = commandSegments.Skip(1).ToArray();
            return availableCommands[commandHeader].Invoke(args);
        }
    }
}
