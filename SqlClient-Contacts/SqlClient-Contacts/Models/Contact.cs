using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqlClient_Contacts.Models
{
    public class Contact
    {
        //properties
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        //empty constructor
        public Contact() { }
        //constuctor with arguments
        public Contact(int contactId, string name, string email, string title)
        {
            this.ContactId = contactId;
            this.Name = name;
            this.Email = email;
            this.Title = title;
        }
    }
}