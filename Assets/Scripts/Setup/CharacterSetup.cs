using UnityEngine;
using UnityEngine.UI;
using VNFramework.Interfaces.Dialogue;
using TMPro;

namespace Assets.Scripts.Setup
{
    public class CharacterSetup : MonoBehaviour
    {
        public GameObject CharacterPanel;
        public RectTransform CharacterPanelRect => CharacterPanel.transform.GetComponent<RectTransform>();
        public static CharacterSetup Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }
    }
}
