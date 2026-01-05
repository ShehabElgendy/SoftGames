using System;
using UnityEngine;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Interface for card animation.
    /// Allows mocking in tests.
    /// </summary>
    public interface ICardAnimator
    {
        void AnimateCardToPosition(
            CardController card,
            Vector3 targetWorldPosition,
            int targetSortingOrder,
            Action onComplete = null);

        void Configure(float duration, float arcHeight);
    }

    /// <summary>
    /// Interface for card stack operations.
    /// </summary>
    public interface ICardStack
    {
        int CardCount { get; }
        Transform CardContainer { get; }
        void AddCard(CardController card);
        CardController RemoveTopCard();
        Vector3 GetNextCardWorldPosition();
        int GetNextSortingOrder();
        void ClearCards();
    }
}
