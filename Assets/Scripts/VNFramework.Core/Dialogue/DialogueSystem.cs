using System.Collections;
using UnityEngine;
using VNFramework.Core.Helpers;
using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Core.Dialogue
{
    public class DialogueSystem : IDialogueSystem
    {
        private Coroutine speaking = null;
        private bool isWaitingForUserInput = false;
        private string previousSpeechText = string.Empty;
        private bool skip = false;

        public IDialogueSystemElements Elements { get; set; }

        public bool IsSpeaking() => speaking != null;
        public bool IsWaitingUserInput() => isWaitingForUserInput;

        public void Say(ISpeech speech)
        {
            StopSpeaking();
            //Elements.SpeechText = speech.AdditiveSpeech ? previousSpeechText : string.Empty;
            speaking = BaseHelpers.StartCoroutine(Speaking(speech));
        }

        public void Skip() => skip = true;

        public void StopSpeaking()
        {
            if (IsSpeaking()) 
            { 
                BaseHelpers.StopCoroutine(speaking);
                speaking = null;
            }
        }

        public void Close()
        {
            StopSpeaking();
            Elements.DialoguePanel.SetActive(false);
        }

        IEnumerator Speaking(ISpeech speech)
        {
            Elements.DialoguePanel.SetActive(true);
            Elements.ApplySpeechSettings(speech.SpeechSettings);
            Elements.SpeakerText = speech.SpeakerName;
            previousSpeechText = Elements.SpeechText;

            var textPresenter = GetPresenter(speech);
            isWaitingForUserInput = false;
            textPresenter.Present();
            while (textPresenter.IsPresenting)
            {
                if (skip)
                {
                    textPresenter.Skip();
                    skip = false;
                }
                Elements.SpeechText = textPresenter.CurrentText;
                yield return new WaitForSeconds(0.025f);
            }
            Elements.SpeechText = textPresenter.CurrentText;
            isWaitingForUserInput = true;

            while (isWaitingForUserInput) yield return new WaitForEndOfFrame();
            StopSpeaking();
        }

        ITextPresenter GetPresenter(ISpeech speech)
        {
            return new TextPresenter(speech, speech.AdditiveSpeech ? previousSpeechText : string.Empty);
        }

    }
}
