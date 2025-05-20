using System.Collections.Generic;
using UnityEngine.Events;

namespace N.Patterns
{
    public class EventDispatcher
    {
        static Dictionary<EventId, UnityAction<object>> listener = new();

        internal static bool HasExistEvent(EventId id) => listener.ContainsKey(id);

        internal static void Register(EventId id, UnityAction<object> callback)
        {
            if (!HasExistEvent(id))
                listener.Add(id, callback);
            else
                listener[id] += callback;
        }

        internal static void Push(EventId id, object data = null)
        {
            if (!HasExistEvent(id)) return;
            listener[id]?.Invoke(data);
        }

        internal static void RemoveCallback(EventId id, UnityAction<object> callback)
        {
            if (!HasExistEvent(id)) return;
            listener[id] -= callback;

            if (listener[id] == null)
                RemoveEvent(id);
        }

        internal static void RemoveEvent(EventId id)
        {
            if (!HasExistEvent(id)) return;
            listener.Remove(id);
        }

        internal static void RemoveAll() => listener.Clear();
    }
}