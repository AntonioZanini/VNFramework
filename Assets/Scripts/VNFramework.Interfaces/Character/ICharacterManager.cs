using UnityEngine;

namespace VNFramework.Interfaces.Character
{
    public interface ICharacterManager
    {
        RectTransform CharacterPanel { get; set; }
        ICharacter GetCharacter<T>(string characterName, bool addNewIfNotExists = false, bool enabled = true) where T : ICharacter;
        ICharacter AddCharacter<T>(string characterName, bool enabled = true) where T : ICharacter;
        GameObject AddCharacterObject(GameObject characterPrefab);
    }
}
