using UnityEngine;
using VNFramework.Interfaces;
using VNFramework.Interfaces.Input;

namespace VNFramework.Core.Input
{
    public class InputArgs : IInputArgs
    {
        public string ActionName { get; set; }
        public KeyCode Key { get; set; }
        public InputEventType EventType { get; set; }
    }
}
