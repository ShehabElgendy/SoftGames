using UnityEngine;
using UnityEngine.UI;

namespace SoftGames.Core
{
    /// <summary>
    /// Quit game button.
    /// Auto-wires to Button.onClick.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class QuitButton : MonoBehaviour
    {
        private Button _button;

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
            Debug.Log("[QuitButton] Quitting game");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
