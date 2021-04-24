using System;
using TMPro;
using UnityEngine;
using VNFramework.Interfaces.Graphic;

namespace VNFramework.Core.Graphic
{
    public class FontSettings : IFontSettings
    {
        public Font Font { get; set; }
        public TMP_FontAsset FontAsset { get; set; }
        public Material FontMaterial { get; set; }
    }
}
