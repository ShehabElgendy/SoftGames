using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace SoftGames.Core
{
    /// <summary>
    /// Global game manager. Handles scene transitions and persistent state.
    /// Uses EventBus for decoupled scene change notifications.
    /// Uses VContainer's EnqueueParent for proper DI across scenes.
    /// </summary>
    public class GameManager : MonoBehaviour, ISceneLoader
    {
        public enum GameScene
        {
            MainMenu = 0,
            AceOfShadows = 1,
            MagicWords = 2,
            PhoenixFlame = 3
        }

        private LifetimeScope _rootScope;

        /// <summary>
        /// Injected by VContainer - the LifetimeScope that owns this registration.
        /// Used for EnqueueParent when loading scenes.
        /// </summary>
        [Inject]
        public void Construct(LifetimeScope ownerScope)
        {
            _rootScope = ownerScope;
            Debug.Log($"[GameManager] Inject SUCCESS - Got LifetimeScope: {ownerScope?.name ?? "NULL"}");
        }

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            EventBus.Publish(new SceneLoadedEvent
            {
                SceneIndex = scene.buildIndex,
                SceneName = scene.name
            });
        }

        public void LoadScene(GameScene scene)
        {
            LoadScene((int)scene);
        }

        public void LoadScene(int sceneIndex)
        {
            Debug.Log($"[GameManager] LoadScene({sceneIndex}) - {GetSceneName(sceneIndex)}");

            EventBus.Publish(new SceneLoadStartedEvent
            {
                SceneIndex = sceneIndex,
                SceneName = GetSceneName(sceneIndex)
            });

            // Enqueue parent scope so the new scene's LifetimeScope becomes a child
            // This allows scene-specific scopes to resolve ISceneLoader from root
            if (_rootScope != null)
            {
                Debug.Log($"[GameManager] EnqueueParent({_rootScope.name}) - Scene LifetimeScopes will inherit from this");
                using (LifetimeScope.EnqueueParent(_rootScope))
                {
                    SceneManager.LoadScene(sceneIndex);
                }
            }
            else
            {
                Debug.LogError("[GameManager] Root scope is NULL! DI hierarchy broken. BackButton won't work.");
                SceneManager.LoadScene(sceneIndex);
            }
        }

        public void LoadScene(string sceneName)
        {
            EventBus.Publish(new SceneLoadStartedEvent
            {
                SceneIndex = -1,
                SceneName = sceneName
            });

            if (_rootScope != null)
            {
                using (LifetimeScope.EnqueueParent(_rootScope))
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        public void LoadMainMenu()
        {
            LoadScene(GameScene.MainMenu);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private string GetSceneName(int index)
        {
            return index switch
            {
                0 => "MainMenu",
                1 => "AceOfShadows",
                2 => "MagicWords",
                3 => "PhoenixFlame",
                _ => $"Scene_{index}"
            };
        }
    }
}
