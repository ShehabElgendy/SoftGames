using System;
using System.Collections.Generic;

namespace SoftGames.Core
{
    /// <summary>
    /// Marker interface for all events.
    /// </summary>
    public interface IEvent { }

    /// <summary>
    /// Generic Event Bus for decoupled communication between systems.
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> eventHandlers = new();

        public static void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            var eventType = typeof(T);

            if (eventHandlers.TryGetValue(eventType, out var existing))
            {
                eventHandlers[eventType] = Delegate.Combine(existing, handler);
            }
            else
            {
                eventHandlers[eventType] = handler;
            }
        }

        public static void Unsubscribe<T>(Action<T> handler) where T : IEvent
        {
            var eventType = typeof(T);

            if (eventHandlers.TryGetValue(eventType, out var existing))
            {
                var newDelegate = Delegate.Remove(existing, handler);
                if (newDelegate == null)
                {
                    eventHandlers.Remove(eventType);
                }
                else
                {
                    eventHandlers[eventType] = newDelegate;
                }
            }
        }

        public static void Publish<T>(T eventData) where T : IEvent
        {
            if (eventHandlers.TryGetValue(typeof(T), out var handler))
            {
                (handler as Action<T>)?.Invoke(eventData);
            }
        }

        public static void Clear()
        {
            eventHandlers.Clear();
        }

        public static void Clear<T>() where T : IEvent
        {
            eventHandlers.Remove(typeof(T));
        }
    }
}
