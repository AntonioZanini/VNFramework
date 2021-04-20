using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VNFramework.Core.Dialogue;
using VNFramework.Interfaces.Dialogue;

namespace Tests.Dialogue
{
    public class DialogueSystemElementsTest
    {
        [Test]
        public void SpeechText_ShouldWriteInTextMesh_WhenSettedValue()
        {
            // ARRANGE
            GameObject mockDialogPanel = new GameObject();
            ILayoutElement speakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>();
            ILayoutElement speechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>();
            string speechText = "Speech text example.";
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = mockDialogPanel,
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeakerNameDisplay = speakerNameDisplay,
                SpeechTextDisplay = speechTextDisplay
            };

            // ACT
            dialogueSystemElements.SpeechText = speechText;

            // ASSERT
            Assert.AreEqual(speechText, ((TextMeshProUGUI)dialogueSystemElements.SpeechTextDisplay).text);
        }

        [Test]
        public void SpeakerName_ShouldWriteInTextMesh_WhenSettedValue()
        {
            // ARRANGE
            GameObject mockDialogPanel = new GameObject();
            ILayoutElement speakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>();
            ILayoutElement speechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>();
            string speakerName = "Narrator";
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = mockDialogPanel,
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeakerNameDisplay = speakerNameDisplay,
                SpeechTextDisplay = speechTextDisplay
            };

            // ACT
            dialogueSystemElements.SpeakerText = speakerName;

            // ASSERT
            Assert.AreEqual(speakerName, ((TextMeshProUGUI)dialogueSystemElements.SpeakerNameDisplay).text);
        }
    }
}
