namespace VNFramework.Interfaces.Character
{
    public interface IMultiLayerRenderedActor<TLayerType>
    {
        IMultiLayerRenderer<TLayerType> Renderer { get; }
    }
}
