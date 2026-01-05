using UnityEngine;
using UnityEngine.UI;

namespace SoftGames.Core
{
    /// <summary>
    /// Ensures canvas scales properly for mobile and desktop.
    /// Attach to Canvas GameObject.
    /// </summary>
    [RequireComponent(typeof(CanvasScaler))]
    public class ResponsiveCanvas : MonoBehaviour
    {
        [SerializeField] private Vector2 referenceResolution = new Vector2(1080, 1920);
        [SerializeField] private float portraitMatch = 0.5f;
        [SerializeField] private float landscapeMatch = 0.7f;

        private CanvasScaler canvasScaler;

        private void Awake()
        {
            canvasScaler = GetComponent<CanvasScaler>();
            ConfigureScaler();
        }

        private void Start()
        {
            UpdateMatchValue();
        }

        private void ConfigureScaler()
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = referenceResolution;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        }

        private void UpdateMatchValue()
        {
            float aspectRatio = (float)Screen.width / Screen.height;
            // More width-based scaling for landscape, more height-based for portrait
            canvasScaler.matchWidthOrHeight = aspectRatio > 1f ? landscapeMatch : portraitMatch;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (canvasScaler != null)
            {
                ConfigureScaler();
            }
        }
#endif
    }
}
