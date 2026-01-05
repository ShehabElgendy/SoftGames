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

        private ISceneLoader sceneLoader;
        private Button button;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(OnClick);
            }
        }

        private void OnClick()
        {
            sceneLoader?.LoadScene((int)targetScene);
        }
    }
}
