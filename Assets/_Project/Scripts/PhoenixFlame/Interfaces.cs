using System;
using UnityEngine;

namespace SoftGames.PhoenixFlame
{
    /// <summary>
    /// Interface for fire color control.
    /// Allows mocking in tests.
    /// </summary>
    public interface IFireController
    {
        void SetFireColor(Color color);
        void TransitionToColor(Color targetColor, float duration, Action onComplete = null);
        Color GetCurrentColor();
    }

    /// <summary>
    /// Interface for color cycling.
    /// </summary>
    public interface IColorCycler
    {
        int CurrentColorIndex { get; }
        bool IsTransitioning { get; }
        void CycleColor();
        void SetColorImmediate(int index);
    }
}
