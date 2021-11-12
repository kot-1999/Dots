using System;
using System.Collections.Generic;

namespace dots.observer
{
    [Serializable]
    public class SimplyObserver : Observer
    {
        private List<Subscriber> subscribers;

        public SimplyObserver()
        {
            subscribers = new List<Subscriber>();
        }


        public void Subscribe(Subscriber s)
        {
            if (s != null)
                subscribers.Add(s);
        }

        public void DeleteSubscriber(Subscriber s)
        {
            subscribers.Remove(s);
        }

        public void DeleteAll()
        {
            subscribers.Clear();
        }

     

        public void Notify(Subscriber s)
        {
            subscribers[subscribers.IndexOf(s)].notify();
        }

        public void NotifyAll()
        {
            foreach (Subscriber s in subscribers)
                s.notify();
        }
    }
}