using System;
using System.Collections.Generic;

namespace MiniEcoMarket
{
    // Customer inherits from User (Inheritance)
    public class Customer : User
    {
        // List<Order> collection stores purchase history
        public List<Order> list { get; set; }

        public Customer(int num, string user, string pass)
            : base(num, user, pass)
        {
            list = new List<Order>();
        }

        // Polymorphism: different DisplayInfo from Farmer
        public override void DisplayInfo()
        {
            Console.WriteLine("--- Customer Profile ---");
            Console.WriteLine("ID       : " + Id);
            Console.WriteLine("Name     : " + Name);
            Console.WriteLine("Role     : " + GetRole());
            Console.WriteLine("Orders   : " + list.Count);
        }

        public override string GetRole()
        {
            return "Customer";
        }

        public override string ToString()
        {
            return Id + "|" + Name + "|" + Password + "|Customer";
        }
    }
}