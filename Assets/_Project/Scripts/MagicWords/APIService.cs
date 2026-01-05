using System;
using System.Collections;
using System.Collections.Generic;
using SoftGames.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Handles HTTP requests to the Magic Words API.
    /// WebGL-compatible using UnityWebRequest.
    /// Implements IAPIService for testability.
    /// </summary>
    public class APIService : MonoBehaviour, IAPIService
    {
        [SerializeField] private string endpoint =
            "https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords";
        [SerializeField] private float timeout = 10f;

        public event Action<List<ProcessedDialogue>> OnDataLoaded;
        public event Action<string> OnError;

        /// <summary>
        /// Fetch dialogue data from API.
        /// </summary>
        public void FetchDialogueData()
        {
            // Publish loading started event
            EventBus.Publish(new DialogueLoadStartedEvent());

            StartCoroutine(FetchDataCoroutine());
        }

        private IEnumerator FetchDataCoroutine()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(endpoint))
            {
                request.timeout = (int)timeout;

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.ProtocolError)
                {
                    string error = $"Failed to load data: {request.error}";
                    Debug.LogError($"API Error: {request.error}");

                    // Publish error event
                    EventBus.Publish(new DialogueLoadErrorEvent { ErrorMessage = error });

                    OnError?.Invoke(error);
                    yield break;
                }

                try
                {
                    string json = request.downloadHandler.text;
                    MagicWordsResponse response = JsonUtility.FromJson<MagicWordsResponse>(json);

                    if (response == null)
                    {
                        string error = "Invalid data format received";
                        EventBus.Publish(new DialogueLoadErrorEvent { ErrorMessage = error });
                        OnError?.Invoke(error);
                        yield break;
                    }

                    // Process the response - match dialogues with avatars
                    List<ProcessedDialogue> processed = ProcessResponse(response);

                    // Publish success event
                    EventBus.Publish(new DialogueLoadedEvent { DialogueCount = processed.Count });

                    OnDataLoaded?.Invoke(processed);
                }
                catch (Exception ex)
                {
                    string error = $"Failed to parse data: {ex.Message}";
                    Debug.LogError($"Parse error: {ex.Message}");

                    EventBus.Publish(new DialogueLoadErrorEvent { ErrorMessage = error });
                    OnError?.Invoke(error);
                }
            }
        }

        /// <summary>
        /// Process API response - match dialogue entries with avatar entries by name.
        /// </summary>
        private List<ProcessedDialogue> ProcessResponse(MagicWordsResponse response)
        {
            List<ProcessedDialogue> result = new List<ProcessedDialogue>();

            if (response.dialogue == null) return result;

            // Build avatar lookup by name
            Dictionary<string, AvatarEntry> avatarLookup = new Dictionary<string, AvatarEntry>();
            if (response.avatars != null)
            {
                foreach (var avatar in response.avatars)
                {
                    if (!string.IsNullOrEmpty(avatar.name) && !avatarLookup.ContainsKey(avatar.name))
                    {
                        avatarLookup[avatar.name] = avatar;
                    }
                }
            }

            // Match each dialogue entry with its avatar
            foreach (var dialogue in response.dialogue)
            {
                if (dialogue == null) continue;

                avatarLookup.TryGetValue(dialogue.name ?? "", out AvatarEntry avatar);
                result.Add(ProcessedDialogue.Create(dialogue, avatar));
            }

            return result;
        }
    }
}
