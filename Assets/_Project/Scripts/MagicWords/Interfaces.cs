using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Interface for API data fetching.
    /// Allows mocking in tests.
    /// </summary>
    public interface IAPIService
    {
        event Action<List<ProcessedDialogue>> OnDataLoaded;
        event Action<string> OnError;
        void FetchDialogueData();
    }

    /// <summary>
    /// Interface for avatar loading.
    /// </summary>
    public interface IAvatarLoader
    {
        void LoadAvatar(string url, Image targetImage, Action onComplete = null);
        void ClearCache();
    }

    /// <summary>
    /// Interface for dialogue rendering.
    /// </summary>
    public interface IDialogueRenderer
    {
        GameObject CreateDialogueBox(ProcessedDialogue data);
        void ClearDialogue();
    }
}
