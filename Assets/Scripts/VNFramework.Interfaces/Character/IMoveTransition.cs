using UnityEngine;

namespace VNFramework.Interfaces.Character
{
    public interface IMoveTransition
    {
        void MoveTo(Vector2 target, float speed, bool smooth = true);
        void StopMoving(bool arriveTargetPositionImmediately = false);
    }
}
