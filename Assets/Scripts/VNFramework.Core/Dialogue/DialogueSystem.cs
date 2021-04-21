using System.Collections;
using UnityEngine;
using VNFramework.Core.Settings;
using VNFramework.Interfaces.Dialogue;
using VNFramework.Interfaces.Global;

namespace VNFramework.Core.Dialogue
{
    public class DialogueSystem : IDialogueSystem
    {
        private Coroutine speaking = null;
        private string previousSpeechText = string.Empty;
        private bool skip = false;
        private ITextPresenter textPresenter;
        private ICoroutineAccessor coroutineAccessor;

        public IDialogueSystemElements Elements { get; set; }

        public bool IsSpeaking() => speaking != null;

        public DialogueSystem()
        {
            textPresenter = new TextPresenter();
            coroutineAccessor = Configurations.GlobalConfiguration.CoroutineAccessor;
        }

        public DialogueSystem(
            ITextPresenter textPresenter, 
            ICoroutineAccessor coroutineAccessor,
            IDialogueSystemElements elements)
        {
            this.textPresenter = textPresenter;
            this.coroutineAccessor = coroutineAccessor;
            Elements = elements;
        }

        public void Say(ISpeech speech)
        {
            StopSpeaking();
            speaking = coroutineAccessor.StartCoroutine(Speaking(speech));
        }

        public void Skip() => skip = true;

        public void StopSpeaking()
        {
            if (IsSpeaking()) 
            {
                coroutineAccessor.StopCoroutine(speaking);
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

            textPresenter.Initialize(speech, speech.AdditiveSpeech ? previousSpeechText : string.Empty);

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

            StopSpeaking();
        }

        public void Show(ISpeech speech)
        {
            Elements.DialoguePanel.SetActive(true);
            Elements.ApplySpeechSettings(speech.SpeechSettings);
            Elements.SpeakerText = speech.SpeakerName;
            Elements.SpeechText = (speech.AdditiveSpeech ? previousSpeechText : string.Empty) + speech.SpeechText;
        }

    }
}
