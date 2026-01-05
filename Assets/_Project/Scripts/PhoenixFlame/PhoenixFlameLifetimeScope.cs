using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Lifetime scope for Phoenix Flame scene.
    /// Parent is RootLifetimeScope (via EnqueueParent in GameManager).
    /// </summary>
    [DefaultExecutionOrder(-500)]
    public class PhoenixFlameLifetimeScope : LifetimeScope
    {
        [Header("Scene Components")]
        [SerializeField] private FireController fireController;
        [SerializeField] private ColorTransitionController colorController;
        [SerializeField] private PhoenixFlameManager phoenixFlameManager;

        [Header("UI")]
        [SerializeField] private BackButton backButton;

        protected override void Configure(IContainerBuilder builder)
        {
            if (fireController != null)
            {
                builder.RegisterComponent(fireController).As<IFireController>();
            }

            if (colorController != null)
            {
                builder.RegisterComponent(colorController).As<IColorCycler>();
            }

            if (phoenixFlameManager != null)
            {
                builder.RegisterComponent(phoenixFlameManager);
            }

            if (backButton != null)
            {
                builder.RegisterComponent(backButton);
            }
        }
    }
}
