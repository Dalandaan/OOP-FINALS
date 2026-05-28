using System;

namespace MiniEcoMarket
{
    // Farmer inherits from User (Inheritance)
    public class Farmer : User
    {
        public string farm { get; set; }

        public Farmer(int num, string user, string pass, string place)
            : base(num, user, pass)
        {
            farm = place;
        }

        // Polymorphism: each user type shows different info
        public override void DisplayInfo()
        {
            Console.WriteLine("--- Farmer Profile ---");
            Console.WriteLine("ID       : " + Id);
            Console.WriteLine("Name     : " + Name);
            Console.WriteLine("Farm     : " + farm);
            Console.WriteLine("Role     : " + GetRole());
        }

        public override string GetRole()
        {
            return "Farmer";
        }

        // Save to file
        public override string ToString()
        {
            return Id + "|" + Name + "|" + Password + "|" + farm + "|Farmer";
        }
    }
}