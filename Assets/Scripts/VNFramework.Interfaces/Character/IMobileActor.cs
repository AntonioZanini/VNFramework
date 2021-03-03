using UnityEngine;

namespace VNFramework.Interfaces.Character
{
    public interface IMobileActor
    {
        void MoveTo(Vector2 target, float speed, bool smooth = true);
        void StopMoving(bool moveToTarget = false);
    }
}
