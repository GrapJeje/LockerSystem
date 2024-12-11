using LockerSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerSystem
{
    internal class Main
    {
        public static LockersConfig Config = new LockersConfig();

        public static void main(string[] args)
        {
            while (true)
            {
                switch (MenuInput(SendConsoleMessage("Voer jouw keuze in: \n"), 4))
                {
                }
            }
        }

        static string SendConsoleMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static int MenuInput(string input, int max)
        {
            if (input.ToLower() == "x")
            {
                Environment.Exit(0);
            }

            if (!int.TryParse(input, out int user_choice))
            {
                Console.WriteLine("Ongeldige invoer, voer een getal in.\n");
                return 0;
            }

            if (!(user_choice >= 1 && user_choice <= max))
            {
                Console.WriteLine($"Ongeldige keuze, kies uit 1 t/m {max}.\n");
                return 0;
            }

            return user_choice;
        }

        static void ChoiceMenu()
        {
            Console.WriteLine("-------------------------------------------\n"
                + "Welkom in het adresboek!\n"
                + "Maak een keuze. \n"
                + "\n1 - Beschikbare kluizen.\n"
                + "2 - Een nieuwe kluis openen.\n"
                + "3 - Kluis tijdelijk openen.\n"
                + "4 - Een kluis permanent openen.\n"
                + "\nx - Voer 'x' in om te stoppen.\n"
                + "\n-------------------------------------------\n");
        }
    }
}
