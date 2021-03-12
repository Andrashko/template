using System;
using System.Collections.Generic;

namespace GRASP
{

    class ClientController{
        private Client client;
        private IBill bill;
        public ClientController(Client client, IBill bill){
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

        public void addCreditCardDialog(){
            Console.Write("Enter card number:");
            string number = Console.ReadLine();
            Console.Write("Enter balance:");
            double balance =  double.Parse(Console.ReadLine());
            Console.WriteLine("Select vendor: \n 1 Visa \n 2 Mastercard");
            string [] vendorsList = new string[2] {"VISA", "MASTERCARD"};
            int vendorIndex = int.Parse(Console.ReadLine());
            string vendor = vendorsList[vendorIndex-1];
            client.addCreditCard(new Card(number, balance, vendor));
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

        public override string ToString(){
            return $"{this.goods.name} - {this.amount}";
        }
    }


    interface IBill{
        void Pay(IPaymentSource paymentSource);
        void addItem(Goods goods, int amount);
    }

    interface IPaymentSource{
        bool checkEnaughtBalance(double sum);
    }
    class Bill: IBill
    {
        private List<Item> items = new List<Item>();
        public Client client;

        public List<iPaymentVendor> vendors ;

        public double calculateTotalCost(){
            double totalCost = 0;
            foreach (var item in this.items)
            {
                totalCost += item.getTotalPrice();
            }
            return totalCost;
        }
        public void Pay(IPaymentSource paymentSource)
        {
            
            //рахує загальну вартість
          
            double totalCost = this.calculateTotalCost();
            
            var vendor = this.vendors.Find(v => v.Check(paymentSource));
            if (vendor != null && vendor.MakePayment(totalCost, paymentSource)){
                Console.WriteLine("Succsess!");
            }
            else
                Console.WriteLine("Failure");
        }

        public void printGoogsList(){
             foreach (var item in this.items){
                 Console.WriteLine(item.ToString());
             }
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

        public void addCreditCard (Card card){
            this.creditCards.Add(card);
        }
    }

    class Card: IPaymentSource
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

    interface iPaymentVendor{
        public bool MakePayment(double sum, IPaymentSource paymentSource);
        public string getName();

        public bool Check(IPaymentSource paymentSource){
            return ((paymentSource as Card).vendor == this.getName());
        }
    }
    class Visa: iPaymentVendor
    {
        public string name = "VISA";
        public bool MakePayment(double sum, IPaymentSource paymentSource)
        {
            Console.WriteLine($"Processing payment by Visa for {sum}");
            return paymentSource.checkEnaughtBalance(sum);
        }

        public string getName(){
            return this.name;
        }
    }

    class Mastercard: iPaymentVendor
    {
        public string name = "MASTERCARD";
        public bool MakePayment(double sum, IPaymentSource paymentSource)
        {
            Console.WriteLine($"Processing payment by Mastrcard for {sum}");
            return paymentSource.checkEnaughtBalance(sum);
        }

         public string getName(){
            return this.name;
        }

    }
}
