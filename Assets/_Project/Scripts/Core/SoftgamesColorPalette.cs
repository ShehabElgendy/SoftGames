using UnityEngine;

namespace SoftGames.Core
{
    /// <summary>
    /// SOFTGAMES official brand color palette.
    /// Create via: Create > Softgames > Color Palette
    /// Light, airy aesthetic matching softgames.com design language.
    /// </summary>
    [CreateAssetMenu(fileName = "SoftgamesColorPalette", menuName = "Softgames/Color Palette")]
    public class SoftgamesColorPalette : ScriptableObject
    {
        [Header("Primary Brand Colors")]
        [Tooltip("SOFTGAMES Orange - Primary accent color")]
        public Color primaryOrange = new Color32(0xF6, 0x8B, 0x2C, 0xFF); // #F68B2C

        [Tooltip("Dark Teal - Headlines and titles")]
        public Color darkTeal = new Color32(0x1F, 0x3D, 0x44, 0xFF); // #1F3D44

        [Header("Scene Background Colors")]
        [Tooltip("Main Menu - Pure white")]
        public Color bgMainMenu = Color.white; // #FFFFFF

        [Tooltip("Gameplay - Light neutral")]
        public Color bgGameplay = new Color32(0xF6, 0xF7, 0xF8, 0xFF); // #F6F7F8

        [Tooltip("Secondary scenes - Soft gray-blue")]
        public Color bgSecondary = new Color32(0xEE, 0xF1, 0xF3, 0xFF); // #EEF1F3

        [Tooltip("Special/Featured - Warm orange tint")]
        public Color bgSpecial = new Color32(0xFF, 0xF4, 0xE8, 0xFF); // #FFF4E8

        [Header("Text Colors")]
        [Tooltip("Main text")]
        public Color textPrimary = new Color32(0x3A, 0x3A, 0x3A, 0xFF); // #3A3A3A

        [Tooltip("Secondary/Descriptions")]
        public Color textSecondary = new Color32(0x7A, 0x7A, 0x7A, 0xFF); // #7A7A7A

        [Tooltip("Muted/Hint text")]
        public Color textMuted = new Color32(0xA0, 0xA6, 0xAB, 0xFF); // #A0A6AB

        [Header("Button Colors")]
        [Tooltip("Primary button background")]
        public Color buttonPrimary = new Color32(0xF6, 0x8B, 0x2C, 0xFF); // #F68B2C

        [Tooltip("Primary button hover/pressed")]
        public Color buttonPrimaryHover = new Color32(0xE6, 0x79, 0x1E, 0xFF); // #E6791E

        [Tooltip("Button text - White")]
        public Color buttonText = Color.white;

        [Tooltip("Disabled button background")]
        public Color buttonDisabled = new Color32(0xDA, 0xDA, 0xDA, 0xFF); // #DADADA

        [Tooltip("Disabled button text")]
        public Color buttonDisabledText = new Color32(0x9B, 0x9B, 0x9B, 0xFF); // #9B9B9B

        [Header("Panel & Card Colors")]
        [Tooltip("Panel background - White")]
        public Color panelBackground = Color.white; // #FFFFFF

        [Tooltip("Panel border/divider")]
        public Color panelBorder = new Color32(0xE1, 0xE4, 0xE8, 0xFF); // #E1E4E8

        [Tooltip("Shadow color - Very soft")]
        public Color shadowColor = new Color(0f, 0f, 0f, 0.08f); // RGBA(0,0,0,0.08)

        [Header("Decorative Accent Colors")]
        [Tooltip("Accent Blue")]
        public Color accentBlue = new Color32(0x2F, 0xA4, 0xFF, 0xFF); // #2FA4FF

        [Tooltip("Accent Green")]
        public Color accentGreen = new Color32(0x2D, 0xBE, 0x7F, 0xFF); // #2DBE7F

        [Tooltip("Accent Pink")]
        public Color accentPink = new Color32(0xF4, 0x5B, 0x9A, 0xFF); // #F45B9A

        [Tooltip("Accent Yellow")]
        public Color accentYellow = new Color32(0xFF, 0xC8, 0x3D, 0xFF); // #FFC83D

        [Header("Game-Specific Colors")]
        [Tooltip("Card table felt green")]
        public Color tableFelt = new Color32(0x1B, 0x5E, 0x4A, 0xFF); // #1B5E4A

        [Tooltip("Fire Orange")]
        public Color fireOrange = new Color32(0xF6, 0x8B, 0x2C, 0xFF); // Same as brand orange

        [Tooltip("Fire Green")]
        public Color fireGreen = new Color32(0x2D, 0xBE, 0x7F, 0xFF); // #2DBE7F

        [Tooltip("Fire Blue")]
        public Color fireBlue = new Color32(0x2F, 0xA4, 0xFF, 0xFF); // #2FA4FF

        /// <summary>
        /// Get button hover color
        /// </summary>
        public Color GetButtonHoverColor() => buttonPrimaryHover;

        /// <summary>
        /// Get button pressed color (slightly darker than hover)
        /// </summary>
        public Color GetButtonPressedColor()
        {
            Color.RGBToHSV(buttonPrimaryHover, out float h, out float s, out float v);
            return Color.HSVToRGB(h, s, v * 0.9f);
        }

        /// <summary>
        /// Get secondary button colors (white bg, orange border/text)
        /// </summary>
        public (Color background, Color border, Color text) GetSecondaryButtonColors()
        {
            return (Color.white, primaryOrange, primaryOrange);
        }
    }
}
