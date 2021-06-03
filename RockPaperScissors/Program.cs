using System;

namespace RockPaperScissors
{
    class Program
    {
        public enum rpsChoice{
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Rock Paper Scissors\nWhat is your name?");
            string name = Console.ReadLine();
            int winningNum = 2;
            int wins = 0;
            int losses = 0;
            int choice = -1;
            while(choice !=4){
                while(wins < winningNum && losses < winningNum){
                    // check if input is a number in range
                    bool succesfulConversion = false;
                    do{
                        Console.WriteLine("Please make a choice\n 1. Rock\n 2. Paper \n 3. Scissors\n 4. Quit");
                        string choiceStr = Console.ReadLine();
                        succesfulConversion = Int32.TryParse(choiceStr, out choice);

                        if(choice > 4 || choice < 1){
                            Console.WriteLine($"You inputted {choice}. That is not a valid choice!");
                        }
                        else if(!succesfulConversion){
                            Console.WriteLine($"You inputted {choiceStr}. That is not a valid choice!");
                        }

                    }while(!succesfulConversion && (choice > 1 && choice < 4));

                    if(choice!=4){
                        // generate random number for computer
                        Random random = new Random();
                        int computerChoice = random.Next(1,Enum.GetNames(typeof(rpsChoice)).Length);
                        Console.WriteLine($"The player chose {choice}");
                        Console.WriteLine($"The computer chose {computerChoice}");

                        if(choice == 1 && computerChoice == 2
                        || choice == 2 && computerChoice == 3
                        || choice == 3 && computerChoice == 1){
                            Console.WriteLine("Computer Wins");
                            losses++;
                        }
                        else if(choice == computerChoice){
                            Console.WriteLine("Tie!");
                        }
                        else{
                            Console.WriteLine($"{name} Wins");
                            wins++;
                        }
                        Console.WriteLine($"{name}: {wins}\nComputer: {losses}");
                    }
                }
                if(choice!=4){
                    Console.WriteLine("Would you like to play again?\n 1. Yes\n 4. Quit");
                    string choiceStr = Console.ReadLine();
                    bool succesfulQuitConversion = Int32.TryParse(choiceStr, out choice);
                    if(choice == 1){
                        wins = 0;
                        losses = 0;
                    }
                    
                }
            }
            
            
            
        }
    }
}
