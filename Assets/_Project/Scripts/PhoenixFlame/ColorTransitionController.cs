using SoftGames.Core;
using UnityEngine;
using TMPro;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Controls color cycling through scripted transitions.
    /// Orange -> Green -> Blue -> Orange (loop)
    /// Implements IColorCycler for testability.
    /// Uses EventBus for decoupled communication.
    /// </summary>
    public class ColorTransitionController : MonoBehaviour, IColorCycler
    {
        [Header("References")]
        [SerializeField] private FireController fireController;
        [SerializeField] private Animator fireAnimator;
        [SerializeField] private TextMeshProUGUI colorIndicatorText;

        [Header("Colors")]
        [SerializeField] private FireColorData colorData;

        [Header("Animator Parameters")]
        [SerializeField] private string colorTriggerName = "NextColor";

        private int currentColorIndex = 0;
        private bool isTransitioning = false;

        public int CurrentColorIndex => currentColorIndex;
        public bool IsTransitioning => isTransitioning;

        private void Start()
        {
            // Initialize with first color (orange)
            SetColorImmediate(0);
        }

        /// <summary>
        /// Cycle to next color. Called by UI button.
        /// </summary>
        public void CycleColor()
        {
            if (isTransitioning) return;

            // Move to next color
            int previousIndex = currentColorIndex;
            currentColorIndex = (currentColorIndex + 1) % 3;

            // Publish event: color change requested
            EventBus.Publish(new FireColorChangeRequestedEvent
            {
                ColorIndex = currentColorIndex,
                ColorName = colorData != null ? colorData.GetColorName(currentColorIndex) : "Unknown"
            });

            // Use animator if available
            if (fireAnimator != null && fireAnimator.runtimeAnimatorController != null)
            {
                fireAnimator.SetTrigger(colorTriggerName);
            }

            // Always do scripted transition
            TransitionToColorIndex(currentColorIndex);

            UpdateIndicator();
        }

        /// <summary>
        /// Set color immediately (no transition).
        /// </summary>
        public void SetColorImmediate(int index)
        {
            currentColorIndex = index % 3;

            if (fireController != null && colorData != null)
            {
                Color color = colorData.GetColor(currentColorIndex);
                fireController.SetFireColor(color);

                // Publish event
                EventBus.Publish(new FireColorChangedEvent
                {
                    ColorIndex = currentColorIndex,
                    ColorName = colorData.GetColorName(currentColorIndex),
                    Color = color
                });
            }

            UpdateIndicator();
        }

        /// <summary>
        /// Smoothly transition to color by index.
        /// </summary>
        private void TransitionToColorIndex(int index)
        {
            if (fireController == null || colorData == null) return;

            isTransitioning = true;

            Color targetColor = colorData.GetColor(index);
            float duration = colorData.transitionDuration;

            fireController.TransitionToColor(targetColor, duration, () =>
            {
                isTransitioning = false;

                // Publish event: color changed
                EventBus.Publish(new FireColorChangedEvent
                {
                    ColorIndex = index,
                    ColorName = colorData.GetColorName(index),
                    Color = targetColor
                });
            });
        }

        private void UpdateIndicator()
        {
            if (colorIndicatorText != null && colorData != null)
            {
                colorIndicatorText.text = $"Current: {colorData.GetColorName(currentColorIndex)}";
            }
        }

        // Animation Event callbacks (called from Animator if used)
        public void OnAnimationSetOrange() => SetColorImmediate(0);
        public void OnAnimationSetGreen() => SetColorImmediate(1);
        public void OnAnimationSetBlue() => SetColorImmediate(2);
    }
}
