using UnityEngine;
using DG.Tweening;

namespace SoftGames.UI
{
    /// <summary>
    /// Pop animation for panels/modals.
    /// Attach to any panel that should animate in/out.
    /// </summary>
    public class PanelPop : MonoBehaviour
    {
        [SerializeField] private float popDuration = 0.3f;
        [SerializeField] private bool animateOnEnable = true;
        [SerializeField] private Ease easeIn = Ease.OutBack;
        [SerializeField] private Ease easeOut = Ease.InBack;

        private void OnEnable()
        {
            if (animateOnEnable)
            {
                PopIn();
            }
        }

        public void PopIn()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, popDuration).SetEase(easeIn);
        }

        public void PopOut(bool deactivateOnComplete = true)
        {
            transform.DOScale(Vector3.zero, popDuration)
                .SetEase(easeOut)
                .OnComplete(() =>
                {
                    if (deactivateOnComplete)
                    {
                        gameObject.SetActive(false);
                    }
                });
        }

        /// <summary>
        /// Show the panel with pop animation.
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            PopIn();
        }

        /// <summary>
        /// Hide the panel with pop animation.
        /// </summary>
        public void Hide()
        {
            PopOut(true);
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }
    }
}
