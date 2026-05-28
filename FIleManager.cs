using System;
using System.Collections.Generic;
using System.IO;

namespace MiniEcoMarket
{
    // Handles saving and loading using StreamReader and StreamWriter
    public static class FileManager
    {
        static string prodFile = "products.txt";
        static string ordFile  = "orders.txt";
        static string usrFile  = "users.txt";

        // PRODUCTS

        public static void SaveProducts(List<Product> data)
        {
            StreamWriter sw = new StreamWriter(prodFile);

            foreach (Product p in data)
                sw.WriteLine(p.ToString());

            sw.Close();
        }

        public static List<Product> LoadProducts()
        {
            List<Product> data = new List<Product>();

            if (!File.Exists(prodFile))
                return data;

            StreamReader sr = new StreamReader(prodFile);

            string txt;

            while ((txt = sr.ReadLine()) != null)
            {
                try
                {
                    data.Add(Product.FromString(txt));
                }
                catch (Exception er)
                {
                    // Exception handling: skip corrupted lines
                    Console.WriteLine("Skipped bad product line: " + er.Message);
                }
            }

            sr.Close();
            return data;
        }

        // ORDERS

        public static void SaveOrders(List<Order> data)
        {
            StreamWriter sw = new StreamWriter(ordFile);

            foreach (Order o in data)
                sw.WriteLine(o.ToString());

            sw.Close();
        }

        public static List<Order> LoadOrders()
        {
            List<Order> data = new List<Order>();

            if (!File.Exists(ordFile))
                return data;

            StreamReader sr = new StreamReader(ordFile);

            string txt;

            while ((txt = sr.ReadLine()) != null)
            {
                try
                {
                    data.Add(Order.FromString(txt));
                }
                catch (Exception er)
                {
                    Console.WriteLine("Skipped bad order line: " + er.Message);
                }
            }

            sr.Close();
            return data;
        }

        // USERS

        public static void SaveUsers(List<Farmer> farm, List<Customer> cust)
        {
            StreamWriter sw = new StreamWriter(usrFile);

            foreach (Farmer f in farm)
                sw.WriteLine(f.ToString());

            foreach (Customer c in cust)
                sw.WriteLine(c.ToString());

            sw.Close();
        }

        public static void LoadUsers(List<Farmer> farm, List<Customer> cust)
        {
            if (!File.Exists(usrFile))
                return;

            StreamReader sr = new StreamReader(usrFile);

            string txt;

            while ((txt = sr.ReadLine()) != null)
            {
                try
                {
                    string[] arr = txt.Split('|');

                    if (arr[arr.Length - 1] == "Farmer")
                        farm.Add(new Farmer(int.Parse(arr[0]), arr[1], arr[2], arr[3]));
                    else
                        cust.Add(new Customer(int.Parse(arr[0]), arr[1], arr[2]));
                }
                catch (Exception er)
                {
                    Console.WriteLine("Skipped bad user line: " + er.Message);
                }
            }

            sr.Close();
        }
    }
}