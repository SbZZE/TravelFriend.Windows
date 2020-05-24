using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.RabbitMQ.Observe
{
    public abstract class Subject
    {
        private List<IObserver> Observers = new List<IObserver>();

        public void Add(IObserver observer) => Observers.Add(observer);

        public void Remove(IObserver observer) => Observers.Remove(observer);

        public void Notify()
        {
            foreach (var o in Observers)
                o.Update();
        }
    }
}
