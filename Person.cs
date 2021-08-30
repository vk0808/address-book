using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }

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
