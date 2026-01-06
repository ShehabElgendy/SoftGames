using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Lifetime scope for Ace of Shadows scene.
    /// Registers services and uses RegisterComponentInHierarchy for UI.
    /// </summary>
    [DefaultExecutionOrder(-500)]
    public class AceOfShadowsLifetimeScope : LifetimeScope
    {
        [Header("Scene Components")]
        [SerializeField] private CardStackManager cardStackManager;
        [SerializeField] private CardAnimator cardAnimator;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(cardAnimator).As<ICardAnimator>();
            builder.RegisterComponent(cardStackManager);

            // Auto-find and inject BackButton in scene hierarchy
            builder.RegisterComponentInHierarchy<BackButton>();
        }
    }
}
