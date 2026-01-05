using UnityEngine;
using TMPro;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Controls color cycling via Animator Controller.
    /// Orange -> Green -> Blue -> Orange (loop)
    /// </summary>
    public class ColorTransitionController : MonoBehaviour
    {
        private const string NEXT_COLOR_TRIGGER = "NextColor";
        private static readonly string[] COLOR_NAMES = { "Orange", "Green", "Blue" };

        [Header("References")]
        [SerializeField] private Animator fireAnimator;
        [SerializeField] private TextMeshProUGUI colorIndicatorText;
        private int currentColorIndex = 0;

        private void Start()
        {
            UpdateIndicator();
        }

        /// <summary>
        /// Cycle to next color. Called by UI button.
        /// </summary>
        public void CycleColor()
        {
            currentColorIndex = (currentColorIndex + 1) % 3;

            if (fireAnimator != null)
            {
                fireAnimator.SetTrigger(NEXT_COLOR_TRIGGER);
            }

            UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            if (colorIndicatorText != null)
            {
                colorIndicatorText.text = $"Current: {COLOR_NAMES[currentColorIndex]}";
            }
        }
    }
}
