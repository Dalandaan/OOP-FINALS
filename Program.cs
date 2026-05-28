using System;
using System.Collections.Generic;

namespace MiniEcoMarket
{
    class Program
    {
        // Collections
        static List<Product> prod = new List<Product>();
        static List<Order> ord = new List<Order>();
        static List<Farmer> farm = new List<Farmer>();
        static List<Customer> cust = new List<Customer>();

        static int nextUser = 1;
        static int nextProd = 1;
        static int nextOrd = 1;

        static User user = null;

        // MAIN
        static void Main(string[] args)
        {
            LoadData();

            bool run = true;

            while (run)
            {
                if (user == null)
                    run = MainMenu();
                else if (user is Farmer)
                    FarmerMenu();
                else
                    CustomerMenu();
            }

            SaveData();

            Console.WriteLine("\nThank you for using Mini EcoMarket! Goodbye.");
        }

        // LOAD & SAVE

        static void LoadData()
        {
            FileManager.LoadUsers(farm, cust);

            prod = FileManager.LoadProducts();
            ord = FileManager.LoadOrders();

            foreach (Farmer f in farm)
                if (f.Id > nextUser - 1)
                    nextUser = f.Id + 1;

            foreach (Customer c in cust)
                if (c.Id > nextUser - 1)
                    nextUser = c.Id + 1;

            foreach (Product p in prod)
                if (p.Id > nextProd - 1)
                    nextProd = p.Id + 1;

            foreach (Order o in ord)
                if (o.Id > nextOrd - 1)
                    nextOrd = o.Id + 1;

            // reconnect orders
            foreach (Order o in ord)
            {
                foreach (Customer c in cust)
                {
                    if (c.Name == o.Cust)
                    {
                        c.list.Add(o);
                        break;
                    }
                }
            }
        }

        static void SaveData()
        {
            FileManager.SaveUsers(farm, cust);
            FileManager.SaveProducts(prod);
            FileManager.SaveOrders(ord);
        }

        // MAIN MENU

        static bool MainMenu()
        {
            Console.WriteLine("\n=== MINI ECOMARKET ===");
            Console.WriteLine("[1] Register as Farmer");
            Console.WriteLine("[2] Register as Customer");
            Console.WriteLine("[3] Login");
            Console.WriteLine("[4] Browse Products");
            Console.WriteLine("[0] Exit");

            Console.Write("Choice: ");

            string ch = Console.ReadLine();

            if (ch == "1")
                RegisterFarmer();

            else if (ch == "2")
                RegisterCustomer();

            else if (ch == "3")
                Login();

            else if (ch == "4")
                ShowAllProducts();

            else if (ch == "0")
                return false;

            else
                Console.WriteLine("Invalid choice.");

            return true;
        }

        // REGISTER

        static void RegisterFarmer()
        {
            Console.WriteLine("\n-- Register as Farmer --");

            try
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Password: ");
                string pass = Console.ReadLine();

                Console.Write("Farm Name: ");
                string place = Console.ReadLine();

                Farmer f = new Farmer(nextUser++, name, pass, place);

                farm.Add(f);

                Console.WriteLine("Registered successfully!");

                f.DisplayInfo();
            }
            catch (Exception er)
            {
                Console.WriteLine("Error: " + er.Message);
            }
        }

        static void RegisterCustomer()
        {
            Console.WriteLine("\n-- Register as Customer --");

            try
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Password: ");
                string pass = Console.ReadLine();

                Customer c = new Customer(nextUser++, name, pass);

                cust.Add(c);

                Console.WriteLine("Registered successfully!");

                c.DisplayInfo();
            }
            catch (Exception er)
            {
                Console.WriteLine("Error: " + er.Message);
            }
        }

        // LOGIN

        static void Login()
        {
            Console.WriteLine("\n-- Login --");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            foreach (Farmer f in farm)
            {
                if (f.Name == name && f.Password == pass)
                {
                    user = f;

                    Console.WriteLine("Welcome, Farmer " + f.Name + "!");

                    return;
                }
            }

            foreach (Customer c in cust)
            {
                if (c.Name == name && c.Password == pass)
                {
                    user = c;

                    Console.WriteLine("Welcome, " + c.Name + "!");

                    return;
                }
            }

            Console.WriteLine("Invalid name or password.");
        }

        // FARMER MENU

        static void FarmerMenu()
        {
            Farmer f = (Farmer)user;

            Console.WriteLine("\n=== FARMER MENU === (" + f.Name + ")");
            Console.WriteLine("[1] Add Product");
            Console.WriteLine("[2] View My Products");
            Console.WriteLine("[3] Update Product");
            Console.WriteLine("[4] My Profile");
            Console.WriteLine("[5] Logout");

            Console.Write("Choice: ");

            string ch = Console.ReadLine();

            if (ch == "1")
                AddProduct(f);

            else if (ch == "2")
                ViewFarmerProducts(f);

            else if (ch == "3")
                UpdateProduct(f);

            else if (ch == "4")
                f.DisplayInfo();

            else if (ch == "5")
            {
                SaveData();

                user = null;

                Console.WriteLine("Logged out.");
            }

            else
                Console.WriteLine("Invalid choice.");
        }

        static void AddProduct(Farmer f)
        {
            Console.WriteLine("\n-- Add Product --");

            try
            {
                Console.Write("Product Name: ");
                string item = Console.ReadLine();

                Console.Write("Price: ");
                double cost = double.Parse(Console.ReadLine());

                Console.Write("Stock: ");
                int qty = int.Parse(Console.ReadLine());

                Console.Write("Category: ");
                string type = Console.ReadLine();

                Product p = new Product(nextProd++, item, cost, qty, type, f.Name);

                prod.Add(p);

                Console.WriteLine("Product added!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Price and stock must be numbers.");
            }
            catch (Exception er)
            {
                Console.WriteLine("Error: " + er.Message);
            }
        }

        static void ViewFarmerProducts(Farmer f)
        {
            Console.WriteLine("\n-- My Products --");

            bool found = false;

            foreach (Product p in prod)
            {
                if (p.Farmer == f.Name)
                {
                    p.Display();

                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("You have no products listed.");
        }

        static void UpdateProduct(Farmer f)
        {
            ViewFarmerProducts(f);

            Console.Write("Enter Product ID to update: ");

            try
            {
                int id = int.Parse(Console.ReadLine());

                Product tar = null;

                foreach (Product p in prod)
                {
                    if (p.Id == id && p.Farmer == f.Name)
                    {
                        tar = p;
                        break;
                    }
                }

                if (tar == null)
                    throw new Exception("Product ID " + id + " not found.");

                Console.Write("New Price (Enter to keep " + tar.Price + "): ");

                string pIn = Console.ReadLine();

                if (pIn != "")
                    tar.Price = double.Parse(pIn);

                Console.Write("New Stock (Enter to keep " + tar.Stock + "): ");

                string sIn = Console.ReadLine();

                if (sIn != "")
                    tar.Stock = int.Parse(sIn);

                Console.WriteLine("Product updated!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid numbers.");
            }
            catch (Exception er)
            {
                Console.WriteLine("Error: " + er.Message);
            }
        }

        // CUSTOMER MENU

        static void CustomerMenu()
        {
            Customer c = (Customer)user;

            Console.WriteLine("\n=== CUSTOMER MENU === (" + c.Name + ")");
            Console.WriteLine("[1] Browse All Products");
            Console.WriteLine("[2] Buy a Product");
            Console.WriteLine("[3] View My Orders");
            Console.WriteLine("[4] My Profile");
            Console.WriteLine("[5] Logout");

            Console.Write("Choice: ");

            string ch = Console.ReadLine();

            if (ch == "1")
                ShowAllProducts();

            else if (ch == "2")
                BuyProduct(c);

            else if (ch == "3")
                ViewOrders(c);

            else if (ch == "4")
                c.DisplayInfo();

            else if (ch == "5")
            {
                SaveData();

                user = null;

                Console.WriteLine("Logged out.");
            }

            else
                Console.WriteLine("Invalid choice.");
        }

        static void ShowAllProducts()
        {
            Console.WriteLine("\n-- Available Products --");

            if (prod.Count == 0)
            {
                Console.WriteLine("No products available.");
                return;
            }

            foreach (Product p in prod)
                p.Display();
        }

        static void BuyProduct(Customer c)
        {
            ShowAllProducts();

            Console.Write("\nEnter Product ID to buy: ");

            try
            {
                int id = int.Parse(Console.ReadLine());

                Product tar = null;

                foreach (Product p in prod)
                {
                    if (p.Id == id)
                    {
                        tar = p;
                        break;
                    }
                }

                if (tar == null)
                    throw new Exception("Product ID " + id + " not found.");

                Console.Write("Quantity (available: " + tar.Stock + "): ");

                int qty = int.Parse(Console.ReadLine());

                if (qty <= 0)
                    throw new Exception("Quantity must be at least 1.");

                if (qty > tar.Stock)
                    throw new Exception("Not enough stock. Only " + tar.Stock + " left.");

                double total = qty * tar.Price;

                tar.Stock -= qty;

                Order o = new Order(nextOrd++, c.Name, tar.Name, qty, total);

                ord.Add(o);

                c.list.Add(o);

                Console.WriteLine("Purchase successful!");

                o.Display();
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            catch (Exception er)
            {
                Console.WriteLine("Error: " + er.Message);
            }
        }

        static void ViewOrders(Customer c)
        {
            Console.WriteLine("\n-- My Order History --");

            if (c.list.Count == 0)
            {
                Console.WriteLine("No orders yet.");
                return;
            }

            foreach (Order o in c.list)
                o.Display();
        }
    }
}