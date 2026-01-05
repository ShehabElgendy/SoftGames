using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// UI component for individual dialogue box.
    /// Attach to dialogue box prefab root.
    /// </summary>
    public class DialogueBoxUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Image avatarImage;
        [SerializeField] private HorizontalLayoutGroup layoutGroup;
        [SerializeField] private RectTransform boxRect;
        [SerializeField] private Image backgroundImage;

        [Header("Position Settings")]
        [SerializeField] private float leftOffset = 50f;
        [SerializeField] private float rightOffset = -50f;

        public Image AvatarImage => avatarImage;

        public void SetName(string name)
        {
            if (nameText != null)
            {
                nameText.text = name;
            }
        }

        public void SetText(string text)
        {
            if (dialogueText != null)
            {
                // Parse emotion tags to TMP sprite tags
                string processedText = EmotionTagParser.ParseEmotions(text);
                dialogueText.text = processedText;
            }
        }

        public void SetPosition(bool isLeft)
        {
            if (layoutGroup != null)
            {
                // Reverse children order for right-side dialogue
                layoutGroup.reverseArrangement = !isLeft;
            }

            if (nameText != null)
            {
                nameText.alignment = isLeft ? TextAlignmentOptions.Left : TextAlignmentOptions.Right;
            }

            if (dialogueText != null)
            {
                dialogueText.alignment = isLeft ? TextAlignmentOptions.TopLeft : TextAlignmentOptions.TopRight;
            }

            // Apply horizontal offset
            if (boxRect != null)
            {
                Vector2 pos = boxRect.anchoredPosition;
                pos.x = isLeft ? leftOffset : rightOffset;
                boxRect.anchoredPosition = pos;
            }
        }

        /// <summary>
        /// Setup the dialogue box with processed data.
        /// </summary>
        public void Setup(ProcessedDialogue data)
        {
            SetName(data.Name);
            SetText(data.Text);
            SetPosition(data.IsLeftPosition);
        }
    }
}
