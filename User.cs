using System;

namespace MiniEcoMarket
{
    // Abstract base class (Abstraction + Inheritance)
    public abstract class User
    {
        // Private fields (Encapsulation)
        private int _id;
        private string _name;
        private string _password;

        // Public properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Name cannot be empty.");
                _name = value;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Password cannot be empty.");
                _password = value;
            }
        }

        // Constructor
        public User(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }

        // Abstract methods — must be overridden (Polymorphism)
        public abstract void DisplayInfo();
        public abstract string GetRole();
    }
}
