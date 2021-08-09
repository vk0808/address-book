using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    public class Persons
    {
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string PhoneNumber
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        public string Zip
        {
            get;
            set;
        }
    }

    public class AddressBook
    {
        List<Persons> People;

        public AddressBook()
        {
            People = new List<Persons>();
        }

        public string welcome()
        {
            return "Welcome to Address Book Program";
        }
    }
}
