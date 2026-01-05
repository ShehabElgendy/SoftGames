using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using SoftGames.Core;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Main orchestrator for Magic Words.
    /// Coordinates API calls, loading states, and dialogue display.
    /// Uses EventBus for decoupled communication.
    /// </summary>
    public class MagicWordsManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private APIService apiService;
        [SerializeField] private AvatarLoader avatarLoader;
        [SerializeField] private Transform dialogueContainer;
        [SerializeField] private GameObject dialogueBoxPrefab;

        [Header("UI States")]
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject errorPanel;
        [SerializeField] private TextMeshProUGUI errorText;

        [Header("Animation")]
        [SerializeField] private float dialogueSpawnDelay = 0.2f;

        private void OnEnable()
        {
            if (apiService != null)
            {
                apiService.OnDataLoaded += HandleDataLoaded;
                apiService.OnError += HandleError;
            }
        }

        private void OnDisable()
        {
            if (apiService != null)
            {
                apiService.OnDataLoaded -= HandleDataLoaded;
                apiService.OnError -= HandleError;
            }
        }

        private void Start()
        {
            LoadData();
        }

        /// <summary>
        /// Start loading data from API.
        /// </summary>
        public void LoadData()
        {
            ShowLoading(true);
            HideError();
            ClearDialogue();

            if (apiService != null)
            {
                apiService.FetchDialogueData();
            }
            else
            {
                HandleError("API Service not configured");
            }
        }

        private void HandleDataLoaded(List<ProcessedDialogue> dialogues)
        {
            ShowLoading(false);
            StartCoroutine(DisplayDialogueSequence(dialogues));
        }

        private void HandleError(string error)
        {
            ShowLoading(false);
            ShowError(error);
        }

        /// <summary>
        /// Display dialogue entries with staggered animation.
        /// </summary>
        private IEnumerator DisplayDialogueSequence(List<ProcessedDialogue> dialogues)
        {
            int index = 0;
            foreach (var data in dialogues)
            {
                if (data == null) continue;

                GameObject box = CreateDialogueBox(data);
                AnimateDialogueEntry(box);

                // Publish event for each dialogue displayed
                EventBus.Publish(new DialogueDisplayedEvent
                {
                    Index = index,
                    CharacterName = data.Name
                });

                index++;
                yield return new WaitForSeconds(dialogueSpawnDelay);
            }
        }

        private GameObject CreateDialogueBox(ProcessedDialogue data)
        {
            GameObject box = Instantiate(dialogueBoxPrefab, dialogueContainer);

            DialogueBoxUI ui = box.GetComponent<DialogueBoxUI>();
            if (ui != null)
            {
                ui.Setup(data);

                // Load avatar
                if (avatarLoader != null)
                {
                    avatarLoader.LoadAvatar(data.AvatarUrl, ui.AvatarImage);
                }
            }

            return box;
        }

        private void AnimateDialogueEntry(GameObject box)
        {
            CanvasGroup canvasGroup = box.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = box.AddComponent<CanvasGroup>();
            }

            // Fade in animation
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, 0.3f);

            // Slide in animation
            RectTransform rect = box.GetComponent<RectTransform>();
            if (rect != null)
            {
                Vector2 originalPos = rect.anchoredPosition;
                rect.anchoredPosition = new Vector2(originalPos.x, originalPos.y - 50f);
                rect.DOAnchorPosY(originalPos.y, 0.3f).SetEase(Ease.OutQuad);
            }
        }

        private void ClearDialogue()
        {
            if (dialogueContainer != null)
            {
                foreach (Transform child in dialogueContainer)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        private void ShowLoading(bool show)
        {
            if (loadingPanel != null)
                loadingPanel.SetActive(show);
        }

        private void ShowError(string message)
        {
            if (errorPanel != null)
            {
                errorPanel.SetActive(true);
                if (errorText != null)
                    errorText.text = message;
            }
        }

        private void HideError()
        {
            if (errorPanel != null)
                errorPanel.SetActive(false);
        }

        /// <summary>
        /// Retry loading data (called from Retry button).
        /// </summary>
        public void Retry()
        {
            LoadData();
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }
    }
}
