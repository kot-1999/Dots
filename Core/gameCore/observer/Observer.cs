using System;

namespace dots.observer
{
    public interface Observer
    {
        public void Subscribe(Subscriber s);
        public void DeleteSubscriber(Subscriber s);
        public void DeleteAll();
        public void Notify(Subscriber s);
        public void NotifyAll();
    }
}