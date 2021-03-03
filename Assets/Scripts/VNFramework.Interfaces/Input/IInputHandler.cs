using static UnityEngine.InputSystem.InputAction;

namespace VNFramework.Interfaces.Input
{
    public interface IInputHandler
    {
        void HandleInput(CallbackContext context);
    }
}
