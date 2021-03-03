using UnityEngine;
using UnityEngine.UI;
using VNFramework.Core.Helpers;
using VNFramework.Interfaces.Character;

namespace VNFramework.Core.Character
{
    public class SingleLayerCharacter : ICharacter, ISingleLayerRenderedActor, IMobileActor
    {
        private bool isInitialized;
        private readonly string renderLayerName = "MainLayer";

        private IMoveTransition MoveTransition { get; set; }

        public string Name { get; set; }
        public ICharacterManager CharacterManager { get; set; }
        public RectTransform RootElement { get; protected set;}
        public ISingleLayerRenderer Renderer { get; protected set; }
        public bool Enabled {
            get { return RootElement.gameObject.activeInHierarchy; }
            set {
                if (RootElement != null) { RootElement.gameObject.SetActive(value); }
            }
        }

        public SingleLayerCharacter()
        {
            isInitialized = false;
        }

        public SingleLayerCharacter(ICharacterManager characterManager, string name, bool enabled)
        {
            Name = name;
            CharacterManager = characterManager;
            Initialize(enabled);
        }

        public void Initialize(bool enabled = true)
        {
            if (!isInitialized)
            {
                GameObject characterPrefab = ResourceHelpers.LoadCharacterPrefab($"Character[{Name}]");
                GameObject characterObject = CharacterManager.AddCharacterObject(characterPrefab);
                RootElement = characterObject.GetComponent<RectTransform>();

                Image renderObject = RootElement.transform.Find(renderLayerName).GetComponentInChildren<Image>();
                Renderer = new SingleLayerRenderer(new RenderLayer() { CurrentRenderer = renderObject });

                MoveTransition = new MoveTransition() { ElementRect = RootElement };

                Enabled = enabled;
                isInitialized = true;
            }
        }

        public void MoveTo(Vector2 target, float speed, bool smooth = true)
        {
            MoveTransition.MoveTo(target, speed, smooth);
        }

        public void StopMoving(bool moveToTarget = false)
        {
            MoveTransition.StopMoving(moveToTarget);
        }

    }
}
