using Assets.Scripts.Setup;
using Assets.Scripts.VNFramework.Core.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class InputTest : MonoBehaviour
    {
        private void Start()
        {
            var inputManager = GlobalSetup.Instance.InputManager;
            inputManager.AddMap("main");
            inputManager.AddActions("main", new List<DataInputAction>()
            {
                new DataInputAction("advanceDialog", actionBinding: "<Keyboard>/space"),
                new DataInputAction("changeExpression", actionBinding: "<Keyboard>/e")
            }
            );
            inputManager.Register("main", "advanceDialog", Test.Instance);
            inputManager.Register("main", "changeExpression", Test.Instance);
            inputManager.Enable("main");
        }
    }
}
