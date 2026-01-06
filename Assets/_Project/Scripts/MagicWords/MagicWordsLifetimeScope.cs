using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Lifetime scope for Magic Words scene.
    /// Registers services and uses RegisterComponentInHierarchy for UI.
    /// </summary>
    [DefaultExecutionOrder(-500)]
    public class MagicWordsLifetimeScope : LifetimeScope
    {
        [Header("Services")]
        [SerializeField] private APIService apiService;
        [SerializeField] private AvatarLoader avatarLoader;

        [Header("Scene Components")]
        [SerializeField] private MagicWordsManager magicWordsManager;

        protected override void Configure(IContainerBuilder builder)
        {
            // Register services as interfaces
            builder.RegisterComponent(apiService).As<IAPIService>();
            builder.RegisterComponent(avatarLoader).As<IAvatarLoader>();
            builder.RegisterComponent(magicWordsManager);

            // Auto-find and inject BackButton in scene hierarchy
            builder.RegisterComponentInHierarchy<BackButton>();
        }
    }
}
