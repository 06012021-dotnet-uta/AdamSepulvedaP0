using System;

namespace RockPaperScissors
{
    public class Player
    {
        private string name;
        private int score;
        public string Name 
        {
            get
            {
                return this.name;
            } 
            set
            {
                if(value.Length>20 || value.Length < 1){
                    throw new InvalidOperationException("that name is too long");
                }
                this.name = value;
            }
        }
        //Constructor
        public Player(){
            this.name = "default name";
            this.score = 0;
        }
        public string CollectNameInput(){
            string name = Console.ReadLine();
            return name;
        }


    }
}