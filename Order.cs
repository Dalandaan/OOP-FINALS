using System;

namespace MiniEcoMarket
{
    public class Order
    {
        public int Id { get; set; }
        public string Cust { get; set; }
        public string Prod { get; set; }
        public int Qty { get; set; }
        public double Total { get; set; }
        public string Day { get; set; }

        public Order(int id, string cust, string prod, int qty, double total)
        {
            Id = id;
            Cust = cust;
            Prod = prod;
            Qty = qty;
            Total = total;

            Day = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public void Display()
        {
            Console.WriteLine("Order #" + Id + " | " + Prod + " x" + Qty + " | Total: P" + Total + " | Date: " + Day);
        }

        // Save to file
        public override string ToString()
        {
            return Id + "|" + Cust + "|" + Prod + "|" + Qty + "|" + Total + "|" + Day;
        }

        // Load from file
        public static Order FromString(string line)
        {
            string[] arr = line.Split('|');

            if (arr.Length != 6)
                throw new Exception("Bad order data: " + line);

            Order o = new Order(
                int.Parse(arr[0]),
                arr[1],
                arr[2],
                int.Parse(arr[3]),
                double.Parse(arr[4])
            );

            o.Day = arr[5];

            return o;
        }
    }
}