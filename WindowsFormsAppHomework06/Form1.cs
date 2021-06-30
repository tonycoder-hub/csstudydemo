using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsAppHomework06
{
    public partial class Form1 : Form
    {
        public static OrderService service = new OrderService();
        public Form1()
        {
            
            List<Order> orders = service.getOrders();

            OrderDetails details1 = new OrderDetails("detail1", "user1", 123.22);
            OrderDetails details2 = new OrderDetails("detail2", "user2", 2323.22);
            OrderDetails details3 = new OrderDetails("detail3", "user3", 3423.22);
            OrderDetails details4 = new OrderDetails("detail4", "user4", 4523.22);

            Order order1 = new Order(1, details1);
            Order order2 = new Order(2, details2);
            Order order3 = new Order(3, details3);
            Order order4 = new Order(4, details4);


            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);
            orders.Add(order4);

            System.Diagnostics.Debug.WriteLine(service.ToString());
            InitializeComponent();
        }
        public void Reset()
        {
            this.bindingSource1.ResetBindings(false);
        }
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Reset();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

            foreach (Order i in orders)
            {
                string filename = "Order_" + i.OrderId + ".xml";
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
        [XmlElement("OrderId")]
        public int OrderId { get; set; }
        [XmlElement("OrderName")]
        public string OrderName { get; set; }
        [XmlElement("UserName")]
        public string OrderUserName { get; set; }
        [XmlElement("Amount")]
        public double OrderAmount { get; set; }
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "OrderDetail: {OrderName: " + OrderName + ";OrderUserName: " + OrderUserName + ";OrderAmount: " + OrderAmount + "}";
        }

     

        public OrderDetails(string orderName, string orderUserName, double orderAmount)
        {
            OrderName = orderName;
            OrderUserName = orderUserName;
            OrderAmount = orderAmount;
        }
    }
    [XmlRoot("OrderRoot", Namespace = "TonyCoderHomework", IsNullable = false)]
    public class Order : IComparable<Order>
    {
        [XmlElement("OrderId")]
        public int OrderId { get; set; }
        [XmlElement("Details")]
        public OrderDetails orderDetails { get; set; }
        public Order() { }
        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   OrderId == order.OrderId &&
                   EqualityComparer<OrderDetails>.Default.Equals(orderDetails, order.orderDetails);
        }

        

        public Order(int orderId, OrderDetails orderDetails)
        {
            OrderId = orderId;
            this.orderDetails = orderDetails;
            orderDetails.OrderId = orderId;
        }

        public override string ToString()
        {
            return "Order: {OrderId: " + OrderId + ";" + orderDetails.ToString() + "}";
        }

        public int CompareTo(Order order)
        {
            return OrderId - order.OrderId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
