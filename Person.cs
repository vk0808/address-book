using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    public class Person
    {
        public string firstName;
        public string lastName;
        public string phoneNumber;
        public string email;
        public string address;
        public string city;
        public string state;
        public string zip;

        public Person(string firstName, string lastName, string phoneNumber, string email, string address, string city, string state, string zip)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
        }
    }
}
