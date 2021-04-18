using VNFramework.Interfaces.Global;
using System.Collections;
using UnityEngine;
using VNFramework.Core.Helpers;
using VNFramework.Interfaces.Dialogue;
using VNFramework.Core.Settings;

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
        private ICoroutineAccessor coroutineAccessor;
        private ILineProcessor lineProcessor;

        public bool IsPresenting => presenting != null;

        public string CurrentText { get; protected set; }

        public TextPresenter(ISpeech speech, string preppendText = "")
        {
            this.speech = speech;
            this.preppendText = preppendText;
            coroutineAccessor = Configurations.GlobalConfiguration.CoroutineAccessor;
            lineProcessor = new LineProcessor<LineSegment>();
        }
        
        public TextPresenter(ISpeech speech, ICoroutineAccessor coroutineAccessor, ILineProcessor lineProcessor, string preppendText = "")
        {
            this.speech = speech;
            this.preppendText = preppendText;
            this.coroutineAccessor = coroutineAccessor;
            this.lineProcessor = lineProcessor;
        }

        public void StopPresenting()
        {
            if (IsPresenting)
            {
                coroutineAccessor.StopCoroutine(presenting);
                presenting = null;
            }
        }

        public void Present()
        {
            StopPresenting();
            presenting = coroutineAccessor.StartCoroutine(Presenting());
        }

        public void Skip() => skip = true;

        IEnumerator Presenting()
        {
            runsThisFrame = 0;
            
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
