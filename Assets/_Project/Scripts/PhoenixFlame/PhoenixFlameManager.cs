using UnityEngine;
using UnityEngine.UI;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Main orchestrator for Phoenix Flame demo.
    /// </summary>
    public class PhoenixFlameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private FireController fireController;
        [SerializeField] private ColorTransitionController colorController;
        [SerializeField] private Button colorButton;

        [Header("Optional")]
        [SerializeField] private FireColorData colorData;

        private void Start()
        {
            // Wire up button
            if (colorButton != null)
            {
                colorButton.onClick.AddListener(OnColorButtonClicked);
            }

            // Initialize fire with default color if we have direct access
            if (fireController != null && colorData != null)
            {
                fireController.SetFireColor(colorData.orangeColor);
            }
        }

        private void OnColorButtonClicked()
        {
            if (colorController != null)
            {
                colorController.CycleColor();
            }
        }

        /// <summary>
        /// Reset to initial state.
        /// </summary>
        public void Reset()
        {
            if (colorController != null)
            {
                colorController.SetColorImmediate(0);
            }
        }

        private void OnDestroy()
        {
            if (colorButton != null)
            {
                colorButton.onClick.RemoveListener(OnColorButtonClicked);
            }
        }
    }
}
