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

        private void Awake()
        {
            fireParticles = GetComponent<ParticleSystem>();
            mainModule = fireParticles.main;
            colorModule = fireParticles.colorOverLifetime;
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
            // Create gradient from base color to darker version to black
            Gradient gradient = new Gradient();

            Color midColor = baseColor * 0.7f;
            midColor.a = 0.8f;

            Color endColor = Color.black;
            endColor.a = 0f;

            gradient.SetKeys(
                new GradientColorKey[]
                {
                    new GradientColorKey(baseColor, 0f),
                    new GradientColorKey(midColor, 0.5f),
                    new GradientColorKey(endColor, 1f)
                },
                new GradientAlphaKey[]
                {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(0.6f, 0.5f),
                    new GradientAlphaKey(0f, 1f)
                }
            );

            colorModule.enabled = true;
            colorModule.color = gradient;
        }

        private void OnDestroy()
        {
            currentColorTween?.Kill();
        }
    }
}
