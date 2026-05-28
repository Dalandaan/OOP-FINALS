using System;

namespace MiniEcoMarket
{
    public class Product
    {
        // Private fields (Encapsulation)
        private int _num;
        private string _item;
        private double _cost;
        private int _qty;

        // Public properties
        public int Id
        {
            get { return _num; }
            set { _num = value; }
        }

        public string Name
        {
            get { return _item; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Product name cannot be empty.");

                _item = value;
            }
        }

        public double Price
        {
            get { return _cost; }
            set
            {
                if (value < 0)
                    throw new Exception("Price cannot be negative.");

                _cost = value;
            }
        }

        public int Stock
        {
            get { return _qty; }
            set
            {
                if (value < 0)
                    throw new Exception("Stock cannot be negative.");

                _qty = value;
            }
        }

        public string Type { get; set; }
        public string Farmer { get; set; }

        public Product(int num, string item, double cost, int qty, string type, string farmer)
        {
            Id = num;
            Name = item;
            Price = cost;
            Stock = qty;
            Type = type;
            Farmer = farmer;
        }

        public void Display()
        {
            Console.WriteLine("[" + Id + "] " + Name + " - P" + Price + " | Stock: " + Stock + " | " + Type + " | By: " + Farmer);
        }

        // Save to file
        public override string ToString()
        {
            return Id + "|" + Name + "|" + Price + "|" + Stock + "|" + Type + "|" + Farmer;
        }

        // Load from file
        public static Product FromString(string line)
        {
            string[] arr = line.Split('|');

            if (arr.Length != 6)
                throw new Exception("Bad product data: " + line);

            return new Product(
                int.Parse(arr[0]),
                arr[1],
                double.Parse(arr[2]),
                int.Parse(arr[3]),
                arr[4],
                arr[5]
            );
        }
    }
}