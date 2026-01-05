using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.UI
{
    /// <summary>
    /// Lifetime scope for MainMenu scene.
    /// Drag SceneButtons into array in Inspector.
    /// Uses RegisterBuildCallback to inject into multiple components without registration conflicts.
    /// </summary>
    public class MainMenuLifetimeScope : LifetimeScope
    {
        [Header("Scene Buttons (drag all SceneButton components here)")]
        [SerializeField] private SceneButton[] sceneButtons;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log($"[MainMenuLifetimeScope] Configure() - Parent: {Parent?.name ?? "NULL"}");

            // Use RegisterBuildCallback to inject into multiple components of same type
            // This avoids "Conflict implementation type" error
            builder.RegisterBuildCallback(resolver =>
            {
                if (sceneButtons != null)
                {
                    foreach (var btn in sceneButtons)
                    {
                        if (btn != null)
                        {
                            resolver.Inject(btn);
                            Debug.Log($"[MainMenuLifetimeScope] Injected: {btn.gameObject.name}");
                        }
                    }
                }
            });
        }
    }
}
