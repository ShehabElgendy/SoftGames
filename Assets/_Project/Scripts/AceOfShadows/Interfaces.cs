using System;
using UnityEngine;
using DG.Tweening;

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
        void Configure(float duration, Ease easeType, float arcHeight);
    }

    /// <summary>
    /// Interface for individual card.
    /// Allows mocking in tests.
    /// </summary>
    public interface ICard
    {
        void SetStackPosition(Vector2 position);
        void SetSortingOrder(int order);
        void SetAnimating(bool animating);
    }

    /// <summary>
    /// Interface for card stack operations.
    /// </summary>
    public interface ICardStack
    {
        int CardCount { get; }
        Transform CardContainer { get; }
        void AddCard(CardController card, bool positionCard = true);
        CardController RemoveTopCard();
        Vector3 GetNextCardWorldPosition();
        int GetNextSortingOrder();
        void ClearCards();
    }
}
