using System;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {

            //System.Console.WriteLine("hello");
            Game game = new Game();
            
            game.WelcomeMessage();
            game.ReadRoundMenu();
            game.InitRound();
        }
    }

    
}
