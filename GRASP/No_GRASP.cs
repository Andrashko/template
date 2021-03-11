using System;
using System.Collections.Generic;

namespace No_GRASP
{
    class Goods
    {
        public string name { get; set; }
        public double price { get; set; }


        public Goods(string name, double price)
        {
            this.name = name;
            this.price = price;
        }
    }

    class Item
    {
        public Goods goods { get; set; }
        public int amount { get; set; }

        public Item(Goods goods, int amount = 1)
        {
            this.goods = goods;
            this.amount = amount;
        }
    }

    class Bill
    {
        public List<Item> items = new List<Item>();
        public Client client;

        public void Pay()
        {
            double totalCost = 0;
            foreach (var item in this.items)
            {
                totalCost += item.goods.price * item.amount;
            }

            Console.WriteLine("Choose number of your credit card");
            int index = int.Parse(Console.ReadLine());
            Card card = this.client.creditCards[index];
            if (card.vendor == "VISA")
                if (card.balance >= totalCost && Visa.MakePayment(totalCost, card.number))
                    Console.WriteLine("Succsess!");
                else
                    Console.WriteLine("Failure");
            else if (card.vendor == "MASTERCARD")
                if (card.balance >= totalCost && Mastercard.MakePayment(totalCost, card.number))
                    Console.WriteLine("Succsess!");
                else
                    Console.WriteLine("Failure");
        }
    }

    class Client
    {
        public List<Card> creditCards = new List<Card>();

    }

    class Card
    {
        public string number { get; set; }
        public double balance { get; set; }
        public string vendor { get; set; }

        public Card(string number, double balance, string vendor)
        {
            this.number = number;
            this.balance = balance;
            this.vendor = vendor;
        }
    }

    class Visa
    {
        static public bool MakePayment(double sum, string cardnumber)
        {
            Console.WriteLine($"Processing payment by visa for {sum}$");
            return true;
        }
    }

    class Mastercard
    {
        static public bool MakePayment(double sum, string cardnumber)
        {
            Console.WriteLine($"Processing payment by Mastrcard for {sum}$");
            return true;
        }

    }
}