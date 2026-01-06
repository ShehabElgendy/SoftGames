using SoftGames.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.UI
{
    /// <summary>
    /// Lifetime scope for MainMenu scene.
    /// Uses RegisterBuildCallback to inject into multiple SceneButtons.
    /// </summary>
    public class MainMenuLifetimeScope : LifetimeScope
    {
        [Header("Scene Buttons")]
        [SerializeField] private SceneButton[] sceneButtons;

        protected override void Configure(IContainerBuilder builder)
        {
            // Multiple buttons need RegisterBuildCallback pattern
            builder.RegisterBuildCallback(resolver =>
            {
                foreach (var btn in sceneButtons)
                {
                    if (btn != null) resolver.Inject(btn);
                }
            });
        }
    }
}
