using System;
using System.Linq;
using VNFramework.Core.Dialogue;
using VNFramework.Core.Settings;
using VNFramework.Interfaces.Scene;

namespace VNFramework.Core.Scene.Commands
{
    public class SpeechCommand : ICommand
    {
        private string speakerName;
        private string speechText;
        private bool additive;

        public static SpeechCommand New(params string[] args)
        {
            return new SpeechCommand(args);
        }

        public SpeechCommand(params string[] args)
        {
            ReadParams(args);
        }

        public void Execute()
        {
            Configurations.GlobalConfiguration.DialogueSystem.Say(new Speech()
            {
                SpeechSettings = Configurations.GlobalConfiguration.DialogueSystem.Elements.DefaultSpeechSettings,
                SpeakerName = speakerName,
                SpeechText = speechText,
                AdditiveSpeech = additive
            });
        }

        private void ReadParams(string[] parameters)
        {
            string[] actualParams = string.Join(" ", parameters)
                                          .Split(
                                             new[] { "'" }, 
                                             StringSplitOptions.RemoveEmptyEntries)
                                          .Where(p => p.Trim().Length > 0)
                                          .ToArray();
            if (actualParams.Length == 1)
            {
                speakerName = string.Empty;
                speechText = actualParams[0];
            }
            else if (actualParams.Length > 1)
            {
                speakerName = actualParams[0];
                speechText = actualParams[1];
                if (actualParams.Length > 2)
                {
                    additive = (actualParams[2].Trim().ToUpper().Equals("A"));
                }
            }
        }
    }
}
