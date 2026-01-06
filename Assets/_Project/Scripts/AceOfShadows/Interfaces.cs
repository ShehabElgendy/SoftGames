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
    }

    /// <summary>
    /// Interface for card behavior - used for mocking in tests.
    /// </summary>
    public interface ICard
    {
        void SetStackPosition(Vector2 position);
        void SetSortingOrder(int order);
        void SetAnimating(bool animating);
    }

}
