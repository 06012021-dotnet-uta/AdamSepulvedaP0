using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P0DbContext;

namespace Project0
{
    class UserInterface : P0DatabaseContext
    {
        public int InputChoice(int maxOption)
        {
            bool succesfulConversion = false;
            int choice;
            string choiceStr = Console.ReadLine();
            succesfulConversion = Int32.TryParse(choiceStr, out choice);
            if (!succesfulConversion)
            {
                Console.WriteLine($"You inputted {choiceStr}. That is not a valid choice!");
                return InputChoice(maxOption);
            }
            else if (choice > maxOption || choice < 1)
            {
                Console.WriteLine($"You inputted number {choice}. That is not a valid choice!");
                return InputChoice(maxOption);
            }
            else
            {
                return choice;
            }
        }
        public bool InputYesorNo()
        {
            Console.WriteLine("(Yes or No?)");
            string input = Console.ReadLine().Trim().ToLower();
            if (input == "yes" || input == "y")
            {
                return true;
            }
            else if (input == "no" || input == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine($"{input} is not valid");
                return InputYesorNo();
            }

        }
        public string InputString()
        {
            string input = Console.ReadLine().Trim();
            if (input.Length > 20)
            {
                Console.WriteLine($"{input} is too long, try again");
                return InputString();
            }
            else if (input.Length < 1)
            {
                Console.WriteLine($"You have inputted nothing, try again");
                return InputString();
            }
            return input;
        }
    }
}
