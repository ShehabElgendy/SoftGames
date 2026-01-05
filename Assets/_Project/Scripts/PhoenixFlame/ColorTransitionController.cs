using UnityEngine;
using TMPro;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// MonoBehaviour wrapper for color cycling.
    /// Handles Unity-specific concerns (Animator, UI).
    /// Delegates logic to ColorCycleModel.
    /// </summary>
    public class ColorTransitionController : MonoBehaviour
    {
        private const string NextColorTrigger = "NextColor";

        [Header("References")]
        [SerializeField] private Animator fireAnimator;
        [SerializeField] private TextMeshProUGUI colorIndicatorText;

        private readonly ColorCycleModel model = new();

        private void Start()
        {
            UpdateIndicator();
        }

        /// <summary>
        /// Cycle to next color. Called by UI button.
        /// </summary>
        public void CycleColor()
        {
            model.CycleNext();

            if (fireAnimator != null)
            {
                fireAnimator.SetTrigger(NextColorTrigger);
            }

            UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            if (colorIndicatorText != null)
            {
                colorIndicatorText.text = $"Current: {model.CurrentColorName}";
            }
        }
    }
}
