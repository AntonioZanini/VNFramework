using Assets.Scripts.Setup;
//using Assets.Scripts.VNFramework.Core.Input;
using System.Collections.Generic;
using UnityEngine;
using VNFramework.Core.Input;
using VNFramework.Interfaces.Input;
//using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class InputTest : MonoBehaviour
    {
        private void Start()
        {
            var inputManager = GlobalSetup.Instance.InputManager;
            inputManager.AddMap("main");
            inputManager.AddActions("main", new List<IInputAction>()
            {
                new InputAction("advanceDialog", KeyCode.Space),
                new InputAction("changeExpression", KeyCode.E)
            }
            );
            inputManager.Register("main", "advanceDialog", Test.Instance);
            inputManager.Register("main", "changeExpression", Test.Instance);
            inputManager.Start();
        }
    }
}
