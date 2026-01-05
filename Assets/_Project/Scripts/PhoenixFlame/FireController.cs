using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using SoftGames.Core;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Controls the fire particle system.
    /// Implements IFireController for testability.
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class FireController : MonoBehaviour, IFireController
    {
        private ParticleSystem fireParticles;
        private ParticleSystem.MainModule mainModule;
        private ParticleSystem.ColorOverLifetimeModule colorModule;

        private Tweener currentColorTween;

        // Cached to avoid GC allocation on every color change
        private Gradient cachedGradient;
        private GradientColorKey[] cachedColorKeys;
        private GradientAlphaKey[] cachedAlphaKeys;

        private void Awake()
        {
            fireParticles = GetComponent<ParticleSystem>();
            mainModule = fireParticles.main;
            colorModule = fireParticles.colorOverLifetime;

            // Pre-allocate gradient and keys
            cachedGradient = new Gradient();
            cachedColorKeys = new GradientColorKey[3];
            cachedAlphaKeys = new GradientAlphaKey[3];
        }

        /// <summary>
        /// Set the fire's base color immediately.
        /// </summary>
        public void SetFireColor(Color color)
        {
            // Update start color
            mainModule.startColor = color;

            // Update color over lifetime gradient
            UpdateColorGradient(color);
        }

        /// <summary>
        /// Smoothly transition to a new color.
        /// </summary>
        public void TransitionToColor(Color targetColor, float duration, Action onComplete = null)
        {
            // Kill any existing tween
            currentColorTween?.Kill();

            Color startColor = mainModule.startColor.color;

            currentColorTween = DOTween.To(
                () => startColor,
                x => SetFireColor(x),
                targetColor,
                duration
            )
            .SetEase(Ease.InOutSine)
            .OnComplete(() => onComplete?.Invoke());
        }

        /// <summary>
        /// Get current fire color.
        /// </summary>
        public Color GetCurrentColor()
        {
            return mainModule.startColor.color;
        }

        private void UpdateColorGradient(Color baseColor)
        {
            // Reuse cached gradient to avoid GC allocation
            Color midColor = baseColor * 0.7f;
            midColor.a = 0.8f;

            Color endColor = Color.black;
            endColor.a = 0f;

            // Update cached keys
            cachedColorKeys[0] = new GradientColorKey(baseColor, 0f);
            cachedColorKeys[1] = new GradientColorKey(midColor, 0.5f);
            cachedColorKeys[2] = new GradientColorKey(endColor, 1f);

            cachedAlphaKeys[0] = new GradientAlphaKey(1f, 0f);
            cachedAlphaKeys[1] = new GradientAlphaKey(0.6f, 0.5f);
            cachedAlphaKeys[2] = new GradientAlphaKey(0f, 1f);

            cachedGradient.SetKeys(cachedColorKeys, cachedAlphaKeys);

            colorModule.enabled = true;
            colorModule.color = cachedGradient;
        }

        private void OnDestroy()
        {
            currentColorTween?.Kill();
        }
    }
}
