using System;
using System.Collections.Generic;
using VNFramework.Interfaces.Scene;

namespace Assets.Scripts.VNFramework.Core.Scene
{
    public class Scripter : IScripter
    {
        private List<string> scriptLines = new List<string>();
        private Int32 currentScriptLine = 0;
        private IScriptItem scriptItem;
        private bool IsDone => (scriptLines.Count <= currentScriptLine);
        public ICommandFactory CommandFactory 
        {
            get => scriptItem.CommandFactory;
            set => scriptItem.CommandFactory = value; 
        }

        public Scripter()
        {
            scriptItem = new ScriptItem();
        }

        public void Execute()
        {
            if (!IsDone)
            {
                
                scriptItem.Execute(scriptLines[currentScriptLine]);
                currentScriptLine++;
            }
        }

        public void Initialize(IScriptText scriptText)
        {
            LoadLines(scriptText.GetText());
        }

        private void LoadLines(string linesText)
        {
            scriptLines.AddRange(
                linesText.Split(
                    new string[] { Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries
                )
            );
        }
    }
}
