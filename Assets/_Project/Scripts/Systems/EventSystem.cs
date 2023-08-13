using System;
using System.Collections.Generic;
using Utils;

namespace dragoni7
{
    public class EventSystem : Singleton<EventSystem>
    {
        private Dictionary<Events, Action<Dictionary<string, object>>> _eventDictionary;
        protected override void Awake()
        {
            _eventDictionary = new();
            base.Awake();
        }

        /// <summary>
        /// Subscribes a function to an event
        /// </summary>
        /// <param name="eventId">Id of event to listen for</param>
        /// <param name="listener">function to be invoked</param>
        public void StartListening(Events eventId, Action<Dictionary<string, object>> listener)
        {

            if (Instance._eventDictionary.TryGetValue(eventId, out Action<Dictionary<string, object>> thisEvent))
            {
                thisEvent += listener;
                Instance._eventDictionary[eventId] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                Instance._eventDictionary.Add(eventId, listener);
            }
        }

        /// <summary>
        /// Unsubscribes a function to an event
        /// </summary>
        /// <param name="eventId">Id of the event to stop listening for</param>
        /// <param name="listener">function to stop listening</param>
        public void StopListening(Events eventId, Action<Dictionary<string, object>> listener)
        {
            if (Instance == null)
            {
                return;
            }

            if (Instance._eventDictionary.TryGetValue(eventId, out Action<Dictionary<string, object>> thisEvent))
            {
                thisEvent -= listener;
                Instance._eventDictionary[eventId] = thisEvent;
            }
        }
        /// <summary>
        /// Invokes an event
        /// </summary>
        /// <param name="eventId">Id of the event</param>
        /// <param name="message">Data to pass to listeners</param>
        public void TriggerEvent(Events eventId, Dictionary<string, object> message)
        {
            if (Instance._eventDictionary.TryGetValue(eventId, out Action<Dictionary<string, object>> thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }
    }
}
