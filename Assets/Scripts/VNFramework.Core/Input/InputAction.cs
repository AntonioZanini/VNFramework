using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VNFramework.Interfaces;
using VNFramework.Interfaces.Input;

namespace VNFramework.Core.Input
{
    public class InputAction : IInputAction
    {
        private bool keyState;

        public string Name { get; protected set; }
        public bool Enabled { get; set; }
        public KeyCode BindingKey { get; set; }

        public event Action<IInputArgs> KeyDown;
        public event Action<IInputArgs> KeyUp;

        public InputAction(string name)
        {
            Name = name;
            Enabled = true;
        }

        public InputAction(string name, KeyCode bindingKey, bool enabled = true)
        {
            Name = name;
            BindingKey = bindingKey;
            Enabled = enabled;
        }

        public void Update()
        {
            if (!Enabled || BindingKey == KeyCode.None) { return; }

            if (IsKeyDown()) { CallKeyDown(); }
            if (IsKeyUp()) { CallKeyUp(); }

            UpdateState();
        }

        private bool IsKeyDown() =>  UnityEngine.Input.GetKey(BindingKey);
        private bool IsKeyUp() => keyState & !IsKeyDown();

        private void CallKeyDown() => KeyDown?.Invoke(GetInputArgs(InputEventType.KeyDown));
        private void CallKeyUp() => KeyUp?.Invoke(GetInputArgs(InputEventType.KeyUp));

        private IInputArgs GetInputArgs(InputEventType eventType)
        {
            return new InputArgs() { ActionName = Name, Key = BindingKey, EventType = eventType };
        }

        private void UpdateState()
        {
            keyState = UnityEngine.Input.GetKey(BindingKey);
        }
    }
}
