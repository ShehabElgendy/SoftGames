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

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void LoadAceOfShadows()
        {
            _sceneLoader.LoadScene((int)GameManager.GameScene.AceOfShadows);
        }

        public void LoadMagicWords()
        {
            _sceneLoader.LoadScene((int)GameManager.GameScene.MagicWords);
        }

        public void LoadPhoenixFlame()
        {
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
