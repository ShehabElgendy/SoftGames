using UnityEngine;
using TMPro;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// MonoBehaviour wrapper for card stack.
    /// Handles Unity-specific concerns (positioning, UI).
    /// Delegates logic to CardStackModel.
    /// </summary>
    public class CardStack : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform cardContainer;
        [SerializeField] private TextMeshProUGUI counterText;

        [Header("Settings")]
        [SerializeField] private Vector2 cardOffset = new Vector2(0.5f, -1f);
        [SerializeField] private int stackId = 0;

        private readonly CardStackModel<CardController> model = new();

        public int CardCount => model.Count;
        public Transform CardContainer => cardContainer != null ? cardContainer : transform;
        public int StackId => stackId;

        /// <summary>
        /// Add a card to this stack.
        /// </summary>
        /// <param name="card">The card to add</param>
        /// <param name="positionCard">If true, position the card. If false, card was animated and is already at correct position</param>
        public void AddCard(CardController card, bool positionCard = true)
        {
            int index = model.Count;
            model.Add(card);

            if (positionCard)
            {
                // Initial setup: reparent and position locally
                card.transform.SetParent(CardContainer, false);
                Vector2 localPos = new Vector2(index * cardOffset.x, index * cardOffset.y);
                card.SetStackPosition(localPos);
            }
            else
            {
                // Card was animated to correct position - just reparent while keeping world position
                RectTransform rect = card.RectTransform;
                Vector3 worldPos = rect.position;
                Vector3 scale = rect.localScale;

                rect.SetParent(CardContainer, true);

                // Ensure position and scale are preserved after reparenting
                rect.position = worldPos;
                rect.localScale = scale;
            }

            // Use base sorting order of 100 to ensure cards render above background
            card.SetSortingOrder(100 + index);
            UpdateCounter();
        }

        /// <summary>
        /// Remove and return the top card from this stack.
        /// </summary>
        public CardController RemoveTopCard()
        {
            var card = model.RemoveTop();
            UpdateCounter();
            return card;
        }

        /// <summary>
        /// Get target position for the next card to be added (world space).
        /// </summary>
        public Vector3 GetNextCardWorldPosition()
        {
            int nextIndex = model.Count;
            Vector2 localOffset = new Vector2(
                nextIndex * cardOffset.x,
                nextIndex * cardOffset.y
            );
            return CardContainer.TransformPoint(localOffset);
        }

        /// <summary>
        /// Get the next sorting order for incoming card.
        /// </summary>
        public int GetNextSortingOrder()
        {
            // Base sorting order of 100 to render above background
            return 100 + model.GetNextSortingOrder();
        }

        private void UpdateCounter()
        {
            if (counterText != null)
            {
                counterText.text = model.Count.ToString();
            }
        }

        /// <summary>
        /// Set the card offset for stacking.
        /// </summary>
        public void SetCardOffset(Vector2 offset)
        {
            cardOffset = offset;
        }

        /// <summary>
        /// Clear all cards from this stack.
        /// </summary>
        public void ClearCards()
        {
            model.Clear();
            UpdateCounter();
        }
    }
}
