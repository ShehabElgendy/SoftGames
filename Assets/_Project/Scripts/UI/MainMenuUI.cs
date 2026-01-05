using UnityEngine;

namespace SoftGames.UI
{
    /// <summary>
    /// Main menu container.
    /// Individual buttons use SceneButton and QuitButton components.
    /// This class is kept for any shared menu logic if needed.
    /// </summary>
    public class MainMenuUI : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("[MainMenuUI] Ready");
        }
    }
}
