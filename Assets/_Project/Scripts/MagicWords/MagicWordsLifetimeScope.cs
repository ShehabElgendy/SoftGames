using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Lifetime scope for Magic Words scene.
    /// Registers scene-specific dependencies.
    /// </summary>
    public class MagicWordsLifetimeScope : LifetimeScope
    {
        [SerializeField] private APIService apiService;
        [SerializeField] private AvatarLoader avatarLoader;
        [SerializeField] private MagicWordsManager magicWordsManager;

        protected override void Configure(IContainerBuilder builder)
        {
            // Register API service
            if (apiService != null)
            {
                builder.RegisterComponent(apiService).As<IAPIService>();
            }

            // Register avatar loader
            if (avatarLoader != null)
            {
                builder.RegisterComponent(avatarLoader).As<IAvatarLoader>();
            }

            // Register manager
            if (magicWordsManager != null)
            {
                builder.RegisterComponent(magicWordsManager);
            }
        }
    }
}
