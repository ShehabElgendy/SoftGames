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
        [SerializeField] private ColorTransitionController colorController;
        [SerializeField] private Button colorButton;

        private void Start()
        {
            if (colorButton != null)
            {
                colorButton.onClick.AddListener(OnColorButtonClicked);
            }
        }

        private void OnColorButtonClicked()
        {
            if (colorController != null)
            {
                colorController.CycleColor();
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
