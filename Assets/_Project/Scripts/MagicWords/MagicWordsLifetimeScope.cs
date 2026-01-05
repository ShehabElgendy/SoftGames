using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Lifetime scope for Magic Words scene.
    /// Parent is RootLifetimeScope (via EnqueueParent in GameManager).
    /// </summary>
    [DefaultExecutionOrder(-500)]
    public class MagicWordsLifetimeScope : LifetimeScope
    {
        [Header("Scene Components")]
        [SerializeField] private APIService apiService;
        [SerializeField] private AvatarLoader avatarLoader;
        [SerializeField] private MagicWordsManager magicWordsManager;

        [Header("UI")]
        [SerializeField] private BackButton backButton;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log($"[MagicWordsLifetimeScope] Configure() - Parent: {Parent?.name ?? "NULL"}");

            if (apiService != null)
            {
                builder.RegisterComponent(apiService).As<IAPIService>();
            }

            if (avatarLoader != null)
            {
                builder.RegisterComponent(avatarLoader).As<IAvatarLoader>();
            }

            if (magicWordsManager != null)
            {
                builder.RegisterComponent(magicWordsManager);
            }

            // Register BackButton (drag in Inspector)
            if (backButton != null)
            {
                builder.RegisterComponent(backButton);
                Debug.Log("[MagicWordsLifetimeScope] Registered BackButton");
            }
        }
    }
}
