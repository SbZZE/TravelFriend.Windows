using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelFriend.Windows.Database;

namespace TravelFriend.Windows.RabbitMQ
{
    public class RabbitMQManager
    {
        private static IModel Channel;
        /// <summary>
        /// 连接RMQ
        /// </summary>
        public static void Connection()
        {
            IConnectionFactory connFactory = new ConnectionFactory//创建连接工厂对象
            {
                HostName = "47.106.139.187",//IP地址
                Port = 5672,//端口号
                UserName = "root",//用户账号
                Password = "root"//用户密码
            };
            IConnection conn = connFactory.CreateConnection();
            Channel = conn.CreateModel();
            //交换机名称
            String exchangeName = "fanoutExchange";
            //声明交换机
            Channel.ExchangeDeclare(exchange: exchangeName, type: "fanout", durable: true);
            //消息队列名称
            String queueName = "fanout.pc";
            //声明队列
            Channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            //将队列与交换机进行绑定
            Channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");
            //声明为手动确认
            Channel.BasicQos(0, 1, false);
            //定义消费者
            var consumer = new EventingBasicConsumer(Channel);
            //接收事件
            consumer.Received += Consumer_Received;
            //开启监听
            Channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var messageStr = Encoding.UTF8.GetString(e.Body.Span);
            var message = JsonConvert.DeserializeObject<MessageModel>(messageStr);
            if (message.Account == AccountManager.Instance.Account)
            {
                switch (message.Type)
                {
                    case MessageType.AVATAR:
                        NotifyManager.UpdateUserAvatar(message.Account);
                        break;
                    case MessageType.INFO:
                        NotifyManager.UpdateUserInfo(message.Account);
                        break;
                }
            }
            //返回消息确认
            Channel.BasicAck(e.DeliveryTag, true);
        }
    }
}
