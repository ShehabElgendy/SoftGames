using UnityEngine;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Color definitions for fire effect.
    /// Create via: Create > Softgames > Fire Color Data
    /// </summary>
    [CreateAssetMenu(fileName = "FireColorData", menuName = "Softgames/Fire Color Data")]
    public class FireColorData : ScriptableObject
    {
        [Header("Fire Colors")]
        [ColorUsage(true, true)]
        public Color orangeColor = new Color(1f, 0.42f, 0f, 1f);        // #FF6B00

        [ColorUsage(true, true)]
        public Color greenColor = new Color(0f, 1f, 0.42f, 1f);         // #00FF6B

        [ColorUsage(true, true)]
        public Color blueColor = new Color(0f, 0.42f, 1f, 1f);          // #006BFF

        [Header("Transition")]
        [Tooltip("Duration of color transition in seconds")]
        public float transitionDuration = 1f;

        public Color[] GetColorSequence()
        {
            return new Color[] { orangeColor, greenColor, blueColor };
        }

        public string[] GetColorNames()
        {
            return new string[] { "Orange", "Green", "Blue" };
        }

        public Color GetColor(int index)
        {
            return index switch
            {
                0 => orangeColor,
                1 => greenColor,
                2 => blueColor,
                _ => orangeColor
            };
        }

        public string GetColorName(int index)
        {
            return index switch
            {
                0 => "Orange",
                1 => "Green",
                2 => "Blue",
                _ => "Orange"
            };
        }
    }
}
