using SoftGames.Core;
using UnityEngine;
using VContainer;

namespace SoftGames.UI
{
    /// <summary>
    /// Main menu navigation.
    /// Attach to main menu canvas or manager object.
    /// Uses VContainer for dependency injection.
    /// </summary>
    public class MainMenuUI : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        private bool _isInjected;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _isInjected = true;
        }

        private bool ValidateInjection()
        {
            if (!_isInjected || _sceneLoader == null)
            {
                Debug.LogError("[MainMenuUI] SceneLoader not injected! Check VContainer setup.");
                return false;
            }
            return true;
        }

        public void LoadAceOfShadows()
        {
            if (!ValidateInjection()) return;
            _sceneLoader.LoadScene((int)GameManager.GameScene.AceOfShadows);
        }

        public void LoadMagicWords()
        {
            if (!ValidateInjection()) return;
            _sceneLoader.LoadScene((int)GameManager.GameScene.MagicWords);
        }

        public void LoadPhoenixFlame()
        {
            if (!ValidateInjection()) return;
            _sceneLoader.LoadScene((int)GameManager.GameScene.PhoenixFlame);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
