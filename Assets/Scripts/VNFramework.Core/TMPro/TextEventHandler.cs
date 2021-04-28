using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using TMPro;

namespace VNFramework.Core.TMPro
{

    public class TextEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isMouseOver;

        public class LinkArgs
        {
            private TMP_LinkInfo linkInfo;
            private string fullText;
            public string linkId => linkInfo.GetLinkID();
            public string linkText => linkInfo.GetLinkText();
            public string linkFullText => fullText;
            public int linkIdFirstCharacterIndex => linkInfo.linkIdFirstCharacterIndex;
            public int linkTextfirstCharacterIndex => linkInfo.linkIdFirstCharacterIndex + linkInfo.linkIdLength + 2;

            public LinkArgs(TMP_LinkInfo info)
            {
                linkInfo = info;
                int actualLength = info.textComponent.text.Substring(linkTextfirstCharacterIndex).IndexOf("</link>");
                fullText = info.textComponent.text.Substring(linkTextfirstCharacterIndex, actualLength);
            }
        }

        [Serializable]
        public class LinkEvent : UnityEvent<LinkArgs> { }

        [Serializable]
        public class CharacterEnterEvent : UnityEvent<char, int> { }

        [Serializable]
        public class CharacterLeaveEvent : UnityEvent<char, int> { }

        [Serializable]
        public class SpriteEnterEvent : UnityEvent<char, int> { }

        [Serializable]
        public class SpriteLeaveEvent : UnityEvent<char, int> { }

        [Serializable]
        public class WordEvent : UnityEvent<TMP_WordInfo, string, int, int> { }

        [Serializable]
        public class WordEnterEvent : UnityEvent<string, int, int> { }

        [Serializable]
        public class WordLeaveEvent : UnityEvent<string, int, int> { }

        [Serializable]
        public class LineEnterEvent : UnityEvent<string, int, int> { }

        [Serializable]
        public class LineLeaveEvent : UnityEvent<string, int, int> { }

        public CharacterEnterEvent onCharacterEnter { get; set; }
        public SpriteEnterEvent onSpriteEnter { get; set; }
        public WordEvent onWordEnter { get; set; }
        public WordEvent onWordLeave { get; set; }
        public LineEnterEvent onLineEnter { get; set; }
        public LineLeaveEvent onLineLeave { get; set; }

        public LinkEvent onLinkEnter { get; set; }
        public LinkEvent onLinkLeave { get; set; }
        public CharacterLeaveEvent onCharacterLeave { get; set; }
        public SpriteLeaveEvent onSpriteLeave { get; set; }

        private TMP_Text m_TextComponent;

        private Camera m_Camera;
        private Canvas m_Canvas;

        private int m_selectedLink = -1;
        private int m_lastCharIndex = -1;
        private int m_lastWordIndex = -1;
        private int m_lastLineIndex = -1;

        public TextEventHandler()
        {
            onWordEnter = new WordEvent();
            onWordLeave = new WordEvent();
            onLinkEnter = new LinkEvent();
            onLinkLeave = new LinkEvent();
        }

        void Awake()
        {
            // Get a reference to the text component.
            m_TextComponent = gameObject.GetComponent<TMP_Text>();

            // Get a reference to the camera rendering the text taking into consideration the text component type.
            if (m_TextComponent.GetType() == typeof(TextMeshProUGUI))
            {
                m_Canvas = gameObject.GetComponentInParent<Canvas>();
                if (m_Canvas != null)
                {
                    if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                        m_Camera = null;
                    else
                        m_Camera = m_Canvas.worldCamera;
                }
            }
            else
            {
                m_Camera = Camera.main;
            }
        }


        void LateUpdate()
        {
            if (!isMouseOver)
            {
                if (m_lastCharIndex != -1)
                {
                    SendCharacterLeaveEvent();
                    m_lastCharIndex = -1;
                }
                if (m_lastWordIndex != -1)
                {
                    var lastWInfo = m_TextComponent.textInfo.wordInfo[m_lastWordIndex];
                    SendOnWordLeave(lastWInfo, lastWInfo.GetWord(), lastWInfo.firstCharacterIndex, lastWInfo.characterCount);
                    m_lastWordIndex = -1;
                }
                if (m_lastLineIndex != -1)
                {
                    TMP_LineInfo lastLineInfo = m_TextComponent.textInfo.lineInfo[m_lastLineIndex];
                    SendOnLineLeave(GetLineText(lastLineInfo), lastLineInfo.firstCharacterIndex, lastLineInfo.characterCount);
                    m_lastLineIndex = -1;
                }
                if (m_selectedLink != -1)
                {
                    TMP_LinkInfo lastLinkInfo = m_TextComponent.textInfo.linkInfo[m_selectedLink];
                    SendOnLinkLeave(new LinkArgs(lastLinkInfo));
                    m_selectedLink = -1;
                }
                return;
            }

            WatchCharacterEvents();
            WatchWordEvents();
            WatchLineEvents();
            WatchLinkEvents();
        }

        private void WatchLinkEvents()
        {
            // Check if mouse intersects with any links.
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextComponent, UnityEngine.Input.mousePosition, m_Camera);

            // Handle new Link selection.
            if (linkIndex != -1 && linkIndex != m_selectedLink)
            {
                if (m_selectedLink != -1)
                {
                    TMP_LinkInfo lastLinkInfo = m_TextComponent.textInfo.linkInfo[m_selectedLink];
                    SendOnLinkLeave(new LinkArgs(lastLinkInfo));
                }

                m_selectedLink = linkIndex;
                TMP_LinkInfo linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];

                SendOnLinkEnter(new LinkArgs(linkInfo));
            }
            else if (m_selectedLink != -1 && linkIndex == -1)
            {
                TMP_LinkInfo lastLinkInfo = m_TextComponent.textInfo.linkInfo[m_selectedLink];
                SendOnLinkLeave(new LinkArgs(lastLinkInfo));
                m_selectedLink = -1;
            }
        }

        private void WatchLineEvents()
        {
            int lineIndex = TMP_TextUtilities.FindIntersectingLine(m_TextComponent, UnityEngine.Input.mousePosition, m_Camera);
            if (lineIndex != -1 && lineIndex != m_lastLineIndex)
            {
                if (m_lastLineIndex != -1)
                {
                    TMP_LineInfo lastLineInfo = m_TextComponent.textInfo.lineInfo[m_lastLineIndex];
                    SendOnLineLeave(GetLineText(lastLineInfo), lastLineInfo.firstCharacterIndex, lastLineInfo.characterCount);
                }
                m_lastLineIndex = lineIndex;

                // Get the information about the selected word.
                TMP_LineInfo lineInfo = m_TextComponent.textInfo.lineInfo[lineIndex];

                SendOnLineEnter(GetLineText(lineInfo), lineInfo.firstCharacterIndex, lineInfo.characterCount);
            }
            else if (m_lastLineIndex != -1 && lineIndex == -1)
            {
                TMP_LineInfo lastLineInfo = m_TextComponent.textInfo.lineInfo[m_lastLineIndex];
                SendOnLineLeave(GetLineText(lastLineInfo), lastLineInfo.firstCharacterIndex, lastLineInfo.characterCount);
                m_lastLineIndex = -1;
            }
        }

        private void WatchWordEvents()
        {
            int wordIndex = TMP_TextUtilities.FindIntersectingWord(m_TextComponent, UnityEngine.Input.mousePosition, m_Camera);
            if (wordIndex != -1 && wordIndex != m_lastWordIndex)
            {
                if (m_lastWordIndex != -1)
                {
                    TMP_WordInfo lastWInfo = m_TextComponent.textInfo.wordInfo[m_lastWordIndex];
                    SendOnWordLeave(lastWInfo, lastWInfo.GetWord(), lastWInfo.firstCharacterIndex, lastWInfo.characterCount);
                }

                m_lastWordIndex = wordIndex;
                TMP_WordInfo wInfo = m_TextComponent.textInfo.wordInfo[wordIndex];

                SendOnWordEnter(wInfo, wInfo.GetWord(), wInfo.firstCharacterIndex, wInfo.characterCount);
            }
            else if (m_lastWordIndex != -1 && wordIndex == -1)
            {
                TMP_WordInfo lastWInfo = m_TextComponent.textInfo.wordInfo[m_lastWordIndex];
                SendOnWordLeave(lastWInfo, lastWInfo.GetWord(), lastWInfo.firstCharacterIndex, lastWInfo.characterCount);
                m_lastWordIndex = -1;
            }
        }

        private void WatchCharacterEvents()
        {
            int charIndex = TMP_TextUtilities.FindIntersectingCharacter(m_TextComponent, UnityEngine.Input.mousePosition, m_Camera, true);
            if (charIndex != -1 && charIndex != m_lastCharIndex)
            {
                if (m_lastCharIndex != -1) { SendCharacterLeaveEvent(); }

                m_lastCharIndex = charIndex;

                if (IsCharACharacter(charIndex))
                    SendOnCharacterEnter(m_TextComponent.textInfo.characterInfo[charIndex].character, charIndex);
                else if (IsCharASprite(charIndex))
                    SendOnSpriteEnter(m_TextComponent.textInfo.characterInfo[charIndex].character, charIndex);
            }
            else if (m_lastCharIndex != -1 && charIndex == -1)
            {
                SendCharacterLeaveEvent();
                m_lastCharIndex = -1;
            }

        }

        private void SendCharacterLeaveEvent()
        {
            if (IsCharACharacter(m_lastCharIndex))
                SendOnCharacterLeave(m_TextComponent.textInfo.characterInfo[m_lastCharIndex].character, m_lastCharIndex);
            else if (IsCharASprite(m_lastCharIndex))
                SendOnSpriteLeave(m_TextComponent.textInfo.characterInfo[m_lastCharIndex].character, m_lastCharIndex);
        }

        private string GetLineText(TMP_LineInfo lineInfo)
        {
            char[] buffer = new char[lineInfo.characterCount];
            for (int i = 0; i < lineInfo.characterCount && i < m_TextComponent.textInfo.characterInfo.Length; i++)
            {
                buffer[i] = m_TextComponent.textInfo.characterInfo[i + lineInfo.firstCharacterIndex].character;
            }
            return new string(buffer);
        }

        private bool IsCharACharacter(int charIndex) => m_TextComponent.textInfo.characterInfo[charIndex].elementType == TMP_TextElementType.Character;
        private bool IsCharASprite(int charIndex) => m_TextComponent.textInfo.characterInfo[charIndex].elementType == TMP_TextElementType.Sprite;

        public void OnPointerEnter(PointerEventData eventData) => isMouseOver = true;

        public void OnPointerExit(PointerEventData eventData) => isMouseOver = false;

        private void SendOnCharacterLeave(char character, int characterIndex) => onCharacterLeave?.Invoke(character, characterIndex);

        private void SendOnCharacterEnter(char character, int characterIndex) => onCharacterEnter?.Invoke(character, characterIndex);

        private void SendOnSpriteLeave(char character, int characterIndex) => onSpriteLeave?.Invoke(character, characterIndex);

        private void SendOnSpriteEnter(char character, int characterIndex) => onSpriteEnter?.Invoke(character, characterIndex);

        private void SendOnWordEnter(TMP_WordInfo wordInfo, string word, int charIndex, int length) 
            => onWordEnter?.Invoke(wordInfo, word, charIndex, length);
        private void SendOnWordLeave(TMP_WordInfo wordInfo, string word, int charIndex, int length)
            => onWordLeave?.Invoke(wordInfo, word, charIndex, length);

        private void SendOnLineEnter(string line, int charIndex, int length) => onLineEnter?.Invoke(line, charIndex, length);

        private void SendOnLineLeave(string line, int charIndex, int length) => onLineLeave?.Invoke(line, charIndex, length);

        private void SendOnLinkEnter(LinkArgs args)
            => onLinkEnter?.Invoke(args);

        private void SendOnLinkLeave(LinkArgs args)
            => onLinkLeave?.Invoke(args);

    }
}