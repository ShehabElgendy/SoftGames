using System;
using System.Collections.Generic;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Data models for Magic Words API response.
    /// IMPORTANT: The API returns separate arrays for dialogue and avatars!
    /// </summary>
    [Serializable]
    public class MagicWordsResponse
    {
        public List<DialogueEntry> dialogue;
        public List<AvatarEntry> avatars;
    }

    [Serializable]
    public class DialogueEntry
    {
        public string name;
        public string text;  // Contains emotion tags like {satisfied}
    }

    [Serializable]
    public class AvatarEntry
    {
        public string name;
        public string url;
        public string position;  // "left" or "right"
    }

    /// <summary>
    /// Combined dialogue data after processing.
    /// </summary>
    public class ProcessedDialogue
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsLeftPosition { get; set; }

        public static ProcessedDialogue Create(DialogueEntry dialogue, AvatarEntry avatar)
        {
            return new ProcessedDialogue
            {
                Name = dialogue?.name ?? "Unknown",
                Text = dialogue?.text ?? "...",
                AvatarUrl = avatar?.url,
                IsLeftPosition = avatar?.position != "right"
            };
        }
    }
}
