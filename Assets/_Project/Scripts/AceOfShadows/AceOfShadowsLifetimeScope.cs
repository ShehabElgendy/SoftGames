using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.AceOfShadows
{
    /// <summary>
    /// Lifetime scope for Ace of Shadows scene.
    /// Registers scene-specific dependencies.
    /// </summary>
    public class AceOfShadowsLifetimeScope : LifetimeScope
    {
        [SerializeField] private CardStackManager cardStackManager;
        [SerializeField] private CardAnimator cardAnimator;

        protected override void Configure(IContainerBuilder builder)
        {
            // Register scene components
            if (cardAnimator != null)
            {
                builder.RegisterComponent(cardAnimator).As<ICardAnimator>();
            }

            if (cardStackManager != null)
            {
                builder.RegisterComponent(cardStackManager);
            }
        }
    }
}
