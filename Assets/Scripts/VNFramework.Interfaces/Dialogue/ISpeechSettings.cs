using UnityEngine;

namespace VNFramework.Interfaces.Dialogue
{
    public interface ISpeechSettings
    {
        Font Font { get; set; }
        float FontSize { get; set; }
        Color FontColor { get; set; }
        bool FontOnSpeakerName { get; set; }
        float DisplaySpeed { get; set; }

        ISpeechSettings Clone();
    }
}
