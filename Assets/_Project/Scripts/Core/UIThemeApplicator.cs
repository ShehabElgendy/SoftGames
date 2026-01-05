using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SoftGames.Core
{
    /// <summary>
    /// Applies SOFTGAMES color palette to UI elements.
    /// Attach to any GameObject to apply theme colors to its children.
    /// </summary>
    public class UIThemeApplicator : MonoBehaviour
    {
        [Header("Color Palette")]
        [SerializeField] private SoftgamesColorPalette colorPalette;

        [Header("Apply Settings")]
        [SerializeField] private bool applyOnStart = true;
        [SerializeField] private bool applyToChildren = true;

        private void Start()
        {
            if (applyOnStart && colorPalette != null)
            {
                ApplyTheme();
            }
        }

        [ContextMenu("Apply Theme")]
        public void ApplyTheme()
        {
            if (colorPalette == null)
            {
                Debug.LogWarning("UIThemeApplicator: No color palette assigned!");
                return;
            }

            if (applyToChildren)
            {
                ApplyToAllChildren(transform);
            }
        }

        private void ApplyToAllChildren(Transform parent)
        {
            // Apply to buttons
            foreach (var button in parent.GetComponentsInChildren<Button>(true))
            {
                ApplyButtonTheme(button);
            }

            // Apply to panels (Images with "Panel" in name)
            foreach (var image in parent.GetComponentsInChildren<Image>(true))
            {
                if (image.gameObject.name.Contains("Panel") || image.gameObject.name.Contains("Background"))
                {
                    image.color = colorPalette.panelBackground;
                }
            }

            // Apply to text
            foreach (var text in parent.GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                ApplyTextTheme(text);
            }
        }

        private void ApplyButtonTheme(Button button)
        {
            // Set button colors
            ColorBlock colors = button.colors;
            colors.normalColor = colorPalette.buttonPrimary;
            colors.highlightedColor = colorPalette.GetButtonHoverColor();
            colors.pressedColor = colorPalette.GetButtonPressedColor();
            colors.selectedColor = colorPalette.buttonPrimary;
            colors.disabledColor = colorPalette.buttonDisabled;
            button.colors = colors;

            // Set button background image to white (tinted by ColorBlock)
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = Color.white;
            }

            // Set button text color
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.color = colorPalette.buttonText;
            }
        }

        private void ApplyTextTheme(TextMeshProUGUI text)
        {
            // Skip button text (already handled)
            if (text.GetComponentInParent<Button>() != null)
                return;

            // Title text (larger font) uses dark teal
            if (text.fontSize >= 36)
            {
                text.color = colorPalette.darkTeal;
            }
            // Regular text
            else
            {
                text.color = colorPalette.textPrimary;
            }
        }
    }
}
