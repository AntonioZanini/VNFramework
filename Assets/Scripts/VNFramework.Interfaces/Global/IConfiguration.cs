using UnityEngine;
using VNFramework.Interfaces.Input;

namespace VNFramework.Interfaces.Global
{
    public interface IConfiguration
    {
        MonoBehaviour GameObject { get; }
        IInputManager InputManager { get; }
    }
}
