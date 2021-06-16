using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P0DbContext;

namespace Project0
{
    public class Cart
    {
        private Dictionary<int, int> inven = new Dictionary<int, int>();
        //P0DatabaseContext context = new P0DatabaseContext();
        public List<Product> Inventory
        {
            get;
            set;
        }

        public void PrintCart()
        {
            using (var context = new P0DatabaseContext())
            {
                Console.WriteLine(" - - - - - - - - Shopping Cart - - - - - - - - ");
                if(inven.Count!=0)
                {

                    decimal sum = 0;
                    foreach(KeyValuePair<int,int> kvp in inven)
                    { 
                        Product product = context.Products.Where(x => x.ProductId == kvp.Key).FirstOrDefault();

                        sum += product.Price * kvp.Value;
                        Console.WriteLine($"\t{kvp.Key}) | {product.ProductName} {String.Format("{0:0.00}", product.Price).PadRight(6, ' ')} | {kvp.Value.ToString().PadLeft(3)} in Stock");
                    }
                    Console.WriteLine($"\tTotal Sum: {String.Format("{0:0.00}", sum)}");
                }
            }
        }
        public void AddtoCart(Product product, int stock)
        {
            int value = 0;
            if(stock > 0)
            {
                if (inven.ContainsKey(product.ProductId))
                {
                    inven.TryGetValue(product.ProductId, out value);
                    if(value >= stock)
                    {
                        Console.WriteLine($"\tError: We cannot add any more of {product.ProductName}");
                        Console.WriteLine("\t-Press any Key to Continue-");
                        Console.ReadLine();
                    }
                    if(value < stock)
                    {
                        Console.WriteLine($"stock = {stock}");
                        inven[product.ProductId] = value + 1;
                    }
                }
                else
                {
                    inven.Add(product.ProductId, 1);
                }
            }
            
        }
        // Decrement Quantities of the StoreInventory Database
        public void UpdateStoreInventory(int storeID)
        {
            using (var context = new P0DatabaseContext())
            {
                
                // Iterate Cart Dictionary 
                foreach (KeyValuePair<int, int> kvp in inven)
                {
                    // Query stock
                    Product product = context.Products.Where(x => x.ProductId == kvp.Key).Single();
                    var storeInventory = context.StoreInventories.Where(x => x.StoreId == storeID).Where(x => x.ProductId == product.ProductId).Single();
                    int stock = storeInventory.Quantity;
                    // Update Quantity Value in StoreInventory
                    storeInventory.Quantity = stock - kvp.Value;
                }
                //Save Changes
                context.SaveChanges();
            }
        }
        // Update Orders
        public void UpdateOrderInventory(int orderID)
        {
            using (var context = new P0DatabaseContext())
            {
                // Iterate Cart Dictionary 
                foreach (KeyValuePair<int, int> kvp in inven)
                {
                    // Create OrderItem
                    CustomerOrderQuantity orderItem = new CustomerOrderQuantity();
                    int productID = context.Products.Where(x => x.ProductId == kvp.Key).Single().ProductId;
                    orderItem.OrderId = orderID;
                    orderItem.ProductId = productID;
                    orderItem.OrderQuantity = kvp.Value;

                    context.CustomerOrderQuantities.Add(orderItem);
                }
                //Save Changes
                context.SaveChanges();
            }
        }
    }// end of class
}// end of project
