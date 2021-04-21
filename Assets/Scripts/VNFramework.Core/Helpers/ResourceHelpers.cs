using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VNFramework.Core.Helpers
{
    public static class ResourceHelpers
    {
        private static IEnumerable<string> characterSpriteFolder = new string[] { "Sprites" };
        private static IEnumerable<string> charactersFolder = new string[] { "Characters" };
        private static IEnumerable<string> fontsFolder = new string[] { "Fonts" };
        private static IEnumerable<string> scenesFolder = new string[] { "SceneScripts"};
        private static T LoadResource<T>(params string[] path) where T : Object
        {
            return Resources.Load<T>(CreatePath(path));
        }

        private static Object LoadResource(params string[] path)
        {
            return Resources.Load(CreatePath(path));
        }

        private static T[] LoadResources<T>(params string[] path) where T : Object
        {
            return Resources.LoadAll<T>(CreatePath(path));
        }

        private static string CreatePath(string[] path)
        {
            return Path.Combine(path);
        }

        private static Sprite[] LoadSprites(params string[] path)
        {
            return LoadResources<Sprite>(path);
        }

        public static Sprite[] LoadCharacterSprites(string characterName)
        {
            var path = new List<string>(characterSpriteFolder);
            path.Add(characterName);
            return LoadSprites(path.ToArray());
        }

        public static Sprite LoadCharacterSprite(string characterName, int index)
        {
            var sprites = LoadCharacterSprites(characterName);
            if (index < 0 || sprites.Length <= index) { return null; }
            
            return sprites[index];
        }

        public static Sprite LoadCharacterSingleSprite(string characterName, string characterFolder)
        {
            string pathFrag = characterName;
            if (!characterFolder.Equals(string.Empty)) { pathFrag = Path.Combine(characterFolder, characterName); }
            var sprites = LoadCharacterSprites(pathFrag);
            if (sprites.Length < 1) { return null; }
            return sprites[0];
        }

        public static Font LoadFont(params string[] path)
        {
            List<string> actualPath = new List<string>(fontsFolder);
            actualPath.AddRange(path);
            return LoadResource<Font>(actualPath.ToArray());
        }
        
        public static GameObject LoadPrefab(params string[] path)
        {
            return LoadResource<GameObject>(path);
        }

        public static GameObject LoadCharacterPrefab(string characterName)
        {
            var path = new List<string>(charactersFolder);
            path.Add(characterName);
            return LoadPrefab(path.ToArray());
        }
        public static string LoadScriptText(string scriptName)
        {
            var path = new List<string>(scenesFolder);
            path.Add(scriptName);
            return LoadResource<TextAsset>(path.ToArray()).text;
        }

    }
}