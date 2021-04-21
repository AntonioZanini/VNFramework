using NUnit.Framework;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
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
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = new GameObject(),
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
                SpeakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
            };
            string speechText = "Speech text example.";

            // ACT
            dialogueSystemElements.SpeechText = speechText;

            // ASSERT
            Assert.AreEqual(speechText, ((TextMeshProUGUI)dialogueSystemElements.SpeechTextDisplay).text);
        }

        [Test]
        public void SpeakerName_ShouldWriteInTextMesh_WhenSettedValue()
        {
            // ARRANGE
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = new GameObject(),
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
                SpeakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
            };
            string speakerName = "Narrator";

            // ACT
            dialogueSystemElements.SpeakerText = speakerName;

            // ASSERT
            Assert.AreEqual(speakerName, ((TextMeshProUGUI)dialogueSystemElements.SpeakerNameDisplay).text);
        }

        [Test]
        public void ApplySpeechSettings_ShouldChangePropertiesOfSpeechTextDisplay_WhenSpeechSettingsHasValues()
        {
            // ARRANGE
            ISpeechSettings speechSettings = DialogueTestHelpers.GetMockSpeechSettings();
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = new GameObject(),
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
                SpeakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
            };
            TextMeshProUGUI speechTextDisplay = (TextMeshProUGUI)dialogueSystemElements.SpeechTextDisplay;

            // ACT
            dialogueSystemElements.ApplySpeechSettings(speechSettings);

            // ASSERT

            Assert.AreEqual(TMP_FontAsset.CreateFontAsset(speechSettings.Font).sourceFontFile, speechTextDisplay.font.sourceFontFile);
            Assert.AreEqual(DialogueTestHelpers.GetColor32(speechSettings.FontColor), speechTextDisplay.faceColor);
            Assert.AreEqual(speechSettings.FontSize, speechTextDisplay.fontSize);
        }
        
        [Test]
        public void SetFontColor_ShouldChangeFontColorOfSpeechTextDisplay_WhenReceivesColorParam()
        {
            // ARRANGE
            Color color = Color.red;
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = new GameObject(),
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
                SpeakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
            };
            TextMeshProUGUI speechTextDisplay = (TextMeshProUGUI)dialogueSystemElements.SpeechTextDisplay;

            // ACT
            dialogueSystemElements.SetFontColor(color);

            // ASSERT

            Assert.AreEqual(DialogueTestHelpers.GetColor32(color), speechTextDisplay.faceColor);
        }
        
        [Test]
        public void SetFontSize_ShouldChangeFontSizeOfSpeechTextDisplay_WhenReceivesSizeParam()
        {
            // ARRANGE
            float fontSize = 25.5f;
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = new GameObject(),
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
                SpeakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
            };
            TextMeshProUGUI speechTextDisplay = (TextMeshProUGUI)dialogueSystemElements.SpeechTextDisplay;

            // ACT
            dialogueSystemElements.SetFontSize(fontSize);

            // ASSERT
            Assert.AreEqual(fontSize, speechTextDisplay.fontSize);
        }
        
        [Test]
        public void SetFont_ShouldChangeFontOfSpeechTextDisplay_WhenReceivesFontParam()
        {
            // ARRANGE
            Font font = Resources.Load<Font>(Path.Combine("Fonts", "OpenSans"));
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = new GameObject(),
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
                SpeakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
            };
            TextMeshProUGUI speechTextDisplay = (TextMeshProUGUI)dialogueSystemElements.SpeechTextDisplay;

            // ACT
            dialogueSystemElements.SetFont(font, false);

            // ASSERT

            Assert.AreEqual(TMP_FontAsset.CreateFontAsset(font).sourceFontFile, speechTextDisplay.font.sourceFontFile);
        }

    }
}
