using UnityEngine;
using System;
using DG.Tweening;
using SoftGames.Core;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Handles smooth card movement animations using DOTween.
    /// Implements ICardAnimator for testability.
    /// </summary>
    public class CardAnimator : MonoBehaviour, ICardAnimator
    {
        [Header("Animation Settings")]
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private Ease easeType = Ease.OutQuad;
        [SerializeField] private float arcHeight = 100f;

        /// <summary>
        /// Animate card from current position to target position with arc.
        /// </summary>
        public void AnimateCardToPosition(
            CardController card,
            Vector3 targetWorldPosition,
            int targetSortingOrder,
            Action onComplete = null)
        {
            if (card == null) return;

            card.SetAnimating(true);

            // Bring card to front during animation
            card.SetSortingOrder(1000);

            RectTransform rect = card.RectTransform;
            Vector3 startPos = rect.position;

            // Calculate mid point for arc
            Vector3 midPoint = (startPos + targetWorldPosition) / 2f;
            midPoint.y += arcHeight;

            // Create path for bezier-like movement
            Vector3[] path = new Vector3[] { startPos, midPoint, targetWorldPosition };

            // Animate along path
            rect.DOPath(path, moveDuration, PathType.CatmullRom)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    rect.position = targetWorldPosition;
                    card.SetSortingOrder(targetSortingOrder);
                    card.SetAnimating(false);
                    onComplete?.Invoke();
                });
        }

        /// <summary>
        /// Configure animation settings.
        /// </summary>
        public void Configure(float duration, float arc)
        {
            moveDuration = duration;
            arcHeight = arc;
        }

        /// <summary>
        /// Configure with ease type.
        /// </summary>
        public void Configure(float duration, Ease ease, float arc)
        {
            moveDuration = duration;
            easeType = ease;
            arcHeight = arc;
        }
    }
}
