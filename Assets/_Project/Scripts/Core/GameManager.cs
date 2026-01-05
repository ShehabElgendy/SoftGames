using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoftGames.Core
{
    /// <summary>
    /// Global game manager. Handles scene transitions and persistent state.
    /// Uses EventBus for decoupled scene change notifications.
    /// Registered via VContainer - no singleton pattern.
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

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
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
            EventBus.Publish(new SceneLoadStartedEvent
            {
                SceneIndex = sceneIndex,
                SceneName = GetSceneName(sceneIndex)
            });
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadScene(string sceneName)
        {
            EventBus.Publish(new SceneLoadStartedEvent
            {
                SceneIndex = -1,
                SceneName = sceneName
            });
            SceneManager.LoadScene(sceneName);
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
