using System;
using TMPro;
using UnityEngine;

namespace VNFramework.Core.GraphicExtensions
{
    public static class GraphicExtensions
    {
        public static Color32 ToColor32(this Color color)
        {
            byte r = Convert.ToByte(Mathf.FloorToInt(color.r * 255));
            byte g = Convert.ToByte(Mathf.FloorToInt(color.g * 255));
            byte b = Convert.ToByte(Mathf.FloorToInt(color.b * 255));
            byte a = Convert.ToByte(Mathf.FloorToInt(color.a * 255));
            return new Color32(r, g, b, a);
        }

        public static TMP_FontAsset ToTMP_FontAsset(this Font font)
        {
            return TMP_FontAsset.CreateFontAsset(font); 
        }
    }
}
