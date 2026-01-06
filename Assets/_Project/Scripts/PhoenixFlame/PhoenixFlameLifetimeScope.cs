using SoftGames.Core;
using VContainer;
using VContainer.Unity;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Lifetime scope for Phoenix Flame scene.
    /// Uses RegisterComponentInHierarchy for UI.
    /// </summary>
    public class PhoenixFlameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Auto-find and inject BackButton in scene hierarchy
            builder.RegisterComponentInHierarchy<BackButton>();
        }
    }
}
