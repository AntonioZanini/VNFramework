using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VNFramework.Core.Dialogue;
using Assets.Scripts.Setup;
using VNFramework.Core.Character;
using VNFramework.Interfaces.Character;
using VNFramework.Core.Helpers;
using System;
using VNFramework.Interfaces.Scene;
using VNFramework.Core.Scene;
using VNFramework.Core.Graphic;
using VNFramework.Interfaces.Input;

public class Test : MonoBehaviour, IInputHandler
{
    DialogueSystem dialogueSystem;
    ICharacterManager characterManager;
    SingleLayerCharacter character;
    IScripter scripter;
    // Start is called before the first frame update

    public static Test Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        dialogueSystem = new DialogueSystem()
        {
            Elements = new DialogueSystemElements()
            {
                DefaultSpeechSettings = new SpeechSettings()
                {
                    DisplaySpeed = 5f,
                    FontSettings = new FontSettings() { 
                        FontAsset = ResourceHelpers.LoadFontAsset("OpenSans SDF"),
                        FontMaterial = ResourceHelpers.LoadMaterial("Fonts", "OpenSans SDF Presset")
                    }, 
                    FontColor = Color.cyan,
                    FontSize = 45,
                    FontOnSpeakerName = false
                },
                DialoguePanel = DialogueSetup.Instance.DialoguePanel,
                SpeakerNameDisplay = DialogueSetup.Instance.SpeakerNameDisplay,
                SpeechTextDisplay = DialogueSetup.Instance.SpeechTextDisplay
            }
            
        };
        GlobalSetup.Instance.DialogueSystem = dialogueSystem;

        characterManager = new CharacterManager()
        {
            CharacterPanel = CharacterSetup.Instance.CharacterPanelRect
        };
        character = (SingleLayerCharacter)characterManager.AddCharacter<SingleLayerCharacter>("Miu", true);
        character.Initialize(false);
        character.Renderer.SetSprite(GetSprite("neutral"));//Transition(ResourceHelpers.LoadCharacterSingleSprite("Miu"), 1f, false);
        character.Enabled = true;

        scripter = new Scripter();
        scripter.CommandFactory = new CommandFactory();
        scripter.Initialize(new ScriptText());
    }
    int indexSpeech = 0;
    int indexSprite = 0;
    Color[] colors = new Color[] { Color.white, Color.black, Color.red, Color.yellow, Color.grey, Color.gray, Color.clear };
    string[] sprites = new string[] { "neutral", "sad", "surprised", "pout", "catsmile" };


    // Update is called once per frame
    void Update()
    {
        
    }

    Sprite GetSprite(string expression)
    {
        return ResourceHelpers.LoadCharacterSingleSprite($"{character.Name.ToLower()}_{expression}", "Miu");
    }

    public void HandleInput(IInputArgs args)
    {
        switch (args.ActionName)
        {
            case "advanceDialog":
                AdvanceDialog();
                break;
            case "changeExpression":
                ChangeExpression();
                break;
            default:
                break;
        }
    }

    private void AdvanceDialog()
    {
        //indexSpeech++;
        //indexSpeech = indexSpeech >= colors.Length ? 0 : indexSpeech;
        //Say();
        scripter.Execute();
    }

    private void Say()
    {
        var settings = dialogueSystem.Elements.DefaultSpeechSettings.Clone();
        settings.FontColor = colors[indexSpeech];
        dialogueSystem.Say(new Speech()
        {
            AdditiveSpeech = false,
            SpeakerName = "Miu",
            SpeechSettings = settings,
            SpeechText = "Olá!"
        });
    }

    private void ChangeExpression()
    {
        character.Renderer.Transition(GetSprite(sprites[indexSprite]), 3f, false);
        indexSprite++;
        indexSprite = indexSprite >= sprites.Length ? 0 : indexSprite;
    }
}
