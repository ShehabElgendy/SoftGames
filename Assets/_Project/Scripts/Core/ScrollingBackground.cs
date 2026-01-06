using UnityEngine;
using UnityEngine.UI;

namespace SoftGames.Core
{
    /// <summary>
    /// Scrolls a background texture infinitely by animating UV offset.
    /// Works with UI RawImage component for seamless tiling.
    /// </summary>
    [RequireComponent(typeof(RawImage))]
    public class ScrollingBackground : MonoBehaviour
    {
        [Header("Scroll Settings")]
        [SerializeField] private Vector2 scrollSpeed = new Vector2(0.1f, 0f);

        private RawImage rawImage;
        private Vector2 offset;

        private void Awake()
        {
            rawImage = GetComponent<RawImage>();
        }

        private void Update()
        {
            offset += scrollSpeed * Time.deltaTime;
            rawImage.uvRect = new Rect(offset, rawImage.uvRect.size);
        }

        public void SetScrollSpeed(Vector2 speed)
        {
            scrollSpeed = speed;
        }

        public void SetScrollSpeed(float x, float y)
        {
            scrollSpeed = new Vector2(x, y);
        }
    }
}
