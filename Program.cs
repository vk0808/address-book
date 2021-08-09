using System;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {

            AddressBook addressBook = new AddressBook();

            Console.WriteLine(addressBook.welcome());

            addressBook.Selection();
        }
    }
}
