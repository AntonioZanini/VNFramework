using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VNFramework.Core.TMPro;

public class TextActions : MonoBehaviour
{
    private TMP_Text m_TextComponent;
    private Color32 originalColor;

    private TextEventHandler TextEvents;

    // Start is called before the first frame update
    void Start()
    {
        TextEvents = GetComponent<TextEventHandler>();

        TextEvents.onWordEnter.AddListener(OnWordEnter);
        TextEvents.onWordLeave.AddListener(OnWordLeave);
        TextEvents.onLinkEnter.AddListener(OnLinkEnter);
        TextEvents.onLinkLeave.AddListener(OnLinkLeave);
    }

    private void OnLinkEnter(TextEventHandler.LinkArgs args)
    {
        AddBold(args.linkRawTextfirstCharacterIndex, args.linkRawTextfirstCharacterIndex + args.linkRawText.Length, args.linkRawText, args.TextComponent);
        ChangeLinkColor(args.linkTextfirstCharacterIndex, args.linkText.Length, GetRadomColor());
    }
    
    private void OnLinkLeave(TextEventHandler.LinkArgs args)
    {
        RemoveBold(args.linkRawTextfirstCharacterIndex, args.linkRawTextfirstCharacterIndex + args.linkRawText.Length, args.linkRawText, args.TextComponent);
        ResetLinkColor(args.linkTextfirstCharacterIndex, args.linkText.Length);
    }

    private void OnWordEnter(TMP_WordInfo wordInfo, string word, int charIndex, int length)
    {
        //ChangeWordColor(wordInfo, GetRadomColor());
    }    

    private void OnWordLeave(TMP_WordInfo wordInfo, string word, int charIndex, int length)
    {
        //ResetWordColor(wordInfo);
    }

    private void Awake()
    {
        m_TextComponent = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color32 GetRadomColor()
    {
        return new Color32(
            (byte)System.Convert.ToInt32(Random.value * 255),
            (byte)System.Convert.ToInt32(Random.value * 255),
            (byte)System.Convert.ToInt32(Random.value * 255),
            255
            );
    }

    void AddBold(int startIndex, int endIndex, string rawText, TMP_Text textComponent)
    {
        if (rawText.Contains("<b>")) { return; }
        textComponent.text = textComponent.text.Substring(0, startIndex) +
                             "<b>" + rawText + "</b>" +
                             textComponent.text.Substring(endIndex);
    }   

    void RemoveBold(int startIndex, int endIndex, string rawText, TMP_Text textComponent)
    {
        if (!rawText.Contains("<b>") && !rawText.Contains("</b>")) { return; }
        textComponent.text = textComponent.text.Substring(0, startIndex) +
                             rawText.Replace("<b>", string.Empty).Replace("</b>", string.Empty) +
                             textComponent.text.Substring(endIndex);
    }

    void ResetLinkColor(int startIndex, int length)
    {
        ChangeLinkColor(startIndex, length, originalColor);
    }

    public void ChangeWordColor(TMP_WordInfo wInfo, Color32 color)
    {
        for (int i = 0; i < wInfo.characterCount; i++)
        {
            int characterIndex = wInfo.firstCharacterIndex + i;
            TMP_CharacterInfo cInfo = m_TextComponent.textInfo.characterInfo[characterIndex];
            ChangeLetterColor(m_TextComponent, cInfo, color);
        }

        // Update Geometry
        m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    public void ChangeLinkColor(int startIndex, int length, Color32 color)
    {
        for (int i = startIndex; i < startIndex + length; i++)
        {
            TMP_CharacterInfo cInfo = m_TextComponent.textInfo.characterInfo[i];
            ChangeLetterColor(m_TextComponent, cInfo, color);
        }

        // Update Geometry
        m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    public void ChangeLetterColor(TMP_Text textMeshPro, TMP_CharacterInfo cInfo, Color32 color)
    {
        // Get a reference to the vertex color
        Color32[] vertexColors = textMeshPro.textInfo.meshInfo[cInfo.materialReferenceIndex].colors32;

        originalColor = vertexColors[cInfo.vertexIndex + 0];

        vertexColors[cInfo.vertexIndex + 0] = color;
        vertexColors[cInfo.vertexIndex + 1] = color;
        vertexColors[cInfo.vertexIndex + 2] = color;
        vertexColors[cInfo.vertexIndex + 3] = color;
    }

}
