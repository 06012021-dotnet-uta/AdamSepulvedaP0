using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P0DbContext;

namespace Project0
{
    class UserOrder : UserInterface
    {
        //Dictionary<int, StoreLocation> stores = new Dictionary<int, StoreLocation>();
        //Dictionary<int, Product> cart = new Dictionary<int, Product>();
        private Cart cart = new Cart();
        private Customer user = new Customer();
        private StoreLocation storePicked;
        private Product productPicked;
        private int storesCount;
        private int productsCount;
        private int cartCount = 0;

        P0DatabaseContext context;



        public UserOrder()
        {
            //Dictionary<int, StoreLocation> stores = new Dictionary<int, StoreLocation>();
            //StoreLocation storePicked = new StoreLocation();
            //Cart cart = new Cart();
        }
        public UserOrder(P0DatabaseContext context)
        {
            this.context = context;
        }
        public UserOrder(Customer user)
        {
            this.user = user;
        }
        // Main Order Menu
        public void OrderMessage()
        {
            Console.WriteLine("Let's Start your Order!");
            Console.WriteLine("\t1) Order from Stores\n\t2) Order History\n\t3) Quit Program");
        }
        // Print List of all stores from database and counts each store
        public void PrintStoreList()
        {
            Console.WriteLine(" - - - - - - - - Store Menu - - - - - - - - ");
            Console.WriteLine("Choose where to shop from the many Kwik-e-mart locations");
            using (var context = new P0DatabaseContext())
            {
                //var stores = context.StoreLocations.Select(x=>x.)
                storesCount = 0;
                foreach(StoreLocation c in StoreLocations)
                {
                    storesCount++;
                    Console.WriteLine($"\t{c.StoreId}) {c.StoreStreet}, {c.StoreCity}, {c.StoreState} {c.StoreZip}");
                    
                    //stores.Add(storesCount,c);
                }
            }
        }
        // Set storePicked as the chosen store from InputChoice()
        public StoreLocation ChooseStore()
        {
            using (var context = new P0DatabaseContext())
            {
                int choice = InputChoice(storesCount);
                storePicked = context.StoreLocations.Where(x => x.StoreId == choice).FirstOrDefault();
                
                //storePicked = stores[choice];
                //Console.WriteLine(storePicked.StoreStreet);
                return storePicked;
            }
        }
        // Print list of all products within storePicked
        public void PrintProductList()
        {
            //Console.WriteLine(" ...ChooseProductsMethod init");
            Console.WriteLine(" - - - - - - - - Shopping Menu - - - - - - - -");
            Console.WriteLine("Choose what products you would like to purchase");
            using (var context = new P0DatabaseContext())
            {
                var productList = context.Products.ToList();
                //var quantity = context.StoreInventories.Where(x => x.StoreId == storePicked.StoreId);
                
                productsCount = 0;
                foreach (Product c in Products)
                {
                    productsCount++;
                    String.Format("0:0.00", c.Price);
                    var quantity = context.StoreInventories.Where(x => x.ProductId == c.ProductId).Where(x=>x.StoreId==storePicked.StoreId).Single();
                    Console.WriteLine($"\t{c.ProductId}) {c.ProductName.PadRight(20,' ')} | ${String.Format("{0:0.00}", c.Price).PadRight(6,' ')} | {quantity.Quantity.ToString().PadLeft(3,' ')} in Stock");
                    //products.Add(productsCount, c);
                }
                Console.WriteLine($"\n\t{productsCount + 1}) Purchase Items From Cart");
                Console.WriteLine($"\t{productsCount + 2}) Exit Shopping Menu");
            }
        }
        public int ChooseProduct()
        {
            using (var context = new P0DatabaseContext())
            {
                int choice = InputChoice(productsCount+2);
                if(choice <= productsCount)
                    productPicked = context.Products.Where(x => x.ProductId == choice).FirstOrDefault();

                return choice;
            }
                
        }

        public void ChooseProducts()
        {
            using (var context = new P0DatabaseContext())
            {
                // Print Product List and Cart list
                Console.Clear();
                PrintProductList();
                cart.PrintCart();
                // Input Choice
                int choice = InputChoice(productsCount+2);
                // Purchase Cart Option
                if(choice == productsCount+1)
                {
                    cart.UpdateStoreInventory(storePicked.StoreId);
                    int orderID = UpdateOrder();
                    cart.UpdateOrderInventory(orderID);
                    Console.WriteLine("Thank you for shopping");
                }
                // Leave Menu Option
                else if(choice == productsCount+2)
                {
                    Console.WriteLine("Goodbye");
                }
                // Add to Cart
                else
                {
                    // Query Chosen Product
                    productPicked = context.Products.Where(x => x.ProductId == choice).Single();
                    // Query Quantity of Picked Product in Picked Store
                    var storeStock = context.StoreInventories.Where(x => x.StoreId == storePicked.StoreId).Where(x => x.ProductId == productPicked.ProductId).Single();
                    int stock = storeStock.Quantity;
                    // Add to Cart Dictionary
                    cart.AddtoCart(productPicked, stock);
                    // Recall Product Menu
                    ChooseProducts();
                }
            }
        }
        public void PrintOrderList()
        {
            Console.WriteLine(" - - - - - - - - Order List - - - - - - - - ");
            using (var context = new P0DatabaseContext())
            {
                //var stores = context.StoreLocations.Select(x=>x.)
                
                foreach (CustomerOrderQuantity c in CustomerOrderQuantities)
                {
                    CustomerOrder order = context.CustomerOrders.Where(x => x.OrderId == c.OrderId).Single();
                    Product product = context.Products.Where(x => x.ProductId == c.ProductId).Single();
                    Customer customer = context.Customers.Where(x => x.CustomerId == order.CustomerId).Single();
                    //CustomerOrderQuantity orderQuantity = context.CustomerOrderQuantities.Where(x => x.OrderId == c.OrderId).Single();


                    string name = customer.FirstName + ' ' + customer.LastName;
                    //StoreLocation store = context.StoreLocations.Where(x => x.StoreId == c.)
                    Console.WriteLine($"\t ID: {c.OrderId} | Name: {name.PadRight(20,' ')} | Username: {customer.Username.PadRight(10,' ')} | {product.ProductName.PadRight(20,' ')} | {c.OrderQuantity.ToString().PadLeft(5,' ')} in Stock | {order.OrderTime}");
                    

                    //stores.Add(storesCount,c);
                }
                Console.WriteLine("\t-Press Enter to Continue-");
                Console.ReadLine();
            }
        }
        // Add Order Record to Database
        public int UpdateOrder()
        {
            using (var context = new P0DatabaseContext())
            {
                CustomerOrder order = new CustomerOrder();
                Console.WriteLine($"CustomerID: {user.CustomerId}");
                order.CustomerId = user.CustomerId;
                context.CustomerOrders.Add(order);
                context.SaveChanges();
                return order.OrderId;
            }
        }
        // Initialize Order Methods
        public void InitOrder(Customer user)

        {
            this.user = user;
            OrderMessage();
            int choice = InputChoice(3);
            switch(choice)
            {
                // Order Products
                case 1:
                    PrintStoreList();
                    ChooseStore();
                    PrintProductList();
                    ChooseProducts();
                    InitOrder(user);
                    break;
                case 2:
                    PrintOrderList();
                    InitOrder(user);
                    break;
                default:
                    break;

            }



        }
    }// end of class
}// end of project
