using UnityEngine;

namespace SoftGames.Core
{
    /// <summary>
    /// Demonstrates EventBus subscription pattern.
    /// Logs game events for debugging and analytics.
    /// </summary>
    public class GameEventLogger : MonoBehaviour
    {
        [SerializeField] private bool enableLogging = true;

        private void OnEnable()
        {
            // Subscribe to all game events
            EventBus.Subscribe<SceneLoadStartedEvent>(OnSceneLoadStarted);
            EventBus.Subscribe<SceneLoadedEvent>(OnSceneLoaded);
            EventBus.Subscribe<CardMoveStartedEvent>(OnCardMoveStarted);
            EventBus.Subscribe<CardMoveCompletedEvent>(OnCardMoveCompleted);
            EventBus.Subscribe<AllCardsMovedEvent>(OnAllCardsMoved);
            EventBus.Subscribe<FireColorChangedEvent>(OnFireColorChanged);
            EventBus.Subscribe<DialogueLoadStartedEvent>(OnDialogueLoadStarted);
            EventBus.Subscribe<DialogueLoadedEvent>(OnDialogueLoaded);
            EventBus.Subscribe<DialogueLoadErrorEvent>(OnDialogueLoadError);
            EventBus.Subscribe<DialogueDisplayedEvent>(OnDialogueDisplayed);
        }

        private void OnDisable()
        {
            // Unsubscribe to prevent memory leaks
            EventBus.Unsubscribe<SceneLoadStartedEvent>(OnSceneLoadStarted);
            EventBus.Unsubscribe<SceneLoadedEvent>(OnSceneLoaded);
            EventBus.Unsubscribe<CardMoveStartedEvent>(OnCardMoveStarted);
            EventBus.Unsubscribe<CardMoveCompletedEvent>(OnCardMoveCompleted);
            EventBus.Unsubscribe<AllCardsMovedEvent>(OnAllCardsMoved);
            EventBus.Unsubscribe<FireColorChangedEvent>(OnFireColorChanged);
            EventBus.Unsubscribe<DialogueLoadStartedEvent>(OnDialogueLoadStarted);
            EventBus.Unsubscribe<DialogueLoadedEvent>(OnDialogueLoaded);
            EventBus.Unsubscribe<DialogueLoadErrorEvent>(OnDialogueLoadError);
            EventBus.Unsubscribe<DialogueDisplayedEvent>(OnDialogueDisplayed);
        }

        private void Log(string message)
        {
            if (enableLogging)
            {
                Debug.Log($"[Event] {message}");
            }
        }

        // Scene Events
        private void OnSceneLoadStarted(SceneLoadStartedEvent e) =>
            Log($"Scene loading: {e.SceneName} (index: {e.SceneIndex})");

        private void OnSceneLoaded(SceneLoadedEvent e) =>
            Log($"Scene loaded: {e.SceneName}");

        // Ace of Shadows Events
        private void OnCardMoveStarted(CardMoveStartedEvent e) =>
            Log($"Card {e.CardIndex} moving: Stack {e.FromStackId} -> {e.ToStackId}");

        private void OnCardMoveCompleted(CardMoveCompletedEvent e) =>
            Log($"Card {e.CardIndex} arrived at Stack {e.ToStackId}. Remaining: {e.RemainingCards}");

        private void OnAllCardsMoved(AllCardsMovedEvent e) =>
            Log($"All {e.TotalCardsMoved} cards moved!");

        // Phoenix Flame Events
        private void OnFireColorChanged(FireColorChangedEvent e) =>
            Log($"Fire color changed to {e.ColorName} (index: {e.ColorIndex})");

        // Magic Words Events
        private void OnDialogueLoadStarted(DialogueLoadStartedEvent e) =>
            Log("Loading dialogue data...");

        private void OnDialogueLoaded(DialogueLoadedEvent e) =>
            Log($"Loaded {e.DialogueCount} dialogue entries");

        private void OnDialogueLoadError(DialogueLoadErrorEvent e) =>
            Log($"Dialogue load error: {e.ErrorMessage}");

        private void OnDialogueDisplayed(DialogueDisplayedEvent e) =>
            Log($"Displaying dialogue {e.Index}: {e.CharacterName}");
    }
}
