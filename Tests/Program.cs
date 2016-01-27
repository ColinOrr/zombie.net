using System;
using Zombie;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic zombie = new ZombieDriver();
            zombie.visit("http://www.google.com");
            zombie.assert.text("title", "Google");
            Console.WriteLine(zombie.html());
        }
    }
}
