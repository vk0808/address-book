using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    public class Person
    {
        public string firstName;
        public string lastName;
        public string address;
        public string city;
        public string state;
        public string zip;
        public string phoneNumber;
        public string email;

        public Person(string firstNname, string lastName, string address, string city, string state, string zip, string phoneNumber, string email)
        {
            this.firstName = firstNname;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
    }
}
