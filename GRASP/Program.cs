using System;
using System.Collections.Generic;

namespace GRASP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Goods> catalog = new List<Goods>(){new Goods("Bread", 14.50), new Goods("Cola", 37.99), new Goods("Chips", 10)};
            Client me = new Client();
            me.creditCards = new List<Card> () { new Card("123", 50000, "VISA"), new Card("345", 100, "MASTERCARD")};
            Bill bill = new Bill ();
            bill.client = me;
            bill.addItem(catalog[0], 2);
            bill.addItem(catalog[2], 3);
            bill.addItem(catalog[1], 1);
            bill.addItem(catalog[2], 5);
            
            ClientController controller = new ClientController(me, bill);
            controller.makePayment();
            Console.ReadLine();
        }
    }
}
