using System;
using System.Collections.Generic;

namespace GRASP
{
    class Program
    {
        static void Main(string[] args)
        {
            Client me = new Client();
            me.creditCards = new List<Card> () { new Card("123", 50000, "VISA"), new Card("345", 100, "MASTERCARD")};
            Bill bill = new Bill ();
            bill.client = me;
            bill.items = new List<Item>(){new Item(new Goods("Bread", 14.50), 2), 
                                        new Item(new Goods("Cola", 37.99)), 
                                        new Item(new Goods("Chips", 10), 5)};
            bill.Pay();
            Console.ReadLine();
        }
    }
}
