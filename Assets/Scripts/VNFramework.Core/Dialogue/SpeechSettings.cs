using UnityEngine;
using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Core.Dialogue
{
    public class SpeechSettings : ISpeechSettings
    {
        public Font Font { get; set; }
        public float FontSize { get; set; }
        public Color FontColor { get; set; }
        public bool FontOnSpeakerName { get; set; }
        public float DisplaySpeed { get; set; }

        public ISpeechSettings Clone()
        {
            return new SpeechSettings()
            {
                Font = Font,
                FontSize = FontSize,
                FontColor = FontColor,
                FontOnSpeakerName = FontOnSpeakerName,
                DisplaySpeed = DisplaySpeed
            };
        }
    }
}
