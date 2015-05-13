using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;

namespace SqlClient_Contacts.Models
{
    public class ContactDataAccess
    {
        public static bool InsertContact(string name, string email, string title)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open
                con.Open();
                try
                {
                    //sql call
                    using (SqlCommand command = new SqlCommand("INSERT INTO Contacts VALUES(@name,@email,@title)", con))
                    {
                        //parameters to avoid Injection
                        command.Parameters.Add(new SqlParameter("name", name));
                        command.Parameters.Add(new SqlParameter("email", email));
                        command.Parameters.Add(new SqlParameter("title", title));
                        //execute
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool DeleteContact(int id)
        {
            //make connection
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open the connection
                con.Open();
                //try to access
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("DELETE FROM Contacts WHERE ContactId = @id",con))
                    {
                        //make parameter
                        command.Parameters.Add(new SqlParameter("id", id));
                        //execute
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public static Contact GetContactById(int id)
        {
            //add all contacts to new list
            List<Contact> allContacts = new List<Contact>();
            //make connection 
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open connection
                con.Open();
                //try connection
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Contacts", con))
                    {
                        //make reader
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int cId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string email = reader.GetString(2);
                            string title = reader.GetString(3);
                            allContacts.Add(new Contact(cId, name, email, title));
                        }
                        return allContacts.Where(x=> x.ContactId == id).First();
                    }
                }
                catch
                {
                    return new Contact();
                }
            }
        }

        public static List<Contact> GetAllContacts() 
        {
            //make connection
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open connection
                con.Open();
                //try connection
                try
                {
                    //make command
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Contacts",con))
                    {
                        //add all contacts to new list
                        List<Contact> allContacts = new List<Contact>();
                        //make reader
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string email = reader.GetString(2);
                            string title = reader.GetString(3);
                           allContacts.Add(new Contact(id,  name, email, title));
                        }
                        return allContacts;
                    }
                }
                catch
                {
                    return new List<Contact>();
                }
            }
        }

        public static bool UpdateContact(int id, string name, string email, string title)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //open
                con.Open();
                try
                {
                    //sql call
                    using (SqlCommand command = new SqlCommand("UPDATE Contacts SET Name = @name, Email = @email, Title = @title WHERE ContactId = @id ", con))
                    {
                        //parameters to avoid Injection
                        command.Parameters.Add(new SqlParameter("id", id));
                        command.Parameters.Add(new SqlParameter("name", name));
                        command.Parameters.Add(new SqlParameter("email", email));
                        command.Parameters.Add(new SqlParameter("title", title));
                        //execute
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

    }
}