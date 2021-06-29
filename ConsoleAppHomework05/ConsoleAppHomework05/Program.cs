using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ConsoleAppHomework05
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            OrderDetails details1 = new OrderDetails("detail1", "user1", 123.22);
            OrderDetails details2 = new OrderDetails("detail2", "user2", 2323.22);
            OrderDetails details3 = new OrderDetails("detail3", "user3", 3423.22);
            OrderDetails details4 = new OrderDetails("detail4", "user4", 4523.22);

            Order order1 = new Order(1, details1);
            Order order2 = new Order(2, details2);
            Order order3 = new Order(3, details3);
            Order order4 = new Order(4, details4);
            // 这里实际上运用了返回值确定状态的方案，可以通过接受返回值来确定当前的订单状态
            // 为了演示提示内容就没有采用。正常情况下，应当以返回值+抛出异常实现。
            OrderService service = new OrderService();
            /*service.addOrder(order1);
            service.addOrder(order1);
            service.addOrder(order2);
            Console.WriteLine(service.ToString());
            service.modifyOrder(1, details4);
            service.modifyOrderDetail(details2, details3);
            service.modifyOrder(4, details4);
            Console.WriteLine("Not sorted: \n");
            Console.WriteLine(service.ToString());
            service.getOrders().Sort();
            Console.WriteLine("Sorted: \n");
            Console.WriteLine(service.ToString());
            // Using lambda, sorted by orderAmount
            service.getOrders().Sort((Order o1, Order o2) => (int)(o1.orderDetails.OrderAmount - o2.orderDetails.OrderAmount));
            Console.WriteLine(service.ToString());
            service.deleteOrder(1);
            service.deleteOrder(1);
            Console.WriteLine(service.ToString());*/

            // Rebuild service and use linq
            service = null;
            order1 = new Order(1, details1);
            order2 = new Order(2, details2);
            order3 = new Order(3, details3);
            order4 = new Order(4, details4);
            Order order5 = new Order(5, details1);
            Order order6 = new Order(6, details2);
            Order order7 = new Order(7, details3);
            Order order8 = new Order(8, details4);
            GC.Collect();
            service = new OrderService();
            // Nothing in list now
            Console.WriteLine(service.ToString());
            // Adding info
            service.addOrder(order1);
            service.addOrder(order2);
            service.addOrder(order3);
            service.addOrder(order5);
            service.addOrder(order6);
            service.addOrder(order7);
            service.addOrder(order8);
            service.addOrder(order4);
            Console.WriteLine(service.ToString());
            // List<Order> orderList = service.getOrders();
            /*IEnumerable<Order> orderQuery =
                from order in orderList
                where order.OrderId > 3 && order.orderDetails.OrderAmount > 1000
                orderby order.orderDetails.OrderUserName descending
                orderby order.OrderId descending
                select order;*/
            /* IEnumerable<Order> orderQuery =
                 from order in orderList
                 orderby order.orderDetails.OrderAmount descending, order.OrderId
                 select order;
             foreach (Order i in orderQuery)
             {
                 Console.WriteLine(i);
             }*/
            service.writeToFile();
            List<Order> newList = new List<Order>();
            for(int i = 1; i < 9; i++)
            {
                string filename = "Order_" + i + ".xml";
                Order obj = service.readFromFile(filename);
                newList.Add(obj);
            }
            Console.WriteLine(newList.Count);
            foreach(Order t in newList)
            {
                Console.WriteLine(t);
            }

        }
    }

    public class OrderService
    {
        private List<Order> myOrders = null;
        public List<Order> getOrders()
        {
            if (myOrders == null)
            {
                myOrders = new List<Order>();
                return myOrders;
            }
            else
            {
                return myOrders;
            }
        }
        public void writeToFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Order));
            List<Order> orders = getOrders();

            foreach(Order i in orders)
            {
                string filename = "Order_"+i.OrderId+".xml";
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, i);
                writer.Close();
            }
        }
        public Order readFromFile(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Order));
            FileStream stream = new FileStream(filename, FileMode.Open);
            Order order = (Order)serializer.Deserialize(stream);
            return order;
        }
        public OrderService()
        {
        }
        public int modifyOrder(int orderId, OrderDetails orderDetails)
        {
            foreach (Order i in getOrders())
            {
                if (i.OrderId == orderId)
                {
                    getOrders().Remove(i);
                    getOrders().Add(new Order(orderId, orderDetails));
                    return 0;
                }
            }
            Console.WriteLine("Modify Order Failed, Order doesn't exist.");
            return -1;
        }
        public int deleteOrder(int orderId)
        {
            foreach (Order i in getOrders())
            {
                if (i.OrderId == orderId)
                {
                    getOrders().Remove(i);
                    return 0;
                }
            }
            Console.WriteLine("Delete Order Failed, Order doesn't exist.");
            return -1;
        }
        public int modifyOrder(int orderId, string orderName, string orderUserName, double orderAmount)
        {
            foreach (Order i in getOrders())
            {
                if (i.OrderId == orderId)
                {
                    getOrders().Remove(i);
                    getOrders().Add(new Order(orderId, new OrderDetails(orderName, orderUserName, orderAmount)));
                    return 0;
                }
            }
            Console.WriteLine("Modify Order Failed, Order doesn't exist.");
            return -1;
        }
        public int modifyOrderDetail(OrderDetails oldDetails, OrderDetails newdetails)
        {
            foreach (Order i in getOrders())
            {
                if (i.orderDetails == oldDetails)
                {
                    i.orderDetails = newdetails;
                    return 0;
                }
            }
            Console.WriteLine("Modify Order Failed, OrderDetail doesn't exist.");
            return -1;
        }
        public int modifyOrderDetail(string name, string orderUserName, double orderAmount)
        {
            foreach (Order i in getOrders())
            {
                if (i.orderDetails.OrderName == name)
                {
                    i.orderDetails = new OrderDetails(name, orderUserName, orderAmount);
                    return 0;
                }
            }
            Console.WriteLine("Modify Order Failed, OrderDetail doesn't exist.");
            return -1;
        }
        public int addOrder(Order order)
        {
            List<Order> orders = getOrders();
            foreach (Order tmp in orders)
            {
                if (tmp.Equals(order))
                {
                    Console.WriteLine("Error while adding order: Order exists.");
                    return -2;
                }
            }

            try
            {
                orders.Add(order);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.WriteLine("Error while adding order: Adding failed.");
                return -1;
            }
            Console.WriteLine("Adding Order Success, order id: {0}", order.OrderId);
            return 0;

        }

        public override string ToString()
        {
            string head = "OrderService: Current OrderAmount is: " + getOrders().Count + "\n";
            string content = "";
            foreach (Order i in getOrders())
            {
                content += i.ToString();
                content += "\n";
            }
            return head + content;
        }


    }


    public class OrderDetails
    {
        [XmlElement("OrderName")]
        public string OrderName;
        [XmlElement("UserName")]
        public string OrderUserName;
        [XmlElement("Amount")]
        public double OrderAmount;
        public OrderDetails()
        {

        }
        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   OrderName == details.OrderName &&
                   OrderUserName == details.OrderUserName &&
                   OrderAmount == details.OrderAmount;
        }

        public override string ToString()
        {
            return "OrderDetail: {OrderName: " + OrderName + ";OrderUserName: " + OrderUserName + ";OrderAmount: " + OrderAmount + "}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderName, OrderUserName, OrderAmount);
        }

        public OrderDetails(string orderName, string orderUserName, double orderAmount)
        {
            OrderName = orderName;
            OrderUserName = orderUserName;
            OrderAmount = orderAmount;
        }
    }
    [XmlRoot("OrderRoot",Namespace ="TonyCoderHomework",IsNullable =false)]
    public class Order : IComparable<Order>
    {
        [XmlElement("OrderId")]
        public int OrderId;
        [XmlElement("Details")]
        public OrderDetails orderDetails;
        public Order() { }
        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   OrderId == order.OrderId &&
                   EqualityComparer<OrderDetails>.Default.Equals(orderDetails, order.orderDetails);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, orderDetails);
        }

        public Order(int orderId, OrderDetails orderDetails)
        {
            OrderId = orderId;
            this.orderDetails = orderDetails;
        }

        public override string ToString()
        {
            return "Order: {OrderId: " + OrderId + ";" + orderDetails.ToString() + "}";
        }

        public int CompareTo(Order? order)
        {
            return OrderId - order.OrderId;
        }
    }
}
