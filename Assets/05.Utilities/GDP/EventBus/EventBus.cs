using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDP.EventBus
{
    public class EventBus
    {
        private static readonly IDictionary<EventType, EventBusEvent> Events =
            new Dictionary<EventType, EventBusEvent>();

        public static void Subscribe(EventType eventType, UnityAction listener)
        {
            EventBusEvent thisEvt;
            if (Events.TryGetValue(eventType, out thisEvt))
            {
                thisEvt.AddListener(listener);
            }
            else
            {
                thisEvt = new EventBusEvent();
                thisEvt.AddListener(listener);
                Events.Add(eventType, thisEvt);
            }
        }

        public static void UnSubscribe(EventType eventType, UnityAction listener)
        {
            if (Events.TryGetValue(eventType, out var thisEvt))
            {
                if (thisEvt is { evt: not null })
                {
                    thisEvt.RemoveListener(listener);
                    if (thisEvt.EvtCount == 0)
                        Events.Remove(eventType);
                }
            }
        }

        public static void Publish(EventType eventType)
        {
            if (Events.TryGetValue(eventType, out var thisEvt))
            {
                thisEvt?.Invoke();
            }
        }
    }
}

public class EventBusEvent
{
    public int EvtCount { get; private set; }
    public UnityEvent evt;

    public void AddListener(UnityAction listener)
    {
        EvtCount++;
        evt.AddListener(listener);
    }

    public void RemoveListener(UnityAction listener)
    {
        EvtCount--;
        evt.RemoveListener(listener);
    }

    public void Invoke()
    {
        evt.Invoke();
    }

    public EventBusEvent()
    {
        evt = new UnityEvent();
        EvtCount = 0;
    }
}