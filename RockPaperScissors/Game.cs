using System;

namespace RockPaperScissors
{
    public enum RPS{
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
    public class Game
    {
        private int winningNum;
        public Player player1;
        public Player player2;
        //Contructor
        public Game(){
            this.winningNum = 2;
            this.player1 = new Player();
            this.player2 = new Player();
            this.player1.Name = "Player 1";
            this.player2.Name = "Player 2";
        }
        //Welcome Message
        public void WelcomeMessage(){
            Console.WriteLine("- - - - - - Welcome to Rock Paper Scissors - - - - - - \n");
        }
        // Reads Player Name
        public void InputPlayerName(){
            System.Console.WriteLine("What is your name?");
            this.player1.Name = Console.ReadLine();
            //this.player1.Name = "Adam";
            this.player2.Name = "Computer";
        }
        //Read Menu of choices
        public void ReadRoundMenu(){
            Console.WriteLine("Please make a choice\n 1. Rock\n 2. Paper \n 3. Scissors\n 4. Quit");
        }
        public void ReadQuitMenu(){
            Console.WriteLine("Would you like to play again?\n 1. Yes\n 2. No");
        }
        //Collect Input and check if input is in range
        public int CollectInput(int maxOption){
            bool succesfulConversion = false;
            int choice;
            string choiceStr = Console.ReadLine();
            succesfulConversion = Int32.TryParse(choiceStr, out choice);
                    if(!succesfulConversion){
                        Console.WriteLine($"You inputted {choiceStr}. That is not a valid choice!");
                        return CollectInput(maxOption);
                        }
                    else if(choice > maxOption || choice < 1){
                        Console.WriteLine($"You inputted number {choice}. That is not a valid choice!");
                        return CollectInput(maxOption);
                    }
                    else{
                        return choice;
                    }
        }
        public int GenerateComputerChoice(){
            Random random = new Random();
            //int computerChoice = random.Next(1,Enum.GetNames(typeof(rpsChoice)).Length);
            int computerChoice = random.Next(1,4);
            return computerChoice;
        }
        // initializ a round of rps
        public void InitRound(){
            int choice = -1;
            int wins = 0;
            int losses = 0;
            int roundmaxOption = 4;
            int quitMaxOption = 2;
            while((wins < winningNum && losses < winningNum)){
                    // collects input and checks if input is a number in range
                    choice = this.CollectInput(roundmaxOption);
                    if(choice == roundmaxOption){
                        break;
                    }
                    else if(choice!=roundmaxOption){
                        // generate random number for computer
                        int computerChoice = GenerateComputerChoice();
                        Console.WriteLine($"{player1.Name} chose {(RPS)choice}");
                        Console.WriteLine($"The {player2.Name} chose {(RPS)computerChoice}\n");

                        if(choice == 1 && computerChoice == 2
                        || choice == 2 && computerChoice == 3
                        || choice == 3 && computerChoice == 1){
                            Console.WriteLine($"{player2.Name} Wins\n");
                            losses++;
                        }
                        else if(choice == computerChoice){
                            Console.WriteLine($"{player1.Name} ties with {player2.Name}\n");
                        }
                        else{
                            Console.WriteLine($"{player1.Name} Wins\n");
                            wins++;
                        }
                        Console.WriteLine($"{player1.Name}: {wins}\nComputer: {losses}");
                        
                    }
                }
                if(wins > losses){
                    System.Console.WriteLine($"Congratulations {player1.Name} has won the round");
                }
                else{
                    System.Console.WriteLine($"Congratulations {player2.Name} has won the round");
                }
                this.ReadQuitMenu();
                choice = this.CollectInput(quitMaxOption);
                if(choice == 1){
                    ReadRoundMenu();
                    InitRound();
                }
                else if(choice == 2){
                    System.Console.WriteLine("Thank you for playing!");
                }
                
        }
    }
}