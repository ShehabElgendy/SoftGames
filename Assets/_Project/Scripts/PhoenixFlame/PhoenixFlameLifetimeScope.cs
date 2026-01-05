using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Lifetime scope for Phoenix Flame scene.
    /// Registers scene-specific dependencies.
    /// </summary>
    public class PhoenixFlameLifetimeScope : LifetimeScope
    {
        [SerializeField] private FireController fireController;
        [SerializeField] private ColorTransitionController colorController;
        [SerializeField] private PhoenixFlameManager phoenixFlameManager;

        protected override void Configure(IContainerBuilder builder)
        {
            // Register fire controller
            if (fireController != null)
            {
                builder.RegisterComponent(fireController).As<IFireController>();
            }

            // Register color controller
            if (colorController != null)
            {
                builder.RegisterComponent(colorController).As<IColorCycler>();
            }

            // Register manager
            if (phoenixFlameManager != null)
            {
                builder.RegisterComponent(phoenixFlameManager);
            }
        }
    }
}
