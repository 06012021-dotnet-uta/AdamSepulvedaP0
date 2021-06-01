using System;

namespace HelloWorldProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is your age?");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What is your name?");
            string name = Console.ReadLine();
            Console.WriteLine($"My name is {name} and I am {age} years old");
        }
    }
}
