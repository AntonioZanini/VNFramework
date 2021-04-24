using UnityEngine;
using VNFramework.Interfaces.Graphic;

namespace VNFramework.Interfaces.Dialogue
{
    public interface ISpeechSettings
    {
        IFontSettings FontSettings { get; set; }
        float FontSize { get; set; }
        Color FontColor { get; set; }
        bool FontOnSpeakerName { get; set; }
        float DisplaySpeed { get; set; }

        ISpeechSettings Clone();
    }
}
