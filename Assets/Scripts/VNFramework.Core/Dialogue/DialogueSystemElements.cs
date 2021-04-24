using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VNFramework.Core.GraphicExtensions;
using VNFramework.Core.Helpers;
using VNFramework.Interfaces.Dialogue;
using VNFramework.Interfaces.Graphic;

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
            SetFontSettings(speechSettings.FontSettings, speechSettings.FontOnSpeakerName);
            SetFontSize(speechSettings.FontSize);
            SetFontColor(speechSettings.FontColor);
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
                SpeechTextElement.color = fontColor.ToColor32();
                SpeakerNameElement.color = fontColor.ToColor32();
            }
            catch
            {
                SetDefaultFontColor();
            }
        }

        public void SetDefaultFontColor()
        {
            SpeechTextElement.color = DefaultSpeechSettings.FontColor.ToColor32();
            SpeakerNameElement.color = DefaultSpeechSettings.FontColor.ToColor32();
        }

        public void SetFontSettings(IFontSettings fontSettings, bool fontOnSpeaker)
        {
            try
            {
                if (fontSettings == null)
                {
                    SetDefaultFontSettings();
                    return;
                }
                if (fontSettings.FontAsset != null) { SpeechTextElement.font = fontSettings.FontAsset; }
                if (fontSettings.FontMaterial != null) { SpeechTextElement.fontMaterial = fontSettings.FontMaterial; }

                if (fontOnSpeaker)
                {
                    SpeakerNameElement.font = DefaultSpeechSettings.FontSettings.FontAsset;
                    return;
                }

                if (fontSettings.FontAsset != null) { SpeakerNameElement.font = fontSettings.FontAsset; }
                if (fontSettings.FontMaterial != null) { SpeakerNameElement.fontMaterial = fontSettings.FontMaterial; }
            }
            catch
            {
                try { SetDefaultFontSettings(); }
                catch { }
            }
        }

        public void SetDefaultFontSettings()
        {
            SpeakerNameElement.font = DefaultSpeechSettings.FontSettings.FontAsset;
            SpeechTextElement.font = DefaultSpeechSettings.FontSettings.FontAsset;
        }
    }
}
