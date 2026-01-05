using UnityEngine;
using System;
using DG.Tweening;
using SoftGames.Core;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Handles smooth card movement animations using DOTween.
    /// Includes flip animation to reveal card front.
    /// </summary>
    public class CardAnimator : MonoBehaviour, ICardAnimator
    {
        [Header("Animation Settings")]
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private Ease easeType = Ease.OutQuad;
        [SerializeField] private float arcHeight = 100f;

        [Header("Flip Settings")]
        [SerializeField] private float flipDuration = 0.3f;
        [SerializeField] private Ease flipEase = Ease.InOutSine;

        /// <summary>
        /// Animate card from current position to target position with arc and flip.
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

            // Create sequence for combined movement and flip
            Sequence sequence = DOTween.Sequence();

            // Move along path
            sequence.Append(rect.DOPath(path, moveDuration, PathType.CatmullRom).SetEase(easeType));

            // Flip animation - starts at 30% of move duration
            float flipStartTime = moveDuration * 0.3f;
            float halfFlip = flipDuration * 0.5f;

            // Scale X to 0 (first half of flip)
            sequence.Insert(flipStartTime,
                rect.DOScaleX(0f, halfFlip).SetEase(flipEase)
                    .OnComplete(() => card.ShowFace(true))); // Swap to front at midpoint

            // Scale X back to 1 (second half of flip)
            sequence.Insert(flipStartTime + halfFlip,
                rect.DOScaleX(1f, halfFlip).SetEase(flipEase));

            // On complete
            sequence.OnComplete(() =>
            {
                rect.position = targetWorldPosition;
                rect.localScale = Vector3.one; // Ensure scale is reset
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
