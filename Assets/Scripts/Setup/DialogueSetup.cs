using UnityEngine;
using UnityEngine.UI;
using VNFramework.Interfaces.Dialogue;
using TMPro;

namespace Assets.Scripts.Setup
{
    public class DialogueSetup : MonoBehaviour
    {
        public TextMeshProUGUI SpeakerNameDisplay;
        public TextMeshProUGUI SpeechTextDisplay;
        public GameObject DialoguePanel;
        public static DialogueSetup Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }
    }
}
