using UnityEngine;

namespace VNFramework.Interfaces.Character
{
    public interface ISpriteTransition
    {
        IRenderLayer RenderLayer { get; set; }
        void StartTransition(Sprite sprite, float speed, bool smooth);
        void EndTransition();
    }
}
