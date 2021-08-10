using System;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {

            AddressBook addressBook = new AddressBook();

            addressBook.welcome();

            addressBook.Selection();
        }
    }
}
