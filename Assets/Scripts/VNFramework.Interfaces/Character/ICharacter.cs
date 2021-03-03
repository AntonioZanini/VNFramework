using UnityEngine;

namespace VNFramework.Interfaces.Character
{
    public interface ICharacter
    {
        string Name { get; set; }
        bool Enabled { get; set; }
        ICharacterManager CharacterManager { get; set; }
        RectTransform RootElement { get; }
        void Initialize(bool enabled);
    }
}
