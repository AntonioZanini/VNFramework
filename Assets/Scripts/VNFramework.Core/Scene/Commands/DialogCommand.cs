using VNFramework.Core.Dialogue;
using VNFramework.Interfaces.Scene;

namespace VNFramework.Core.Scene.Commands
{
    class DialogCommand : ICommand
    {
        private string speakerName;
        private string speechText;
        private bool additive;
        public DialogCommand(params string[] args)
        {
            ReadParams(args);
        }

        public void Execute()
        {
            DialogueSystem dialogueSystem;
            dialogueSystem.Say(new Speech()
            {
                SpeechSettings = dialogueSystem.Elements.DefaultSpeechSettings,
                SpeakerName = speakerName,
                SpeechText = speechText,
                AdditiveSpeech = additive
            });
        }

        private void ReadParams(string[] parameters)
        {
            if (parameters.Length == 1)
            {
                speakerName = string.Empty;
                speechText = parameters[0];
            }
            else if (parameters.Length > 1)
            {
                speakerName = parameters[0];
                speechText = parameters[1];
                if (parameters.Length > 2)
                {
                    additive = (parameters[2].ToUpper().Equals("A"));
                }
            }
        }
    }
}
