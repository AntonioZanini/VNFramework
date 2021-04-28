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
        ChangeLinkColor(args.linkTextfirstCharacterIndex, args.linkFullText.Length, GetRadomColor());
    }
    
    private void OnLinkLeave(TextEventHandler.LinkArgs args)
    {
        ResetLinkColor(args.linkTextfirstCharacterIndex, args.linkFullText.Length);
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

    void ResetWordColor(TMP_WordInfo wInfo)
    {
        ChangeWordColor(wInfo, originalColor);
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
