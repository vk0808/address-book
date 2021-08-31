using System;

namespace AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {

            AddressBook addressBook = new AddressBook();

            // Call Welcome message
            addressBook.welcome();

            // Call address book menu with operations
            addressBook.Selection();


            // Call read and read csv
            addressBook.WriteCSV();
            addressBook.ReadCSV();

            // Call read and write json
            addressBook.WriteJSON();
            addressBook.ReadJSON();
        }
    }
}
