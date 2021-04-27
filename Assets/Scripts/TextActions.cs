using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VNFramework.Core.TMPro;

public class TextActions : MonoBehaviour
{
    private TMP_Text m_TextComponent;
    private Color32 originalColor;

    // Start is called before the first frame update
    void Start()
    {
        //TextEventHandler.Instance.onLinkEnter += OnLinkEnter;
        //TextEventHandler.Instance.onLinkLeave += OnLinkLeave;
        TextEventHandler.Instance.onWordEnter.AddListener(OnWordEnter);
        TextEventHandler.Instance.onWordLeave.AddListener(OnWordLeave);
    }

    private void OnWordEnter(TMP_WordInfo wordInfo, string word, int charIndex, int length)
    {
        ChangeWordColor(wordInfo, GetRadomColor());
    }    

    private void OnWordLeave(TMP_WordInfo wordInfo, string word, int charIndex, int length)
    {
        ResetWordColor(wordInfo);
    }

    private void Awake()
    {
        m_TextComponent = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLinkEnter(TMP_LinkInfo linkInfo, string linkID, string linkText, int linkIndex)
    {


    }
    private void OnLinkLeave(TMP_LinkInfo linkInfo, string linkID, string linkText, int linkIndex)
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
