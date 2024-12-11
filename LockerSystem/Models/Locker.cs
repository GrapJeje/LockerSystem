using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerSystem.Models
{
    public class Locker
    {
        public int Number { get; }
        public bool Occupied { get; private set; }
        private int? Password { get; set; }

        public Locker(int number)
        {
            Number = number;
            Occupied = Main.Config.GetOccupied(number);
            Password = Main.Config.GetPassword(number);
        }

        public static Locker get(int number) => new Locker(number);
        public int getNumber() => Number;
        public bool isOccupied() => Occupied;
        public int? getPassword()
        {
            if (!Occupied)
                return null;
            return Password;
        }

        public void OpenLocker(int password)
        {
            if (Occupied) throw new ArgumentException("Kluis is al bezet.");

            this.SetPassword(password);
            Occupied = true;

            Main.Config.SetLocker(Number, Occupied, password);
        }

        public void CloseLocker()
        {
            if (!Occupied) throw new ArgumentException("Kluis is niet bezet.");

            Main.Config.SetLocker(Number, false, null);
        }

        private void SetPassword(int password)
        {
            if (password < 1000 || password > 9999)
                throw new ArgumentException("Wachtwoord moet minimaal 4 cijfers lang zijn!");

            Password = password;
        }
    }
}
