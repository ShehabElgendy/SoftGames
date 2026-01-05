using UnityEngine;
using TMPro;

namespace SoftGames.Core
{
    /// <summary>
    /// Displays FPS counter in top-left corner.
    /// Attach to a TextMeshProUGUI element.
    /// </summary>
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private float updateInterval = 0.5f;
        [SerializeField] private Color goodFPSColor = Color.green;
        [SerializeField] private Color mediumFPSColor = Color.yellow;
        [SerializeField] private Color badFPSColor = Color.red;

        private float deltaTime = 0f;
        private float timer = 0f;

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            timer += Time.unscaledDeltaTime;

            if (timer >= updateInterval)
            {
                timer = 0f;
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            if (fpsText == null) return;

            float fps = 1f / deltaTime;
            float ms = deltaTime * 1000f;

            fpsText.text = $"{fps:F0} FPS\n{ms:F1} ms";

            // Color code based on performance
            if (fps >= 55f)
                fpsText.color = goodFPSColor;
            else if (fps >= 30f)
                fpsText.color = mediumFPSColor;
            else
                fpsText.color = badFPSColor;
        }
    }
}
