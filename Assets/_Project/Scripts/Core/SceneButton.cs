using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace SoftGames.Core
{
    /// <summary>
    /// Generic scene loader button.
    /// Attach to any Button, set target scene in Inspector.
    /// Auto-wires to Button.onClick.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class SceneButton : MonoBehaviour
    {
        [SerializeField] private GameManager.GameScene targetScene;

        private ISceneLoader _sceneLoader;
        private Button _button;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            Debug.Log($"[SceneButton:{targetScene}] Inject SUCCESS");
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClick);
            }
        }

        private void OnClick()
        {
            Debug.Log($"[SceneButton] Loading {targetScene}");
            _sceneLoader?.LoadScene((int)targetScene);
        }
    }
}
