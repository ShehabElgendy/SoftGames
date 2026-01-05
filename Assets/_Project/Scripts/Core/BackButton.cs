using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace SoftGames.Core
{
    /// <summary>
    /// Back button to return to main menu.
    /// Auto-wires to Button component on same GameObject.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class BackButton : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        private Button _button;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            Debug.Log($"[BackButton] Inject SUCCESS - Got ISceneLoader: {sceneLoader?.GetType().Name ?? "NULL"}");
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(GoBack);
            Debug.Log("[BackButton] Auto-wired to Button.onClick");
        }

        private void Start()
        {
            if (_sceneLoader == null)
            {
                Debug.LogError("[BackButton] ISceneLoader is NULL! Register this component in scene LifetimeScope.");
            }
            else
            {
                Debug.Log("[BackButton] Ready");
            }
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(GoBack);
            }
        }

        public void GoBack()
        {
            Debug.Log("[BackButton] GoBack() called");
            if (_sceneLoader != null)
            {
                _sceneLoader.LoadMainMenu();
            }
            else
            {
                Debug.LogError("[BackButton] Cannot go back - ISceneLoader is NULL!");
            }
        }
    }
}
