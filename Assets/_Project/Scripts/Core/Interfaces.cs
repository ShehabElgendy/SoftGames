using System;
using UnityEngine;

namespace SoftGames.Core
{
    /// <summary>
    /// Core interfaces that have no domain-specific dependencies.
    /// Domain-specific interfaces are in their respective assemblies.
    /// </summary>

    /// <summary>
    /// Interface for scene management.
    /// </summary>
    public interface ISceneLoader
    {
        void LoadScene(int sceneIndex);
        void LoadScene(string sceneName);
        void LoadMainMenu();
    }
}
