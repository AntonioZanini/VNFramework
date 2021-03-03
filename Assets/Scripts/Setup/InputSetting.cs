using Assets.Scripts.VNFramework.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class InputSetting: MonoBehaviour
    {
        public InputManager InputManager { get; set; }
        public static InputSetting Instance { get; private set; }
        void Awake() => Instance = this;

    }
}
