using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Globalization;
using CsvHelper;
using Newtonsoft.Json;

namespace AddressBook
{
    public class AddressBook
    {
        List<Person> People;
        List<Person> contacts;

        /// Constructor
        public AddressBook()
        {
            People = new List<Person>();
            ReadFromFile();
        }

        /// Displays welcome message
        public void welcome()
        {
            Console.WriteLine("Welcome to Address Book Program\n");
        }


        /// Method to read from file
        private void ReadFromFile()
        {
            string filePath = @"../../../Utility/AddressBook.txt";
            try
            {
                string[] fileContents = File.ReadAllLines(filePath);
                contacts = new List<Person>();
                foreach (string line in fileContents)
                {
                    if (line.Contains(","))
                    {
                        string[] address = line.Split(",");
                        if (!add(address[0], address[1], address[2], address[3], address[4], address[5], address[6], address[7]))
                        {
                            Console.WriteLine($"An address is already on file for {address[0]} {address[1]}\n");
                        }
                    }
                }
                Console.WriteLine("Address book successfully read!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        /// Method to write to csv file
        public void WriteCSV()
        {
            string exportFilePath = @"../../../Utility/Address.csv";


            using (var writer = new StreamWriter(exportFilePath))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                ////csvWriter.WriteRecords(People);

                //csvWriter.WriteField("First_Name");
                //csvWriter.WriteField("Last_Name");
                //csvWriter.WriteField("Phone_number");
                //csvWriter.WriteField("Email_ID");
                //csvWriter.WriteField("Address");
                //csvWriter.WriteField("City");
                //csvWriter.WriteField("State");
                //csvWriter.WriteField("ZIP_Code");
                //csvWriter.NextRecord();

                foreach (Person person in People)
                {
                    csvWriter.WriteField(person.firstName);
                    csvWriter.WriteField(person.lastName);
                    csvWriter.WriteField(person.phoneNumber);
                    csvWriter.WriteField(person.email);
                    csvWriter.WriteField(person.address);
                    csvWriter.WriteField(person.city);
                    csvWriter.WriteField(person.state);
                    csvWriter.WriteField(person.zip);
                    csvWriter.NextRecord();
                }
            }
            Console.WriteLine("\nWrite data successfully to Export.csv\n");
        }


        /// Method to read from csv file
        public void ReadCSV()
        {
            string importFilePath = @"../../../Utility/Address.csv";

            // config for no header
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(importFilePath))
            using (var csv = new CsvReader(reader, config))
            {
                IEnumerable<Person> records = csv.GetRecords<Person>();
                Console.WriteLine("\nRead data successfully from Address.csv\n");
                Console.Write($"\n{"firstname",10} |{"lastname",10} |{"phone",15} |{"email",25} |{"address",20} |{"city",15} |{"state",15} |{"zip-code",10} |\n");
                Console.Write($"{new string('-', 134)} |\n");

                foreach (Person addressData in records)
                {
                    Console.Write($"{addressData.firstName,10} |");
                    Console.Write($"{addressData.lastName,10} |");
                    Console.Write($"{addressData.phoneNumber,15} |");
                    Console.Write($"{addressData.email,25} |");
                    Console.Write($"{addressData.address,20} |");
                    Console.Write($"{addressData.city,15} |");
                    Console.Write($"{addressData.state,15} |");
                    Console.Write($"{addressData.zip,10} |\n");
                }
                Console.Write($"{new string('-', 134)} |\n\n");
            }
        }

        /// Method to read option
        public void Selection()
        {
            string choice = "";
            while (!choice.ToUpper().Equals("Q"))
            {
                displayMenu();
                Console.WriteLine("\nSelect an option: ");
                choice = Console.ReadLine();
                performAction(choice);
            }
        }

        /// Method to display menu
        private void displayMenu()
        {
            Console.WriteLine("\nMain Menu");
            Console.WriteLine("------------------------------");
            Console.WriteLine("V - View All Addresses");
            Console.WriteLine("A - Add an Address");
            Console.WriteLine("E - Edit an Address");
            Console.WriteLine("S - Search an Address");
            Console.WriteLine("D - Delete an Address");
            Console.WriteLine("O - Sort Addresses");
            Console.WriteLine("Q - Quit");
        }

        /// Method to perform operations
        private void performAction(string selection)
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            details.Add("First_Name", "");
            details.Add("Last_Name", "");
            details.Add("Phone_number", "");
            details.Add("Email_ID", "");
            details.Add("Address", "");
            details.Add("City", "");
            details.Add("State", "");
            details.Add("ZIP_Code", "");

            switch (selection.ToUpper())
            {
                /// Adding new Address
                case "A":
                    List<string> keys = new List<string>(details.Keys);
                    foreach (string key in keys)
                    {
                        Console.WriteLine($"\nEnter {key}");
                        details[key] = Console.ReadLine();
                    }

                    if (add(details["First_Name"], details["Last_Name"], details["Phone_number"], details["Email_ID"], details["Address"], details["City"], details["State"], details["ZIP_Code"]))
                    {
                        Console.WriteLine("\nAddress successfully added!");
                    }
                    else
                    {
                        Console.WriteLine($"\nAn address is already on file for {details["First_Name"]} {details["Last_Name"]}.");
                    }
                    break;

                /// View all addresses
                case "V":
                    if (isEmpty())
                    {
                        Console.WriteLine("\nThere are no entries.");
                    }
                    else
                    {
                        Console.Write("\nView by\n1. City\n2. State\n3. All\nEnter your option: ");
                        switch (int.Parse(Console.ReadLine()))
                        {
                            case 1:
                                Console.Write("\nEnter the name of city: ");
                                string cityName = Console.ReadLine();
                                Console.WriteLine("\nAddresses:");

                                viewByCityState("city", cityName);
                                break;

                            case 2:
                                Console.Write("\nEnter the name of state: ");
                                string stateName = Console.ReadLine();
                                Console.WriteLine("\nAddresses:");

                                viewByCityState("state", stateName);
                                break;
                            
                            case 3:
                                Console.WriteLine("\nAddresses:");
                                Console.WriteLine($"Count: {People.Count}");
                                view(People);
                                break;

                            default:
                                Console.WriteLine("\nInvalid option");
                                return;
                        }
                    }
                    break;

                /// Edit an existing address
                case "E":
                    Console.WriteLine("\nEnter the first name: ");
                    string editPersonFirst = Console.ReadLine();
                    Console.WriteLine("\nEnter the last name: ");
                    string editPersonLast = Console.ReadLine();

                    Person person = find(editPersonFirst, editPersonLast);
                    if (person == null)
                    {
                        Console.WriteLine($"\nAddress for {editPersonFirst} {editPersonLast} count not be found.");
                    }
                    else
                    {
                        Console.WriteLine("\nEnter index: \n0: First Name\n1: Last Name\n2: Phone number\n3: Email ID\n4: Address\n5: City\n6: State\n7: ZIP Code");
                        int editKey = int.Parse(Console.ReadLine());
                        switch (editKey)
                        {
                            case 0:
                                Console.WriteLine("\nEnter new First Name: ");
                                person.firstName = Console.ReadLine();
                                Console.WriteLine($"\nFirst Name updated for {editPersonFirst} {editPersonLast}");
                                break;
                            case 1:
                                Console.WriteLine("\nEnter new Last Name: ");
                                person.lastName = Console.ReadLine();
                                Console.WriteLine($"\nLast Name updated for {editPersonFirst} {editPersonLast}");
                                break;
                            case 2:
                                Console.WriteLine("\nEnter new Phone number: ");
                                person.phoneNumber = Console.ReadLine();
                                Console.WriteLine($"\nPhone Number updated for {editPersonFirst} {editPersonLast}");
                                break;
                            case 3:
                                Console.WriteLine("\nEnter new Email ID: ");
                                person.email = Console.ReadLine();
                                Console.WriteLine($"\nEmail Id updated for {editPersonFirst} {editPersonLast}");
                                break;
                            case 4:
                                Console.WriteLine("\nEnter new Address: ");
                                person.address = Console.ReadLine();
                                Console.WriteLine($"\nAddress updated for {editPersonFirst} {editPersonLast}");
                                break;
                            case 5:
                                Console.WriteLine("\nEnter new City: ");
                                person.city = Console.ReadLine();
                                Console.WriteLine($"\nCity updated for {editPersonFirst} {editPersonLast}");
                                break;
                            case 6:
                                Console.WriteLine("\nEnter new State: ");
                                person.state = Console.ReadLine();
                                Console.WriteLine($"\nState updated for {editPersonFirst} {editPersonLast}");
                                break;
                            case 7:
                                Console.WriteLine("\nEnter new ZIP Code: ");
                                person.zip = Console.ReadLine();
                                Console.WriteLine($"\nZIP Code updated for {editPersonFirst} {editPersonLast}");
                                break;
                            default:
                                Console.WriteLine("\nYou have entered wrong index");
                                break;
                        }
                    }
                    break;

                /// Delete an address
                case "D":
                    Console.WriteLine("\nEnter First Name to Delete: ");
                    string delPersonFirst = Console.ReadLine();
                    Console.WriteLine("\nEnter Last Name to Delete: ");
                    string delPersonLast = Console.ReadLine();

                    if (delete(delPersonFirst, delPersonLast))
                    {
                        Console.WriteLine("\nAddress successfully deleted");
                    }
                    else
                    {
                        Console.WriteLine($"\nAddress for {delPersonFirst} {delPersonLast} could not be found.");
                    }
                    break;


                // Search by city or state
                case "S":
                    string personName;
                    Console.Write("\nSearch by\n1. City\n2. State\nEnter your option: ");
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            Console.Write("\nEnter the name of city: ");
                            string cityName = Console.ReadLine();
                            Console.Write("\nEnter the name of person: ");
                            personName = Console.ReadLine();
                            SearchByCityState("city", cityName, personName);
                            break;
                        case 2:
                            Console.Write("\nEnter the name of state: ");
                            string stateName = Console.ReadLine();
                            Console.Write("\nEnter the name of person: ");
                            personName = Console.ReadLine();
                            SearchByCityState("state", stateName, personName);
                            break;
                        default:
                            Console.WriteLine("\nInvalid option");
                            return;
                    }
                    break;


                /// Quit from program
                case "Q":
                    Console.WriteLine("\nQuitting....");
                    break;


                /// Sort the list by city
                case "O":
                    SortList();
                    break;


                default:
                    Console.WriteLine("\nYou have entered wrong option");
                    break;
            }
        }

        /// Method to add address to Person list
        private bool add(string firstName, string lastName, string phoneNumber, string email, string address, string city, string state, string zip)
        {
            Person person = new Person(firstName, lastName, phoneNumber, email, address, city, state, zip);
            Person result = find(firstName, lastName);


            if (result != null)
            {
                return false;
            }
            else
            {
                People.Add(person);
                return true;
            }
        }

        /// Method to find an address from the list using firstname
        private Person find(string firstName, string lastName)
        {
            Person info = People.Find(a => (a.firstName == firstName && a.lastName == lastName));
            return info;
        }

        
        /// Method to check if address book is empty
        private bool isEmpty()
        {
            return (People.Count == 0);
        }

        /// Method to delete an address
        private bool delete(string firstName, string lastName)
        {
            Person person = find(firstName, lastName);

            if (person != null)
            {
                People.Remove(person);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// Method to view all addresses
        private void view(List<Person> records)
        {
            Console.Write($"\n{"firstname",10} |{"lastname",10} |{"phone",15} |{"email",25} |{"address",20} |{"city",15} |{"state",15} |{"zip-code",10} |\n");
            Console.Write($"{new string('-', 134)} |\n");

            foreach (Person addressData in records)
            {
                Console.Write($"{addressData.firstName,10} |");
                Console.Write($"{addressData.lastName,10} |");
                Console.Write($"{addressData.phoneNumber,15} |");
                Console.Write($"{addressData.email,25} |");
                Console.Write($"{addressData.address,20} |");
                Console.Write($"{addressData.city,15} |");
                Console.Write($"{addressData.state,15} |");
                Console.Write($"{addressData.zip,10} |\n");
            }
            Console.Write($"{new string('-', 134)} |\n\n");
        }

        /// Method to view all addresses by city or state
        private void viewByCityState(string type, string name)
        {
            if (type.ToLower() == "city")
            {
                List<Person> info = People.FindAll(a => (a.city == name));
                Console.WriteLine($"\nCount: {info.Count}");
                view(info);
            }
            else if (type.ToLower() == "state")
            {
                List<Person> info = People.FindAll(a => (a.state == name));
                Console.WriteLine($"\nCount: {info.Count}");
                view(info);
            }
            else
            {
                Console.WriteLine("\nPerson not found");
            }
        }

        /// Method to view person by city
        private void viewByCity(string personName, string name)
        {
            List<Person> info = People.FindAll(a => (a.firstName == personName && a.city == name));
            if (info.Count == 0)
            {
                Console.WriteLine("\nPerson not found");
            }
            else
            {
                view(info);
            }

        }

        /// Method to view person by state
        private void viewByState(string personName, string name)
        {
            List<Person> info = People.FindAll(a => (a.firstName == personName && a.state == name));
            if (info.Count == 0)
            {
                Console.WriteLine("\nPerson not found");
            }
            else
            {
                view(info);
            }
        }

        // Method to search
        private void SearchByCityState(string type, string name, string personName)
        {
            if (isEmpty())
            {
                Console.WriteLine("\nAdress book is empty");
            }
            else
            {
                if (type.ToLower() == "city")
                {
                    viewByCity(personName, name);
                }
                else if (type.ToLower() == "state")
                {
                    viewByState(personName, name);
                }
            }
        }

        /// Method to sort list by city
        private void SortList()
        {
            People = People.OrderBy(person => person.city).ToList();
            view(People);
        }
    }
}
