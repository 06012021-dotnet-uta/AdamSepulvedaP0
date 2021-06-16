using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P0DbContext;

namespace Project0
{
    class UserRegistration : UserInterface
    {
        private Customer user = new Customer();
        private string fName;
        private string lName;
        private string username;
        private string password;

        public Customer User
        {
            get; 
            set;
        }
        public void WelcomeMessage()
        {
            System.Console.WriteLine("\nWelcome to Kwik-E-Mart Shopping in CONSOLE!\n\nAre you a new customer? Sign up if you are!");
        }
        public void LoginMessage()
        {
            System.Console.WriteLine("\t1) Sign in\n\t2) Log in\n\t3) Quit Program");
        }
        public void InputAccount() 
        {
            
            Console.WriteLine("Enter your First Name: ");
            this.fName = InputString();
            Console.WriteLine("Enter your Last Name: ");
            this.lName = InputString();
            Console.WriteLine("Enter your Username: ");
            this.username = InputString();
            Console.WriteLine("Enter your Password: ");
            this.password = InputString();
            Console.WriteLine("Confirm if the information bellow is correct");
            Console.WriteLine($"First Name: {fName}\n" +
                $"Last Name: {lName}\n" +
                $"Username: {username}\n" +
                $"Password: ******");
            bool confirm = InputYesorNo();
            if(confirm)
            {
                Console.WriteLine("Thank you for creating an account at Kwik-e-mart!");
            }
            else
            {
                InputAccount();
            }
        }
        public bool CheckLogin(P0DatabaseContext context)
        {
            Console.WriteLine("Enter your Username: ");
            this.username = InputString();
            Console.WriteLine("Enter your Password: ");
            this.password = InputString();
            user = context.Customers.Where(x => x.Username == username).FirstOrDefault();
            if (context.Customers.Where(x => x.Username == username).FirstOrDefault()!=null)
            {
                if (context.Customers.Where(x => x.UserPassord == password).FirstOrDefault() != null)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Your password was incorrect, please try again");
                }
            }
            else
            {
                Console.WriteLine("That username does not exist, please try again");
            }
            return CheckLogin(context);
            
        }
        public Customer CreateAccount(P0DatabaseContext context)
        {
            var user = new Customer();
            user.FirstName = fName;
            user.LastName = lName;
            user.Username = username;
            user.UserPassord = password;

            //this.user = user;
            context.Customers.Add(user);
            context.SaveChanges();

            return user;

            //var result = context.Customers.Where(s => s.FirstName == "Adam").FirstOrDefault<Customer>();
            //Console.WriteLine(result.FirstName);
        }
        public Customer InitRegistration(P0DatabaseContext context)
        {
            int choice;
            bool registered = false;
            WelcomeMessage();
            LoginMessage();
            choice = InputChoice(3);
            switch (choice)
            {
                case 1:
                    InputAccount();
                    user = CreateAccount(context);
                    registered = true;
                    break;
                case 2:
                    registered = CheckLogin(context);
                    break;
                case 3:
                    return null;
                default:
                    InitRegistration(context);
                    break;
            }
            return user;

        }

    }// end of class
}//end of project
