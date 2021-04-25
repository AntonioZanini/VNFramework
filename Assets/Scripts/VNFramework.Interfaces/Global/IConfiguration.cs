using UnityEngine;
using VNFramework.Interfaces.Dialogue;

namespace VNFramework.Interfaces.Global
{
    public interface IConfiguration
    {
        MonoBehaviour GameObject { get; }
        //IInputManager InputManager { get; }
        IDialogueSystem DialogueSystem { get; set; }
        ICoroutineAccessor CoroutineAccessor { get; }
    }
}
