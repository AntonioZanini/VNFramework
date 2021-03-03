using Assets.Scripts.VNFramework.Core.Input;
using UnityEngine;
using VNFramework.Core.Settings;
using VNFramework.Interfaces.Global;
using VNFramework.Interfaces.Input;

namespace Assets.Scripts.Setup
{
    public class GlobalSetup : MonoBehaviour, IConfiguration
    {
        public static GlobalSetup Instance { get; private set; }

        public MonoBehaviour GameObject => Instance;

        public IInputManager InputManager { get; private set; }

        void Awake()
        {
            Instance = this;
            Configurations.GlobalConfiguration = this;
            
        }

        public GlobalSetup()
        {
            InputManager = new InputManager();
        }
    }
}
