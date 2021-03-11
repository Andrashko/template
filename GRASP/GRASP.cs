using System;
using System.Collections.Generic;

namespace GRASP
{

    class ClientController{
        private Client client;
        private Bill bill;
        public ClientController(Client client, Bill bill){
            this.client = client;
            this.bill = bill;
        }
        public Card selectCardToPay(){
            //вибрати карту
            Console.WriteLine("Choose number of your credit card");
            int index = int.Parse(Console.ReadLine());
            return this.client.selectCard(index);
        }

        public void makePayment(){
            this.bill.Pay(this.selectCardToPay());
        }

        public void startNewBill(){
            this.bill = new Bill();
        }
    }
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
        
        //information expert
        public double getTotalPrice(){
            return this.goods.price * this.amount;
        }

        public bool isEquealGoods (Goods goods){
            return this.goods.name == goods.name;
        }

        public void incAmount(int value){
            this.amount += value;
        }
    }

    class Bill
    {
        private List<Item> items = new List<Item>();
        public Client client;

        public void Pay(Card card)
        {
            //рахує загальну вартість
            double totalCost = 0;
            foreach (var item in this.items)
            {
                totalCost += item.getTotalPrice();
            }

            if (card.vendor == "VISA")
                if (Visa.MakePayment(totalCost, card))
                    Console.WriteLine("Succsess!");
                else
                    Console.WriteLine("Failure");
            else if (card.vendor == "MASTERCARD")
                if (Mastercard.MakePayment(totalCost, card))
                    Console.WriteLine("Succsess!");
                else
                    Console.WriteLine("Failure");
        }

        public void addItem(Goods goods, int amount){
            int index = this.items.FindIndex(item => item.isEquealGoods(goods));
            if (index == -1)
                this.items.Add(new Item(goods, amount));
            else
                this.items[index].incAmount(amount);
        }
    }

    class Client
    {
        public List<Card> creditCards = new List<Card>();

        //information expert
        public Card selectCard(int index){
            if (index < this.creditCards.Count && index >= 0)
                return this.creditCards[index];
            else
                throw new Exception("Out of range");
        }
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

        //information expert    
        public bool checkEnaughtBalance(double sum){
            return this.balance >= sum;
        }
    }

    class Visa
    {
        static public bool MakePayment(double sum, Card card)
        {
            Console.WriteLine($"Processing payment by Visa for {sum}");
            return card.checkEnaughtBalance(sum);
        }
    }

    class Mastercard
    {
        static public bool MakePayment(double sum, Card card)
        {
            Console.WriteLine($"Processing payment by Mastrcard for {sum}");
            return card.checkEnaughtBalance(sum);
        }

    }
}
