using System;

using Struct.Core;

namespace Test
{
    class Program
    {
        // local storage
        private static LocalStorage localStorage = new LocalStorage();

        static void Main(string[] args)
        {
            // if player info doesn't exist, create it
            if (!localStorage.Exists("PlayerInfo"))
                localStorage.Set("PlayerInfo",
                    new Player
                    {
                        Name = "Jim Roton",
                        Phone = "214.735.0934",
                        Address = new Address()
                        {
                            Address1 = "2316 Elm Valley Dr",
                            City = "Little Elm",
                            State = "TX",
                            Zip = "75068"
                        }
                    }
                );

            // get player info from storage
            Player p = localStorage.Get<Player>("PlayerInfo");

            // write it out
            Console.Write(p);
            Console.WriteLine("");

            if (!localStorage.Exists("SwordOfDestiny"))
                localStorage.Set("SwordOfDestiny",
                    new Sword
                    {
                        Name = "SwordOfDestiny",
                        Damage = "2D6"
                    }
                );

            Sword sword = localStorage.Get<Sword>("SwordOfDestiny");

            Console.Write(sword);
        }
    }
}
