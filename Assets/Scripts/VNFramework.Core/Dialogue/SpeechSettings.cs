using UnityEngine;
using VNFramework.Interfaces.Dialogue;
using VNFramework.Interfaces.Graphic;

namespace VNFramework.Core.Dialogue
{
    public class SpeechSettings : ISpeechSettings
    {
        public IFontSettings FontSettings { get; set; }
        public float FontSize { get; set; }
        public Color FontColor { get; set; }
        public bool FontOnSpeakerName { get; set; }
        public float DisplaySpeed { get; set; }

        public ISpeechSettings Clone()
        {
            return new SpeechSettings()
            {
                FontSettings = FontSettings,
                FontSize = FontSize,
                FontColor = FontColor,
                FontOnSpeakerName = FontOnSpeakerName,
                DisplaySpeed = DisplaySpeed
            };
        }
    }
}
