using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoftGames.Core
{
    /// <summary>
    /// Generic Event Bus for decoupled communication between systems.
    /// Supports both global events and typed event channels.
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> _eventHandlers = new();

        /// <summary>
        /// Subscribe to an event type.
        /// </summary>
        public static void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            Type eventType = typeof(T);

            if (_eventHandlers.TryGetValue(eventType, out Delegate existing))
            {
                _eventHandlers[eventType] = Delegate.Combine(existing, handler);
            }
            else
            {
                _eventHandlers[eventType] = handler;
            }
        }

        /// <summary>
        /// Unsubscribe from an event type.
        /// </summary>
        public static void Unsubscribe<T>(Action<T> handler) where T : IEvent
        {
            Type eventType = typeof(T);

            if (_eventHandlers.TryGetValue(eventType, out Delegate existing))
            {
                Delegate newDelegate = Delegate.Remove(existing, handler);
                if (newDelegate == null)
                {
                    _eventHandlers.Remove(eventType);
                }
                else
                {
                    _eventHandlers[eventType] = newDelegate;
                }
            }
        }

        /// <summary>
        /// Publish an event to all subscribers.
        /// </summary>
        public static void Publish<T>(T eventData) where T : IEvent
        {
            Type eventType = typeof(T);

            if (_eventHandlers.TryGetValue(eventType, out Delegate handler))
            {
                (handler as Action<T>)?.Invoke(eventData);
            }
        }

        /// <summary>
        /// Clear all event subscriptions.
        /// Call this when changing scenes if needed.
        /// </summary>
        public static void Clear()
        {
            _eventHandlers.Clear();
        }

        /// <summary>
        /// Clear subscriptions for a specific event type.
        /// </summary>
        public static void Clear<T>() where T : IEvent
        {
            _eventHandlers.Remove(typeof(T));
        }
    }

    /// <summary>
    /// Marker interface for all events.
    /// </summary>
    public interface IEvent { }

    // ============================================
    // GAME EVENTS
    // ============================================

    /// <summary>
    /// Fired when a scene starts loading.
    /// </summary>
    public struct SceneLoadStartedEvent : IEvent
    {
        public int SceneIndex;
        public string SceneName;
    }

    /// <summary>
    /// Fired when a scene finishes loading.
    /// </summary>
    public struct SceneLoadedEvent : IEvent
    {
        public int SceneIndex;
        public string SceneName;
    }

    // ============================================
    // ACE OF SHADOWS EVENTS
    // ============================================

    /// <summary>
    /// Fired when a card starts moving.
    /// </summary>
    public struct CardMoveStartedEvent : IEvent
    {
        public int CardIndex;
        public int FromStackId;
        public int ToStackId;
    }

    /// <summary>
    /// Fired when a card finishes moving.
    /// </summary>
    public struct CardMoveCompletedEvent : IEvent
    {
        public int CardIndex;
        public int ToStackId;
        public int RemainingCards;
    }

    /// <summary>
    /// Fired when all card animations are complete.
    /// </summary>
    public struct AllCardsMovedEvent : IEvent
    {
        public int TotalCardsMoved;
    }

    // ============================================
    // MAGIC WORDS EVENTS
    // ============================================

    /// <summary>
    /// Fired when dialogue data loading starts.
    /// </summary>
    public struct DialogueLoadStartedEvent : IEvent { }

    /// <summary>
    /// Fired when dialogue data is successfully loaded.
    /// </summary>
    public struct DialogueLoadedEvent : IEvent
    {
        public int DialogueCount;
    }

    /// <summary>
    /// Fired when dialogue loading fails.
    /// </summary>
    public struct DialogueLoadErrorEvent : IEvent
    {
        public string ErrorMessage;
    }

    /// <summary>
    /// Fired when a dialogue entry is displayed.
    /// </summary>
    public struct DialogueDisplayedEvent : IEvent
    {
        public int Index;
        public string CharacterName;
    }

    // ============================================
    // PHOENIX FLAME EVENTS
    // ============================================

    /// <summary>
    /// Fired when fire color change is requested.
    /// </summary>
    public struct FireColorChangeRequestedEvent : IEvent
    {
        public int ColorIndex;
        public string ColorName;
    }

    /// <summary>
    /// Fired when fire color transition completes.
    /// </summary>
    public struct FireColorChangedEvent : IEvent
    {
        public int ColorIndex;
        public string ColorName;
        public UnityEngine.Color Color;
    }
}
