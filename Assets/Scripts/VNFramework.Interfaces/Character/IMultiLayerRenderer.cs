using UnityEngine;

namespace VNFramework.Interfaces.Character
{
    public interface IMultiLayerRenderer<TLayerType>
    {
        IRenderLayer GetRenderLayer(TLayerType layerType);
        void AddLayer(TLayerType layerType, IRenderLayer renderLayer);
        void SetSprites(Sprite[] sprites);
        void Transition(Sprite[] sprites, float speed, bool smooth);
    }
}
