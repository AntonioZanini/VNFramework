using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VNFramework.Core.GraphicExtensions;
using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Core.Dialogue
{
    public class DialogueSystemElements : IDialogueSystemElements
    {
        private TextMeshProUGUI SpeechTextElement => (TextMeshProUGUI)SpeechTextDisplay;
        private TextMeshProUGUI SpeakerNameElement => (TextMeshProUGUI)SpeakerNameDisplay;

        public ISpeechSettings DefaultSpeechSettings { get; set; }
        public ILayoutElement SpeakerNameDisplay { get; set; }
        public ILayoutElement SpeechTextDisplay { get; set; }
        public GameObject DialoguePanel { get; set; }
        public string SpeakerText { get => SpeakerNameElement.text; set => SpeakerNameElement.text = value; }
        public string SpeechText { get => SpeechTextElement.text; set => SpeechTextElement.text = value; }

        public void ApplySpeechSettings(ISpeechSettings speechSettings)
        {
            SetFont(speechSettings.Font, speechSettings.FontOnSpeakerName);
            SetFontColor(speechSettings.FontColor);
            SetFontSize(speechSettings.FontSize);
        }

        public void SetFontSize(float fontSize)
        {
            try
            {
                if (fontSize <= 0)
                {
                    SetDefaultFontSize();
                    return;
                }
                SpeechTextElement.fontSize = fontSize;
            }
            catch
            {
                SetDefaultFontSize();
            }
        }

        public void SetDefaultFontSize()
        {
            SpeechTextElement.fontSize = DefaultSpeechSettings.FontSize;
        }

        public void SetFontColor(Color fontColor)
        {
            try
            {
                if (fontColor == null)
                {
                    SetDefaultFontColor();
                    return;
                }
                SpeechTextElement.faceColor = fontColor.ToColor32();
                SpeakerNameElement.faceColor = fontColor.ToColor32();
            }
            catch
            {
                SetDefaultFontColor();
            }
        }

        public void SetDefaultFontColor()
        {
            SpeechTextElement.faceColor = DefaultSpeechSettings.FontColor.ToColor32();
            SpeakerNameElement.faceColor = DefaultSpeechSettings.FontColor.ToColor32();
        }

        public void SetFont(Font font, bool fontOnSpeaker)
        {
            try
            {
                if (font == null)
                {
                    SetDefaultFont();
                    return;
                }
                SpeechTextElement.font = font.ToTMP_FontAsset();
                SpeakerNameElement.font = fontOnSpeaker ?
                                          font.ToTMP_FontAsset() :
                                          DefaultSpeechSettings.Font.ToTMP_FontAsset();
            }
            catch
            {
                try { SetDefaultFont(); }
                catch { }
            }
        }

        public void SetDefaultFont()
        {
            SpeechTextElement.font = DefaultSpeechSettings.Font.ToTMP_FontAsset();
            SpeakerNameElement.font = DefaultSpeechSettings.Font.ToTMP_FontAsset();
        }

    }
}
