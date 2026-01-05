using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SoftGames.Core;

namespace SoftGames.MagicWords
{
    /// <summary>
    /// Async image loader with caching and fallback support.
    /// Implements IAvatarLoader for testability.
    /// Includes proper memory management for WebGL.
    /// </summary>
    public class AvatarLoader : MonoBehaviour, IAvatarLoader
    {
        [SerializeField] private Sprite placeholderSprite;
        [SerializeField] private float loadTimeout = 5f;

        private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

        /// <summary>
        /// Load avatar image and set to target Image component.
        /// Falls back to placeholder on failure.
        /// </summary>
        public void LoadAvatar(string url, Image targetImage, Action onComplete = null)
        {
            if (targetImage == null)
            {
                onComplete?.Invoke();
                return;
            }

            if (string.IsNullOrEmpty(url))
            {
                SetPlaceholder(targetImage);
                onComplete?.Invoke();
                return;
            }

            // Check cache first
            if (spriteCache.TryGetValue(url, out Sprite cached))
            {
                targetImage.sprite = cached;
                onComplete?.Invoke();
                return;
            }

            StartCoroutine(LoadAvatarCoroutine(url, targetImage, onComplete));
        }

        private IEnumerator LoadAvatarCoroutine(string url, Image targetImage, Action onComplete)
        {
            // Set placeholder while loading
            SetPlaceholder(targetImage);

            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                request.timeout = (int)loadTimeout;

                yield return request.SendWebRequest();

                // Check if target was destroyed during load
                if (targetImage == null)
                {
                    onComplete?.Invoke();
                    yield break;
                }

                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning($"Failed to load avatar: {url} - {request.error}");
                    // Placeholder already set
                }
                else
                {
                    try
                    {
                        Texture2D texture = DownloadHandlerTexture.GetContent(request);
                        Sprite sprite = Sprite.Create(
                            texture,
                            new Rect(0, 0, texture.width, texture.height),
                            new Vector2(0.5f, 0.5f),
                            100f
                        );

                        // Destroy existing cached sprite to prevent memory leak
                        if (spriteCache.TryGetValue(url, out Sprite existingSprite))
                        {
                            if (existingSprite != null)
                            {
                                if (existingSprite.texture != null)
                                    Destroy(existingSprite.texture);
                                Destroy(existingSprite);
                            }
                        }

                        // Cache the sprite
                        spriteCache[url] = sprite;

                        // Check again if target still exists
                        if (targetImage != null)
                        {
                            targetImage.sprite = sprite;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"Failed to create sprite: {ex.Message}");
                        // Placeholder already set
                    }
                }

                onComplete?.Invoke();
            }
        }

        private void SetPlaceholder(Image targetImage)
        {
            if (placeholderSprite != null && targetImage != null)
            {
                targetImage.sprite = placeholderSprite;
            }
        }

        /// <summary>
        /// Clear the sprite cache and release memory.
        /// </summary>
        public void ClearCache()
        {
            foreach (var kvp in spriteCache)
            {
                if (kvp.Value != null)
                {
                    if (kvp.Value.texture != null)
                    {
                        Destroy(kvp.Value.texture);
                    }
                    Destroy(kvp.Value);
                }
            }
            spriteCache.Clear();
        }

        private void OnDestroy()
        {
            ClearCache();
        }
    }
}
