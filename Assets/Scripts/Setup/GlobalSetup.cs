using Assets.Scripts.VNFramework.Core.Input;
using UnityEngine;
using VNFramework.Core.Settings;
using VNFramework.Interfaces.Dialogue;
using VNFramework.Interfaces.Global;
using VNFramework.Interfaces.Input;

namespace Assets.Scripts.Setup
{
    public class GlobalSetup : MonoBehaviour, IConfiguration
    {
        public static GlobalSetup Instance { get; private set; }

        public MonoBehaviour GameObject => Instance;
        public IInputManager InputManager { get; private set; }
        public IDialogueSystem DialogueSystem { get; set; }
        public ICoroutineAccessor CoroutineAccessor { get; private set; }

        void Awake()
        {
            Instance = this;
            Configurations.GlobalConfiguration = this;
            CoroutineAccessor = new CoroutineAccessor();
        }

        public GlobalSetup()
        {
            InputManager = new InputManager();
        }
    }
}
