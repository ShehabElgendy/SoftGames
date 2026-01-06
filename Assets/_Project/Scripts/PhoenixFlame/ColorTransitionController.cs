using UnityEngine;
using TMPro;
using SoftGames.Core;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Controls fire color transitions via Animator Controller.
    /// Assign the fire Animator (on ParticleSystem) in Inspector.
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
            UpdateIndicatorText();
        }

        /// <summary>
        /// Triggers next color transition. Called by PhoenixFlameManager.
        /// </summary>
        public void CycleColor()
        {
            if (fireAnimator == null) return;

            currentColorIndex = (currentColorIndex + 1) % COLOR_NAMES.Length;
            fireAnimator.SetTrigger(NEXT_COLOR_TRIGGER);
            UpdateIndicatorText();

            EventBus.Publish(new FireColorChangedEvent
            {
                ColorName = COLOR_NAMES[currentColorIndex],
                ColorIndex = currentColorIndex
            });
        }

        private void UpdateIndicatorText()
        {
            if (colorIndicatorText != null)
            {
                colorIndicatorText.text = $"Current: {COLOR_NAMES[currentColorIndex]}";
            }
        }
    }
}
