using System;
using System.Collections.Generic;
using VNFramework.Interfaces.Scene;

namespace VNFramework.Core.Scene
{
    public class ScriptItem : IScriptItem
    {
        private const string COMMAND_SEPARATOR = "|";
        private List<string> allCommandTexts = new List<string>();

        public ICommandFactory CommandFactory { get; set; }

        public void Execute(string itemText)
        {
            ClearCommands();
            LoadCommands(itemText);
            foreach (string commandText in allCommandTexts)
            {
                ICommand command = CommandFactory.GetCommand(commandText);
                command.Execute();
            }
        }

        private void LoadCommands(string commandsText)
        {
            allCommandTexts.AddRange(
                commandsText.Split(
                    new[] { COMMAND_SEPARATOR },
                    StringSplitOptions.RemoveEmptyEntries
                )
            );
        }

        private void ClearCommands()
        {
            allCommandTexts.Clear();
        }
    }
}
