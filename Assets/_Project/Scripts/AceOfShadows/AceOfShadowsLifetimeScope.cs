using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Lifetime scope for Ace of Shadows scene.
    /// Parent is RootLifetimeScope (via EnqueueParent in GameManager).
    /// </summary>
    [DefaultExecutionOrder(-500)]
    public class AceOfShadowsLifetimeScope : LifetimeScope
    {
        [Header("Scene Components")]
        [SerializeField] private CardStackManager cardStackManager;
        [SerializeField] private CardAnimator cardAnimator;

        [Header("UI")]
        [SerializeField] private BackButton backButton;

        protected override void Configure(IContainerBuilder builder)
        {
            if (cardAnimator != null)
            {
                builder.RegisterComponent(cardAnimator).As<ICardAnimator>();
            }

            if (cardStackManager != null)
            {
                builder.RegisterComponent(cardStackManager);
            }

            if (backButton != null)
            {
                builder.RegisterComponent(backButton);
            }
        }
    }
}
