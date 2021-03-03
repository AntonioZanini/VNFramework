using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VNFramework.Interfaces.Character;

namespace VNFramework.Core.Character
{
    public class RenderLayer : IRenderLayer
    {
        protected Image currentRenderer;

        private ISpriteTransition Transition { get; set; }

        public Image CurrentRenderer
        {
            get => currentRenderer;
            set
            {
                currentRenderer = value;
                if (!RendererList.Contains(CurrentRenderer))
                {
                    RendererList.Add(currentRenderer);
                }
            }
        }
        public List<Image> RendererList { get; protected set; }

        public RenderLayer()
        {
            RendererList = new List<Image>();
            Transition = new SpriteTransition();
            Transition.RenderLayer = this;
        }

        public bool IsCurrentRenderer(Image image)
        {
            if (CurrentRenderer == null) { return false; }
            return (CurrentRenderer == image);
        }

        public bool IsCurrentRenderer(Sprite sprite)
        {
            if (CurrentRenderer == null) { return false; }
            return (CurrentRenderer.sprite == sprite);
        }

        public void StartTransition(Sprite sprite, float speed, bool smooth)
        {
            Transition.StartTransition(sprite, speed, smooth);
        }

        public void EndTransition()
        {
            Transition.EndTransition();
        }
    }
}
