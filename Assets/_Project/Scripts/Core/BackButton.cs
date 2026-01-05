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
            button.onClick.AddListener(GoBack);
        }

        private void Start()
        {
            if (sceneLoader == null)
            {
                Debug.LogError("[BackButton] ISceneLoader is NULL! Register this component in scene LifetimeScope.");
            }
        }

        private void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(GoBack);
            }
        }

        public void GoBack()
        {
            if (sceneLoader != null)
            {
                sceneLoader.LoadMainMenu();
            }
            else
            {
                Debug.LogError("[BackButton] Cannot go back - ISceneLoader is NULL!");
            }
        }
    }
}
