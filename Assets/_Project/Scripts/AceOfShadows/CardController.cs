using UnityEngine;
using UnityEngine.UI;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Individual card behavior and visual management.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class CardController : MonoBehaviour, ICard
    {
        [Header("Visual")]
        [SerializeField] private Sprite cardFrontSprite;
        [SerializeField] private Sprite cardBackSprite;

        private RectTransform rectTransform;
        private Image cardImage;
        private Canvas sortingCanvas;

        public RectTransform RectTransform => rectTransform;
        public bool IsAnimating { get; private set; }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            cardImage = GetComponent<Image>();

            // Add canvas for sorting order control
            sortingCanvas = gameObject.AddComponent<Canvas>();
            sortingCanvas.overrideSorting = true;

            // Add GraphicRaycaster if needed for interaction
            if (GetComponent<GraphicRaycaster>() == null)
            {
                gameObject.AddComponent<GraphicRaycaster>();
            }

            // Set initial sprite (show BACK of card - will flip to front during animation)
            if (cardImage != null && cardBackSprite != null)
            {
                cardImage.sprite = cardBackSprite;
            }
        }

        /// <summary>
        /// Set card's position within a stack.
        /// </summary>
        public void SetStackPosition(Vector2 position)
        {
            rectTransform.anchoredPosition = position;
        }

        /// <summary>
        /// Set sorting order for proper card layering.
        /// </summary>
        public void SetSortingOrder(int order)
        {
            if (sortingCanvas != null)
            {
                sortingCanvas.sortingOrder = order;
            }
        }

        /// <summary>
        /// Show card face or back.
        /// </summary>
        public void ShowFace(bool showFront)
        {
            if (cardImage != null)
            {
                cardImage.sprite = showFront ? cardFrontSprite : cardBackSprite;
            }
        }

        /// <summary>
        /// Mark animation state.
        /// </summary>
        public void SetAnimating(bool animating)
        {
            IsAnimating = animating;
        }

        /// <summary>
        /// Set the card sprite directly.
        /// </summary>
        public void SetSprite(Sprite sprite)
        {
            if (cardImage != null)
            {
                cardImage.sprite = sprite;
            }
        }
    }
}
