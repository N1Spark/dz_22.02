using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz_22._02
{
    class Program
    {
        public interface ICheck
        {
            void Print(Customer customer, Type type);
        }

        public interface ICustom
        {
            void Init();
            void Add(double amount);
            void TypeMoney();
            void DrawType();
        }

        public interface Type
        {
            double Convert_Money(double money);
            string ShowType();
        }
        class EUR : Type
        {
            public double Convert_Money(double money)
            {
                return money * 39.15;
            }
            public string ShowType() { return "EUR"; }
        }
        class USD : Type
        {
            public double Convert_Money(double money)
            {
                return money * 36.74;
            }
            public string ShowType() { return "USD"; }
        }
        class GRN : Type
        {
            public double Convert_Money(double money)
            {
                return money;
            }
            public string ShowType() { return "GRN"; }
        }

        public class Customer : ICustom
        {
            public string Name { get; private set; }
            public string Card { get; private set; }
            public double Money { get; private set; }
            public Type type { get; private set; }
            public double amount { get; private set; }
            public Customer() { }
            public Customer(string name, string card, int money)
            {
                Name = name;
                Card = card;
                Money = money;
            }
            public void Init()
            {
                Console.Write($"Customer: ");
                Name = Console.ReadLine();
                Console.Write($"Card: ");
                Card = Console.ReadLine();
                Console.Write($"Money: ");
                Money = Convert.ToDouble(Console.ReadLine());
            }
            public void Add(double amount) => Money += amount;
            public void Draw(double amount)
            {
                if (amount > Money)
                {
                    Console.WriteLine("Not enough in card");
                    return;
                }
                Money -= amount;
            }
            public void DrawType()
            {
                if (type == null)
                    type = new GRN();
                Console.Write("Count: ");
                double amount = Convert.ToDouble(Console.ReadLine());
                this.Draw(type.Convert_Money(amount));
            }
            public void TypeMoney()
            {
                Console.Write("Type (1. EUR\t2. USD\t3. GRN): ");
                int buf = Convert.ToInt32(Console.ReadLine());
                switch (buf)
                {
                    case 1:
                        type = new EUR(); break;
                    case 2:
                        type = new USD(); break;
                    case 3:
                        type = new GRN(); break;
                    default:
                        Console.WriteLine("Wrong answer");
                        return;
                }
            }
            public override string ToString()
            {
                return $"Name: {Name}\nCard: {Card}";
            }
        }

        class Bank
        {
            public void Init(ICustom customer)
            {
                customer.Init();
            }
            public void Add(ICustom customer, double amount) => customer.Add(amount);

            public void Check(ICheck check, Customer customer, Type type)
            {
                check.Print(customer, type);
            }
        }
        class Check_Print : ICheck
        {
            public void Print(Customer customer, Type type)
            {
                Console.WriteLine(customer.ToString() + $"Count draw: {customer.amount:f2}\nType: " + type.ShowType());
            }
        }
        class Check_Mail : ICheck
        {
            public void Print(Customer customer, Type type)
            {
                Console.WriteLine("Email: ");
                string buf = Console.ReadLine();
                Console.WriteLine(customer.ToString() + $"Count draw: {customer.amount:f2}\nType: " + type.ShowType() +
                    "\nСheck sent to mail: " + buf);
            }
        }
        class Check_SMS : ICheck
        {
            public void Print(Customer customer, Type type)
            {
                Console.WriteLine("Num: ");
                string buf = Console.ReadLine();
                Console.WriteLine(customer.ToString() + $"Count draw: {customer.amount:f2}\nType: " + type.ShowType() +
                    "\nСheck sent to Num: " + buf);
            }
        }

        static void Main(string[] args)
        {
        }
    }
}
