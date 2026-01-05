using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Lifetime scope for Phoenix Flame scene.
    /// </summary>
    public class PhoenixFlameLifetimeScope : LifetimeScope
    {
        [SerializeField] private BackButton backButton;

        protected override void Configure(IContainerBuilder builder)
        {
            if (backButton != null)
            {
                builder.RegisterComponent(backButton);
            }
        }
    }
}
