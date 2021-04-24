using TMPro;
using UnityEngine;

namespace VNFramework.Interfaces.Graphic
{
    public interface IFontSettings
    {
        Font Font { get; }
        TMP_FontAsset FontAsset { get; }
        Material FontMaterial { get; }

    }
}
