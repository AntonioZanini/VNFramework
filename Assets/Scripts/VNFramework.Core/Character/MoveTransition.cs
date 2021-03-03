using System.Collections;
using UnityEngine;
using VNFramework.Core.Helpers;
using VNFramework.Interfaces.Character;

namespace VNFramework.Core.Character
{
    public class MoveTransition : IMoveTransition
    {
        Vector2 targetPosition;
        Coroutine moving;

        public RectTransform ElementRect { get; set; }
        private Vector2 ContentSize => ElementRect.anchorMax - ElementRect.anchorMin;
        bool IsMoving => (moving != null);

        public void MoveTo(Vector2 target, float speed, bool smooth = true)
        {
            StopMoving();
            moving = BaseHelpers.StartCoroutine(Moving(target, speed, smooth));
        }

        public void StopMoving(bool arriveTargetPositionImmediately = false)
        {
            if (IsMoving)
            {
                BaseHelpers.StopCoroutine(moving);
                if (arriveTargetPositionImmediately)
                {
                    SetPosition(targetPosition);
                }
            }
            moving = null;
        }

        private void SetPosition(Vector2 target)
        {
            targetPosition = target;
            float maxXValue = 1f - ContentSize.x;
            float maxYValue = 1f - ContentSize.y;

            Vector2 minAnchorTarget = new Vector2(
                 maxXValue * targetPosition.x,
                 maxYValue * targetPosition.y
                 );

            ElementRect.anchorMin = minAnchorTarget;
            ElementRect.anchorMax = ElementRect.anchorMin + ContentSize;
        }

        IEnumerator Moving(Vector2 target, float speed, bool smooth)
        {
            speed *= Time.deltaTime;
            targetPosition = target;
            float maxXValue = 1f - ContentSize.x;
            float maxYValue = 1f - ContentSize.y;

            Vector2 minAnchorTarget = new Vector2(
                 maxXValue * targetPosition.x,
                 maxYValue * targetPosition.y
                 );

            while (ElementRect.anchorMin != minAnchorTarget)
            {
                ElementRect.anchorMin = (!smooth) ?
                    Vector2.MoveTowards(ElementRect.anchorMin, minAnchorTarget, speed) :
                    Vector2.Lerp(ElementRect.anchorMin, minAnchorTarget, speed);
                ElementRect.anchorMax = ElementRect.anchorMin + ContentSize;
                yield return new WaitForEndOfFrame();
            }

            StopMoving();
        }
    }
}
