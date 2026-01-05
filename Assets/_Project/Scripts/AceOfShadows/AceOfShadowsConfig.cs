using UnityEngine;
using DG.Tweening;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// ScriptableObject configuration for Ace of Shadows.
    /// Create via: Create > Softgames > Ace of Shadows Config
    /// </summary>
    [CreateAssetMenu(fileName = "AceOfShadowsConfig", menuName = "Softgames/Ace of Shadows Config")]
    public class AceOfShadowsConfig : ScriptableObject
    {
        [Header("Card Settings")]
        [Tooltip("Total number of cards to create")]
        public int totalCards = 144;

        [Tooltip("Time between each card move (seconds)")]
        public float moveInterval = 1f;

        [Tooltip("Duration of card move animation")]
        public float moveDuration = 0.5f;

        [Header("Stack Layout")]
        [Tooltip("Offset between stacked cards (creates depth effect)")]
        public Vector2 cardStackOffset = new Vector2(0.5f, -1f);

        [Header("Animation")]
        [Tooltip("Easing type for card movement")]
        public Ease moveEaseType = Ease.OutQuad;

        [Tooltip("Arc height for card movement (0 = straight line)")]
        public float moveArcHeight = 100f;
    }
}
