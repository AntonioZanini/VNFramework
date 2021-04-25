using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestLink : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{

    private TextMeshProUGUI m_TextMeshPro;
    private Canvas m_Canvas;
    private Camera m_Camera;
    private bool isHoveringObject;
    void Awake()
    {
        m_TextMeshPro = gameObject.GetComponent<TextMeshProUGUI>();


        m_Canvas = gameObject.GetComponentInParent<Canvas>();

        // Get a reference to the camera if Canvas Render Mode is not ScreenSpace Overlay.
        if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            m_Camera = m_Canvas.GetComponent<Camera>();
        else
            m_Camera = m_Canvas.worldCamera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, Input.mousePosition, m_Camera);
        Debug.Log("Click: ");
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = m_TextMeshPro.textInfo.linkInfo[linkIndex];
            Debug.Log(linkInfo.GetLinkID());
            Debug.Log(linkInfo.GetLinkText());
        }
        else
        {
            int charIndex = TMP_TextUtilities.FindIntersectingCharacter(m_TextMeshPro, Input.mousePosition, m_Camera, true);

            if (charIndex != -1)
            {
                TMP_CharacterInfo cInfo = m_TextMeshPro.textInfo.characterInfo[charIndex];
                ChangeLetterColor(m_TextMeshPro, cInfo, GetRadomColor());
                m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            }
        }
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

    public int GetLinkIndex() => TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, Input.mousePosition, m_Camera);
    public bool IsMouseOnLink() => (GetLinkIndex() != -1);
    public bool IsMouseOnLink(string id)
    {
        int index = GetLinkIndex();
        if (index == -1) { return false; }

        TMP_LinkInfo linkInfo = m_TextMeshPro.textInfo.linkInfo[index];
        return linkInfo.GetLinkID().ToUpper().Equals(id.ToUpper());
    }

    private TMP_MeshInfo[] m_cachedMeshInfoVertexData;

    void RestoreCachedVertexAttributes(int index)
    {
        if (index == -1 || index > m_TextMeshPro.textInfo.characterCount - 1) return;

        // Get the index of the material / sub text object used by this character.
        int materialIndex = m_TextMeshPro.textInfo.characterInfo[index].materialReferenceIndex;

        // Get the index of the first vertex of the selected character.
        int vertexIndex = m_TextMeshPro.textInfo.characterInfo[index].vertexIndex;

        // Restore Vertices
        // Get a reference to the cached / original vertices.
        Vector3[] src_vertices = m_cachedMeshInfoVertexData[materialIndex].vertices;

        // Get a reference to the vertices that we need to replace.
        Vector3[] dst_vertices = m_TextMeshPro.textInfo.meshInfo[materialIndex].vertices;

        // Restore / Copy vertices from source to destination
        dst_vertices[vertexIndex + 0] = src_vertices[vertexIndex + 0];
        dst_vertices[vertexIndex + 1] = src_vertices[vertexIndex + 1];
        dst_vertices[vertexIndex + 2] = src_vertices[vertexIndex + 2];
        dst_vertices[vertexIndex + 3] = src_vertices[vertexIndex + 3];

        // Restore Vertex Colors
        // Get a reference to the vertex colors we need to replace.
        Color32[] dst_colors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;

        // Get a reference to the cached / original vertex colors.
        Color32[] src_colors = m_cachedMeshInfoVertexData[materialIndex].colors32;

        // Copy the vertex colors from source to destination.
        dst_colors[vertexIndex + 0] = src_colors[vertexIndex + 0];
        dst_colors[vertexIndex + 1] = src_colors[vertexIndex + 1];
        dst_colors[vertexIndex + 2] = src_colors[vertexIndex + 2];
        dst_colors[vertexIndex + 3] = src_colors[vertexIndex + 3];

        // Restore UV0S
        // UVS0
        Vector2[] src_uv0s = m_cachedMeshInfoVertexData[materialIndex].uvs0;
        Vector2[] dst_uv0s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs0;
        dst_uv0s[vertexIndex + 0] = src_uv0s[vertexIndex + 0];
        dst_uv0s[vertexIndex + 1] = src_uv0s[vertexIndex + 1];
        dst_uv0s[vertexIndex + 2] = src_uv0s[vertexIndex + 2];
        dst_uv0s[vertexIndex + 3] = src_uv0s[vertexIndex + 3];

        // UVS2
        Vector2[] src_uv2s = m_cachedMeshInfoVertexData[materialIndex].uvs2;
        Vector2[] dst_uv2s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs2;
        dst_uv2s[vertexIndex + 0] = src_uv2s[vertexIndex + 0];
        dst_uv2s[vertexIndex + 1] = src_uv2s[vertexIndex + 1];
        dst_uv2s[vertexIndex + 2] = src_uv2s[vertexIndex + 2];
        dst_uv2s[vertexIndex + 3] = src_uv2s[vertexIndex + 3];


        // Restore last vertex attribute as we swapped it as well
        int lastIndex = (src_vertices.Length / 4 - 1) * 4;

        // Vertices
        dst_vertices[lastIndex + 0] = src_vertices[lastIndex + 0];
        dst_vertices[lastIndex + 1] = src_vertices[lastIndex + 1];
        dst_vertices[lastIndex + 2] = src_vertices[lastIndex + 2];
        dst_vertices[lastIndex + 3] = src_vertices[lastIndex + 3];

        // Vertex Colors
        src_colors = m_cachedMeshInfoVertexData[materialIndex].colors32;
        dst_colors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;
        dst_colors[lastIndex + 0] = src_colors[lastIndex + 0];
        dst_colors[lastIndex + 1] = src_colors[lastIndex + 1];
        dst_colors[lastIndex + 2] = src_colors[lastIndex + 2];
        dst_colors[lastIndex + 3] = src_colors[lastIndex + 3];

        // UVS0
        src_uv0s = m_cachedMeshInfoVertexData[materialIndex].uvs0;
        dst_uv0s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs0;
        dst_uv0s[lastIndex + 0] = src_uv0s[lastIndex + 0];
        dst_uv0s[lastIndex + 1] = src_uv0s[lastIndex + 1];
        dst_uv0s[lastIndex + 2] = src_uv0s[lastIndex + 2];
        dst_uv0s[lastIndex + 3] = src_uv0s[lastIndex + 3];

        // UVS2
        src_uv2s = m_cachedMeshInfoVertexData[materialIndex].uvs2;
        dst_uv2s = m_TextMeshPro.textInfo.meshInfo[materialIndex].uvs2;
        dst_uv2s[lastIndex + 0] = src_uv2s[lastIndex + 0];
        dst_uv2s[lastIndex + 1] = src_uv2s[lastIndex + 1];
        dst_uv2s[lastIndex + 2] = src_uv2s[lastIndex + 2];
        dst_uv2s[lastIndex + 3] = src_uv2s[lastIndex + 3];

        // Need to update the appropriate 
        m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }


    private int m_selectedWord = -1;
    private int m_lastIndex = -1;
    private Matrix4x4 m_matrix;
    private Color32 originalColor;

    void OnEnable()
    {
        // Subscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    void OnDisable()
    {
        // UnSubscribe to event fired when text object has been regenerated.
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }


    void ON_TEXT_CHANGED(Object obj)
    {
        if (obj == m_TextMeshPro)
        {
            // Update cached vertex data.
            m_cachedMeshInfoVertexData = m_TextMeshPro.textInfo.CopyMeshInfoVertexData();
        }
    }

    void LateUpdate()
    {
        if (isHoveringObject)
        {
            int charIndex = TMP_TextUtilities.FindIntersectingCharacter(m_TextMeshPro, Input.mousePosition, m_Camera, true);

            // Undo Swap and Vertex Attribute changes.
            if (charIndex == -1 || charIndex != m_lastIndex)
            {
                RestoreCachedVertexAttributes(m_lastIndex);
                m_lastIndex = -1;
            }

            if (charIndex != -1 && charIndex != m_lastIndex && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                m_lastIndex = charIndex;

                // Get the index of the material / sub text object used by this character.
                int materialIndex = m_TextMeshPro.textInfo.characterInfo[charIndex].materialReferenceIndex;

                // Get the index of the first vertex of the selected character.
                int vertexIndex = m_TextMeshPro.textInfo.characterInfo[charIndex].vertexIndex;

                // Get a reference to the vertices array.
                Vector3[] vertices = m_TextMeshPro.textInfo.meshInfo[materialIndex].vertices;

                // Determine the center point of the character.
                Vector2 charMidBasline = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) / 2;

                // Need to translate all 4 vertices of the character to aligned with middle of character / baseline.
                // This is needed so the matrix TRS is applied at the origin for each character.
                Vector3 offset = charMidBasline;

                // Translate the character to the middle baseline.
                vertices[vertexIndex + 0] = vertices[vertexIndex + 0] - offset;
                vertices[vertexIndex + 1] = vertices[vertexIndex + 1] - offset;
                vertices[vertexIndex + 2] = vertices[vertexIndex + 2] - offset;
                vertices[vertexIndex + 3] = vertices[vertexIndex + 3] - offset;

                float zoomFactor = 1.5f;

                // Setup the Matrix for the scale change.
                m_matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * zoomFactor);

                // Apply Matrix operation on the given character.
                vertices[vertexIndex + 0] = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 0]);
                vertices[vertexIndex + 1] = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 1]);
                vertices[vertexIndex + 2] = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 2]);
                vertices[vertexIndex + 3] = m_matrix.MultiplyPoint3x4(vertices[vertexIndex + 3]);

                // Translate the character back to its original position.
                vertices[vertexIndex + 0] = vertices[vertexIndex + 0] + offset;
                vertices[vertexIndex + 1] = vertices[vertexIndex + 1] + offset;
                vertices[vertexIndex + 2] = vertices[vertexIndex + 2] + offset;
                vertices[vertexIndex + 3] = vertices[vertexIndex + 3] + offset;

                // Change Vertex Colors of the highlighted character
                Color32 c = new Color32(255, 255, 192, 255);

                // Get a reference to the vertex color
                Color32[] vertexColors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;

                vertexColors[vertexIndex + 0] = c;
                vertexColors[vertexIndex + 1] = c;
                vertexColors[vertexIndex + 2] = c;
                vertexColors[vertexIndex + 3] = c;


                // Get a reference to the meshInfo of the selected character.
                TMP_MeshInfo meshInfo = m_TextMeshPro.textInfo.meshInfo[materialIndex];

                // Get the index of the last character's vertex attributes.
                int lastVertexIndex = vertices.Length - 4;

                // Swap the current character's vertex attributes with those of the last element in the vertex attribute arrays.
                // We do this to make sure this character is rendered last and over other characters.
                meshInfo.SwapVertexData(vertexIndex, lastVertexIndex);

                // Need to update the appropriate 
                m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            }
        }
        else
        {
            // Restore any character that may have been modified
            if (m_lastIndex != -1)
            {
                RestoreCachedVertexAttributes(m_lastIndex);
                m_lastIndex = -1;
            }
        }


        //Check if Mouse intersects any words and if so assign a random color to that word.
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(m_TextMeshPro, Input.mousePosition, m_Camera);

        // Clear previous word selection.
        if ( m_selectedWord != -1 && (wordIndex == -1 || wordIndex != m_selectedWord))
        {
            TMP_WordInfo wInfo = m_TextMeshPro.textInfo.wordInfo[m_selectedWord];

            ChangeWordColor(m_TextMeshPro, wInfo, originalColor);

            m_selectedWord = -1;
        }

        if (isHoveringObject)
        {
            // Word Selection Handling
            if (wordIndex != -1 && wordIndex != m_selectedWord && IsMouseOnLink("teste"))
            {
                m_selectedWord = wordIndex;

                TMP_WordInfo wInfo = m_TextMeshPro.textInfo.wordInfo[wordIndex];

                ChangeWordColor(m_TextMeshPro, wInfo, new Color32(255, 0, 0, 255));
            }
        }
    }

    public void ChangeWordColor(TextMeshProUGUI textMeshPro, TMP_WordInfo wInfo, Color32 color)
    {
        for (int i = 0; i < wInfo.characterCount; i++)
        {
            int characterIndex = wInfo.firstCharacterIndex + i;
            TMP_CharacterInfo cInfo = textMeshPro.textInfo.characterInfo[characterIndex];
            ChangeLetterColor(textMeshPro, cInfo, color);
        }

        // Update Geometry
        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    public void ChangeLetterColor(TextMeshProUGUI textMeshPro, TMP_CharacterInfo cInfo, Color32 color)
    {
        // Get a reference to the vertex color
        Color32[] vertexColors = textMeshPro.textInfo.meshInfo[cInfo.materialReferenceIndex].colors32;

        originalColor = vertexColors[cInfo.vertexIndex + 0];

        vertexColors[cInfo.vertexIndex + 0] = color;
        vertexColors[cInfo.vertexIndex + 1] = color;
        vertexColors[cInfo.vertexIndex + 2] = color;
        vertexColors[cInfo.vertexIndex + 3] = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter()");
        isHoveringObject = true;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit()");
        isHoveringObject = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(eventData);
    }
}
    //    void OnMouseOver()
    //    {
    //        //If your mouse hovers over the GameObject with the script attached, output this message
    //        Debug.Log("Mouse is over GameObject.");
    //    }

    //    void OnMouseExit()
    //    {
    //        //The mouse is no longer hovering over the GameObject so output this message each frame
    //        Debug.Log("Mouse is no longer on GameObject.");
    //    }
    //    public TextMeshProUGUI pTextMeshPro;
    //    public Camera pCamera;

    //    public void OnMouseDown()
    //    {

    //        int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, Input.mousePosition, pCamera);
    //        if (linkIndex != -1)
    //        { // was a link clicked?
    //            TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];

    //        }
    //    }

