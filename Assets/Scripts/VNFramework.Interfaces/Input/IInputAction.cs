using System;
using UnityEngine;

namespace VNFramework.Interfaces.Input
{
    public interface IInputAction
    {
        string Name { get; }
        bool Enabled { get; set; }
        KeyCode BindingKey { get; set; }
        void Update();
        event Action<IInputArgs> KeyDown;
        event Action<IInputArgs> KeyUp;
    }
}
