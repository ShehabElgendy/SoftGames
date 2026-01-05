using SoftGames.Core;
using UnityEngine;
using VContainer;

namespace SoftGames.UI
{
    /// <summary>
    /// Back button to return to main menu.
    /// Attach to back button GameObject.
    /// Uses VContainer for dependency injection.
    /// </summary>
    public class BackButton : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void GoBack()
        {
            _sceneLoader.LoadMainMenu();
        }
    }
}
