using NSubstitute;
using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using VNFramework.Core.Dialogue;
using VNFramework.Interfaces.Dialogue;
using VNFramework.Interfaces.Global;

namespace Tests.Dialogue
{
    public class DialogueSystemTest
    {

        [Test]
        public void Show_ShouldDisplayAllTextInstantly()
        {
            // ARRANGE
            string targetText = "Hello World";

            ITextPresenter textPresenter = Substitute.For<ITextPresenter>();
            textPresenter.CurrentText.Returns(targetText);

            TextMeshProUGUI txtSpeakerName = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>();
            TextMeshProUGUI txtSpeechText = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>();
            GameObject gameObject = new GameObject();

            string speakerName = string.Empty;
            string speechText = string.Empty;
            IDialogueSystemElements dialogueSystemElements = Substitute.For<IDialogueSystemElements>();
            dialogueSystemElements.DialoguePanel.Returns(gameObject);
            dialogueSystemElements.SpeakerNameDisplay.Returns(txtSpeakerName);
            dialogueSystemElements.SpeakerNameDisplay.Returns(txtSpeakerName);
            dialogueSystemElements.SpeechTextDisplay.Returns(txtSpeechText);
            dialogueSystemElements.SpeakerText.Returns(speakerName);
            dialogueSystemElements.SpeechText.Returns(speechText);

            ISpeech speech = DialogueTestHelpers.GetMockSpeech(
                targetText,
                DialogueTestHelpers.GetMockSpeechSettings()
            );

            IDialogueSystem dialogueSystem = new DialogueSystem(
                textPresenter,
                DialogueTestHelpers.GetMockCoroutineAccessor(),
                dialogueSystemElements
                );

            // ACT
            dialogueSystem.Show(speech);

            // ASSERT
            Assert.AreEqual(speech.SpeechText, dialogueSystemElements.SpeechText);
        }

        [UnityTest]
        public IEnumerator Say_ShouldWriteAllSpeechTextOnElementSpeechText_WhenPresentingEnds()
        {
            // ARRANGE
            string targetText = "Hello World";
            ICoroutineAccessor coroutineAccessor = DialogueTestHelpers.GetMockCoroutineAccessor();
            ISpeech mockSpeech = DialogueTestHelpers.GetMockSpeech(
                targetText,
                DialogueTestHelpers.GetMockSpeechSettings()
                );

            ITextPresenter mockTextPresenter = new MockTextPresenter(coroutineAccessor);

            TextMeshProUGUI txtSpeakerName = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>();
            TextMeshProUGUI txtSpeechText = DialogueTestHelpers.CreateMonoBehaviourObject<TextMeshProUGUI>();
            GameObject gameObject = new GameObject();

            string speakerName = string.Empty;
            string speechText = string.Empty;
            IDialogueSystemElements mockDialogueSystemElements = Substitute.For<IDialogueSystemElements>();
            mockDialogueSystemElements.DialoguePanel.Returns(gameObject);
            mockDialogueSystemElements.SpeakerNameDisplay.Returns(txtSpeakerName);
            mockDialogueSystemElements.SpeakerNameDisplay.Returns(txtSpeakerName);
            mockDialogueSystemElements.SpeechTextDisplay.Returns(txtSpeechText);
            mockDialogueSystemElements.SpeakerText.Returns(speakerName);
            mockDialogueSystemElements.SpeechText.Returns(speechText);

            IDialogueSystem dialogueSystem = new DialogueSystem(
                mockTextPresenter,
                coroutineAccessor,
                mockDialogueSystemElements
                );

            // ACT
            dialogueSystem.Say(mockSpeech);

            while (dialogueSystem.IsSpeaking()) { yield return new WaitForFixedUpdate(); }

            // ASSERT
            Assert.AreEqual(targetText, mockDialogueSystemElements.SpeechText);
        }

        private class MockTextPresenter : ITextPresenter
        {
            private bool skip = false;
            private ISpeech speech;
            private string preppendText;
            private ICoroutineAccessor coroutineAccessor;
            private Coroutine presenting = null;
            public bool IsPresenting => (presenting != null);

            public string CurrentText { get; private set; }

            public MockTextPresenter(ICoroutineAccessor coroutineAccessor)
            {
                this.coroutineAccessor = coroutineAccessor;
            }

            public void Initialize(ISpeech speech, string preppendText)
            {
                this.speech = speech;
                this.preppendText = preppendText;
            }

            public void Present()
            {
                presenting = coroutineAccessor.StartCoroutine(Presenting());
            }

            IEnumerator Presenting()
            {
                CurrentText = preppendText;
                int lettersPerCall = 5;
                int lettersCount = 0;
                foreach (char letter in speech.SpeechText)
                {
                    CurrentText += letter;
                    lettersCount++;
                    if (lettersCount == lettersPerCall)
                    {
                        lettersCount = 0;
                        float waitTime = skip ? 0.01f : 0.01f * speech.SpeechSettings.DisplaySpeed;
                        yield return new WaitForSeconds(waitTime);
                    }
                }
                presenting = null;
            }

            public void Skip()
            {
                skip = true;
            }
        }
    }
}
