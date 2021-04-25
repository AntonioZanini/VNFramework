using System;
using System.Collections.Generic;
using UnityEngine;
using VNFramework.Interfaces.Character;

namespace VNFramework.Core.Character
{
    public class CharacterManager : ICharacterManager
    {
        private Dictionary<string, ICharacter> characters = new Dictionary<string, ICharacter>();

        public RectTransform CharacterPanel { get; set; }

        public ICharacter AddCharacter<T>(string characterName, bool enabled = true) where T : ICharacter
        {
            T character = Activator.CreateInstance<T>();
            character.Name = characterName;
            character.Enabled = enabled;
            character.CharacterManager = this;
            characters.Add(character.Name, character);
            return character;
        }

        public GameObject AddCharacterObject(GameObject characterPrefab)
        {
            return GameObject.Instantiate(characterPrefab, CharacterPanel);
        }

        public ICharacter GetCharacter<T>(string characterName, bool addNewIfNotExists = false, bool enabled = true) where T : ICharacter
        {
            if (characters.ContainsKey(characterName)) { return characters[characterName]; }
            else if (addNewIfNotExists) { return AddCharacter<T>(characterName, enabled); }
            return null;
        }
    }
}
