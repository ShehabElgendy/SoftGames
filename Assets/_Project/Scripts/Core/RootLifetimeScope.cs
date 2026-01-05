using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.Core
{
    /// <summary>
    /// Root lifetime scope for app-wide dependencies.
    /// Attach to a GameObject in the first scene with "Don't Destroy On Load".
    /// </summary>
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameManager gameManagerPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            // Register GameManager as singleton implementing ISceneLoader
            builder.RegisterComponentInNewPrefab(gameManagerPrefab, Lifetime.Singleton)
                .As<ISceneLoader>()
                .As<GameManager>();

            // Register EventBus operations as static - no need to register
            // EventBus is already static and accessible globally
        }
    }
}
