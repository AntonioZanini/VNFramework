using UnityEngine;

namespace VNFramework.Interfaces.Character
{
    public interface ISingleLayerRenderer
    {
        void SetSprite(Sprite sprite);
        void Transition(Sprite sprite, float speed, bool smooth);
    }
}
