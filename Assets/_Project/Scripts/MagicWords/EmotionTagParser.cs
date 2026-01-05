using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Parses emotion tags in dialogue text and converts to TMP sprite tags.
    /// Uses the MeridaOrange approach for WebGL compatibility.
    /// </summary>
    public static class EmotionTagParser
    {
        // Maps API emotion tags to TMP sprite indices
        // These indices should match your TMP Sprite Asset
        private static readonly Dictionary<string, int> EmotionToSpriteIndex = new()
        {
            { "satisfied", 0 },
            { "intrigued", 1 },
            { "skeptical", 2 },
            { "excited", 3 },
            { "thoughtful", 4 },
            { "happy", 5 },
            { "sad", 6 },
            { "angry", 7 },
            { "surprised", 8 },
            { "confused", 9 },
        };

        // Regex to find {emotion} tags
        private static readonly Regex EmotionRegex = new(@"\{(\w+)\}", RegexOptions.Compiled);

        /// <summary>
        /// Parse emotion tags and convert to TMP sprite tags.
        /// Example: "{satisfied}" becomes "<sprite=0>"
        /// </summary>
        public static string ParseEmotions(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            return EmotionRegex.Replace(text, match =>
            {
                string emotion = match.Groups[1].Value.ToLower();
                if (EmotionToSpriteIndex.TryGetValue(emotion, out int index))
                {
                    return $"<sprite={index}>";
                }
                // Unknown tag - remove it
                return "";
            });
        }

        /// <summary>
        /// Parse emotion tags and convert to named TMP sprite tags.
        /// Example: "{satisfied}" becomes "<sprite name=\"satisfied\">"
        /// </summary>
        public static string ParseEmotionsNamed(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            return EmotionRegex.Replace(text, match =>
            {
                string emotion = match.Groups[1].Value.ToLower();
                if (EmotionToSpriteIndex.ContainsKey(emotion))
                {
                    return $"<sprite name=\"{emotion}\">";
                }
                return "";
            });
        }

        /// <summary>
        /// Strip all emotion tags from text without replacement.
        /// </summary>
        public static string StripEmotionTags(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return EmotionRegex.Replace(text, "");
        }
    }
}
