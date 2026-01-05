using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoftGames.Core
{
    /// <summary>
    /// Root lifetime scope for app-wide dependencies.
    /// Auto-instantiated by VContainerSettings from prefab.
    /// All scene LifetimeScopes become children automatically.
    /// </summary>
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameManager gameManager;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("[RootLifetimeScope] Configure()");

            if (gameManager != null)
            {
                Debug.Log("[RootLifetimeScope] Registering GameManager as ISceneLoader");
                builder.RegisterComponent(gameManager)
                    .As<ISceneLoader>()
                    .As<GameManager>();
            }
            else
            {
                Debug.LogError("[RootLifetimeScope] GameManager is NULL! Check prefab.");
            }
        }

        protected override void Awake()
        {
            Debug.Log("[RootLifetimeScope] Awake() - DontDestroyOnLoad");
            DontDestroyOnLoad(gameObject);
            base.Awake();
            Debug.Log("[RootLifetimeScope] Ready");
        }
    }
}
