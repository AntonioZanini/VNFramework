using NSubstitute;
using NUnit.Framework;
using System.IO;
using TMPro;
using UnityEngine;
using VNFramework.Core.Dialogue;
using VNFramework.Interfaces.Dialogue;
using VNFramework.Interfaces.Graphic;

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

            Assert.AreEqual(speechSettings.FontSettings.FontAsset.sourceFontFile, speechTextDisplay.font.sourceFontFile);
            Assert.AreEqual(speechSettings.FontSize, speechTextDisplay.fontSize);
            Assert.AreEqual(speechSettings.FontColor, DialogueTestHelpers.GetColor(speechTextDisplay.color));
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

            Assert.AreEqual(color, DialogueTestHelpers.GetColor(speechTextDisplay.color));
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
            IFontSettings mockFontSettings = Substitute.For<IFontSettings>();
            mockFontSettings.FontAsset.Returns(Resources.Load<TMP_FontAsset>(Path.Combine("Fonts", "OpenSans SDF")));
            IDialogueSystemElements dialogueSystemElements = new DialogueSystemElements()
            {
                DialoguePanel = new GameObject(),
                DefaultSpeechSettings = DialogueTestHelpers.GetMockSpeechSettings(),
                SpeechTextDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
                SpeakerNameDisplay = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>(),
            };
            TextMeshProUGUI speechTextDisplay = (TextMeshProUGUI)dialogueSystemElements.SpeechTextDisplay;

            // ACT
            dialogueSystemElements.SetFontSettings(mockFontSettings, false);

            // ASSERT

            Assert.AreEqual(mockFontSettings.FontAsset.sourceFontFile, speechTextDisplay.font.sourceFontFile);
        }

    }
}
