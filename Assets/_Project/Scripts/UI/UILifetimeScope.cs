using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.UI
{
    /// <summary>
    /// Lifetime scope for UI elements.
    /// Can be used as a child scope in each scene.
    /// </summary>
    public class UILifetimeScope : LifetimeScope
    {
        [SerializeField] private MainMenuUI mainMenuUI;
        [SerializeField] private BackButton backButton;

        protected override void Configure(IContainerBuilder builder)
        {
            // Register UI components for injection
            if (mainMenuUI != null)
            {
                builder.RegisterComponent(mainMenuUI);
            }

            if (backButton != null)
            {
                builder.RegisterComponent(backButton);
            }
        }
    }
}
