using System;
using System.Timers;

namespace ConsoleCoreEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            // event source
            Timer timer = new Timer();
            timer.Interval = 1000;
            // event subscriber
            Boy boy = new Boy();
            Girl girl = new Girl();
            // subscription(event->handler)
            timer.Elapsed += boy.Action;
            timer.Elapsed += girl.Action;
            //timer.Start();
            //Console.ReadLine();

            Customer customer = new Customer();
            Waiter waiter = new Waiter();

            customer.Order += waiter.Action;

            customer.Action();

            customer.PayTheBill();
        }
    }

    class Boy
    {
        // event handler
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Jump");
        }
    }

    class Girl
    {
        // event handler
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Sing");
        }
    }

    //public delegate void OrderEventHandler(Object obj, EventArgs e);

    public class OrderEventArgs: EventArgs
    {
        public string DishName { get; set; }
        public string Size { get; set; }
    }

    public class Customer
    {
        //private OrderEventHandler orderEventHandler;
        //public event OrderEventHandler Order
        //{
        //    add
        //    {
        //        this.orderEventHandler += value;
        //    }
        //    remove
        //    {
        //        this.orderEventHandler -= value;
        //    }
        //}
        public event EventHandler Order;
        public double Bill { get; set; }
        public void ComeInAndThink()
        {
            Console.WriteLine("Customer Come In");
            Console.WriteLine("Customer Sit Down");
            Console.WriteLine("Customer Think");
            Console.WriteLine("Customer Order");
        }
        public void OnOrder(string dishName, string dishSize)
        {
            OrderEventArgs e = new OrderEventArgs();
            e.DishName = dishName;
            e.Size = dishSize;
            if(this.Order != null)
                this.Order.Invoke(this, e);
        }

        public void Action()
        {
            this.OnOrder("Dumplings", "Large");
        }

        public void PayTheBill()
        {
            Console.WriteLine($"I will pay ${Bill}");
        }
    }

    public class Waiter
    {
        public void Action(object sender, EventArgs e)
        {
            Customer customer = sender as Customer;
            OrderEventArgs oe = e as OrderEventArgs;
            Console.WriteLine($"We will serve you {oe.Size} {oe.DishName}");
            customer.Bill += 10;
        }
    }
}
