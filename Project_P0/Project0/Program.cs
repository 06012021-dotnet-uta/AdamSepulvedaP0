using System;
using System.Linq;
using P0DbContext;

namespace Project0
{

	
	public class Program
	{
		// create a project for DbContext
		// download these packages => EFCore.SqlServer, .design, .tools
		// Scaffold-DbContext -Provider Microsoft.EntityFrameworkCore.SqlServer -Connection "Server=localhost\SQLEXPRESS;Database=P0Database;Trusted_Connection=True;"
		// demo the context
		// create matching classes as needed


		

		//Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;


		static void Main(string[] args)
		{
			P0DatabaseContext context = new P0DatabaseContext();
			UserRegistration userRegistration = new UserRegistration();
			UserOrder userOrder = new UserOrder(context);
			Customer user = userRegistration.InitRegistration(context);
			Console.WriteLine($"CustomerID: {user.CustomerId}");
			userOrder.InitOrder(user);



			/*Coding challenge
            1. implement a loop to play again if the player chooses to.
            2. get the players name to print out the winners names
            3. implement the code to play 3 rounds.
            */

		}//end of main
	}//end of class
}//end of namespace

// 1. Create an account and login
// 2. Choose store from list of locations
// 3. View all available products of that store
// 4. Fill a cart with a user-chosen product
// 5. Choose the number of products
// 6. Check out

// The user will be able to view details of only their own past orders and will be able to view a history of orders of any one specific location.
// You do not need to include an admin level access
// Assume any user can do/view these things.
// Upon log-out, the program will not quit but allow another user to log in
// On the log-in page there will be the option to quit the program.

/*
 ---- FUNCTIONALITY ----
place orders to store locations for customers
add a new customer
search customers by name
display details of an order
display all order history of a store location
display all order history of a customer
input validation
exception handling
persistent data (to a DB); no hardcoding of data.(prices, customers, order history, etc.)
(+5 pts): order history can be sorted by earliest, latest, cheapest, most expensive)
(+5 pts): get a suggested order for a customer based on his order history)
(+5 pts): display some statistics based on order history)
*/
/*
  ---- DESIGN ----
use EF Core with database-first approach
use a DB in third normal form
use the minimum accessibility for fields, properties, and classes within your application.
Exception Handling. prevent out of range values in a logical way.
define and use an interface for all helper classes.
(+5 pts): use an eventHandler with at least 3 methods.
*/
/*
 ---- CORE / DOMAIN / BUSINESS LOGIC ----
have one project that contains all business logic
have one library project that contains domain classes (customer, order, store, product, etc.)
have a separate library project for Db Access (mentioned below)
documentation with <summary> XML comments on all public types and members (including: <params> and <return>)
*/