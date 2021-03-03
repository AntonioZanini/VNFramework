using System.Collections;
using UnityEngine;
using VNFramework.Core.Helpers;
using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Core.Dialogue
{
    public class TextPresenter : ITextPresenter
    {
        private int charactersPerFrame = 1;
        private ISpeech speech;
        private string preppendText;
        private bool skip = false;
        private Coroutine presenting = null;
        private short runsThisFrame = 0;

        public bool IsPresenting => presenting != null;

        public string CurrentText { get; protected set; }

        public TextPresenter(ISpeech speech, string preppendText = "")
        {
            this.speech = speech;
            this.preppendText = preppendText;
        }

        public void StopPresenting()
        {
            if (IsPresenting)
            {
                BaseHelpers.StopCoroutine(presenting);
                presenting = null;
            }
        }

        public void Present()
        {
            StopPresenting();
            presenting = BaseHelpers.StartCoroutine(Presenting());
        }

        public void Skip() => skip = true;

        IEnumerator Presenting()
        {
            runsThisFrame = 0;
            LineProcessor lineProcessor = new LineProcessor();
            lineProcessor.ProcessLine(speech.SpeechText);
            CurrentText = preppendText;

            foreach (var segment in lineProcessor.Segments)
            {
                if (segment.IsTag) { CurrentText += segment.Text; }
                else
                {
                    foreach (char letter in segment.Text.ToCharArray())
                    {
                        bool isDone = DisplaySegmentLetter(letter);
                        if (isDone)
                        {
                            float waitTime = skip ? 0.01f : 0.01f * speech.SpeechSettings.DisplaySpeed;
                            yield return new WaitForSeconds(waitTime);
                        }
                    }
                }
            }
            presenting = null;
        }

        private bool DisplaySegmentLetter(char letter)
        {
            CurrentText += letter;

            runsThisFrame++;
            int maxRunsPerFrame = skip ? 5 : charactersPerFrame;
            if (runsThisFrame == maxRunsPerFrame)
            {
                runsThisFrame = 0;
                return true;
            }
            return false;
        }

    }
}
