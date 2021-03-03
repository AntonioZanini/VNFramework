using UnityEngine;
using UnityEngine.UI;

namespace VNFramework.Interfaces.Dialogue
{
    public interface IDialogueSystemElements
    {
        ISpeechSettings DefaultSpeechSettings { get; set; }
        ILayoutElement SpeakerNameDisplay { get; set; }
        ILayoutElement SpeechTextDisplay { get; set; }
        string SpeakerText { get; set; }
        string SpeechText { get; set; }
        GameObject DialoguePanel { get; set; }

        void ApplySpeechSettings(ISpeechSettings speechSettings);

        void SetFontSize(float fontSize);
        void SetDefaultFontSize();

        void SetFontColor(Color fontColor);
        void SetDefaultFontColor();

        void SetFont(Font font, bool fontOnSpeaker);
        void SetDefaultFont();
    }
}
