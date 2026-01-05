using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SoftGames.Core;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Manages a single card stack with counter display.
    /// Implements ICardStack for testability.
    /// </summary>
    public class CardStack : MonoBehaviour, ICardStack
    {
        [Header("References")]
        [SerializeField] private Transform cardContainer;
        [SerializeField] private TextMeshProUGUI counterText;

        [Header("Settings")]
        [SerializeField] private Vector2 cardOffset = new Vector2(0.5f, -1f);
        [SerializeField] private int stackId = 0;

        private List<CardController> cards = new List<CardController>();

        public int CardCount => cards.Count;
        public Transform CardContainer => cardContainer != null ? cardContainer : transform;
        public int StackId => stackId;

        /// <summary>
        /// Add a card to this stack.
        /// </summary>
        public void AddCard(CardController card)
        {
            cards.Add(card);
            card.transform.SetParent(CardContainer, true);

            // Position card with stack offset
            int index = cards.Count - 1;
            Vector2 localPos = new Vector2(
                index * cardOffset.x,
                index * cardOffset.y
            );
            card.SetStackPosition(localPos);

            // Ensure proper sorting
            card.SetSortingOrder(index);

            UpdateCounter();
        }

        /// <summary>
        /// Remove and return the top card from this stack.
        /// </summary>
        public CardController RemoveTopCard()
        {
            if (cards.Count == 0) return null;

            CardController topCard = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);

            UpdateCounter();
            return topCard;
        }

        /// <summary>
        /// Get target position for the next card to be added (world space).
        /// </summary>
        public Vector3 GetNextCardWorldPosition()
        {
            int nextIndex = cards.Count;
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
            return cards.Count;
        }

        private void UpdateCounter()
        {
            if (counterText != null)
            {
                counterText.text = cards.Count.ToString();
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
            cards.Clear();
            UpdateCounter();
        }
    }
}
