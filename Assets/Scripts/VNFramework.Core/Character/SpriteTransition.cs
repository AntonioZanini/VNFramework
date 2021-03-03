using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VNFramework.Core.Helpers;
using VNFramework.Interfaces.Character;

namespace VNFramework.Core.Character
{
    public class SpriteTransition : ISpriteTransition
    {
        private Coroutine transitioning = null;

        private bool IsTransitioning => (transitioning != null);
        private List<Image> RendererList => RenderLayer.RendererList;
        private bool IsCurrent(Image image) => RenderLayer.IsCurrentRenderer(image);
        private bool IsCurrent(Sprite sprite) => RenderLayer.IsCurrentRenderer(sprite);
        public IRenderLayer RenderLayer { get; set; }

        public void StartTransition(Sprite sprite, float speed, bool smooth)
        {
            if (IsCurrent(sprite)) { return; }
            EndTransition();
            transitioning = BaseHelpers.StartCoroutine(TransitioningSprite(sprite, speed, smooth));
        }

        public void EndTransition()
        {
            if (IsTransitioning) { BaseHelpers.StopCoroutine(transitioning); }
            transitioning = null;
        }

        public void TransitionSprite(Sprite sprite, float speed, bool smooth)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator TransitioningSprite(Sprite sprite, float speed, bool smooth)
        {
            for (int i = 0; i < RendererList.Count; i++)
            {
                Image image = RendererList[i];
                if (image.sprite == sprite)
                {
                    RenderLayer.CurrentRenderer = image;
                    break;
                }
            }
            if (!IsCurrent(sprite))
            {
                Image image = Object
                    .Instantiate(RenderLayer.CurrentRenderer.gameObject, RenderLayer.CurrentRenderer.transform.parent)
                    .GetComponent<Image>();
                RendererList.Add(image);
                RenderLayer.CurrentRenderer = image;
                image.color = GraphicHelpers.SetAlpha(image.color, 0f);
                image.sprite = sprite;
            }

            while (TransitionImages(speed, smooth))
            {
                yield return new WaitForEndOfFrame();
            }

            EndTransition();
        }

        public bool TransitionImages(float speed, bool smooth)
        {
            bool anyValueChanged = false;
            speed *= Time.deltaTime;

            for (int i = RendererList.Count - 1; i >= 0; i--)
            {
                Image image = RendererList[i];
                bool isActiveImage = IsCurrent(image);
                float targetOpacity = isActiveImage ? 1f : 0f;
                float alphaValue = smooth ? Mathf.Lerp(image.color.a, targetOpacity, speed)
                                          : Mathf.MoveTowards(image.color.a, targetOpacity, speed);

                image.color = GraphicHelpers.SetAlpha(image.color, alphaValue);
                anyValueChanged = true;

                if (!isActiveImage && image.color.a <= 0.01f)
                {
                    RendererList.RemoveAt(i);
                    Object.DestroyImmediate(image.gameObject);
                }
            }

            if (RendererList.Count == 1 && RenderLayer.CurrentRenderer.color.a == 1f)
            {
                return false;
            }

            return anyValueChanged;
        }

    }
}
