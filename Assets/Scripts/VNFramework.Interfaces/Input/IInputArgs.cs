using UnityEngine;

namespace VNFramework.Interfaces.Input
{
    public interface IInputArgs
    {
        string ActionName { get; }
        KeyCode Key { get;}
        InputEventType EventType { get;}
    }
}
