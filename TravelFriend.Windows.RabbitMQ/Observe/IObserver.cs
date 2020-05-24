using System;
using System.Collections.Generic;
using System.Text;

namespace TravelFriend.Windows.RabbitMQ.Observe
{
    public interface IObserver
    {
        public void Update();
    }
}
