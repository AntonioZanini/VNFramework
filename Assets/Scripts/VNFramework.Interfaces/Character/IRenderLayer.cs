using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VNFramework.Interfaces.Character
{
    public interface IRenderLayer
    {
        Image CurrentRenderer { get; set; }
        List<Image> RendererList { get; }
        bool IsCurrentRenderer(Image image);
        bool IsCurrentRenderer(Sprite sprite);
        void StartTransition(Sprite sprite, float speed, bool smooth);
        void EndTransition();
    }
}
