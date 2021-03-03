using UnityEngine;
using VNFramework.Interfaces.Character;

namespace VNFramework.Core.Character
{
    public class SingleLayerRenderer : ISingleLayerRenderer
    {
        private IRenderLayer renderLayer;

        public SingleLayerRenderer(IRenderLayer layer)
        {
            renderLayer = layer;
        }

        public void SetSprite(Sprite sprite)
        {
            renderLayer.CurrentRenderer.sprite = sprite;
        }

        public void Transition(Sprite sprite, float speed, bool smooth)
        {
            renderLayer.StartTransition(sprite, speed, smooth);
        }
    }
}
