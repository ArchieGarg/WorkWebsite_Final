using Login_Form.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Login_Form.Controllers
{
    public class NewParentController : SuperParentController
    {
        private static bool once = true;
        private static Object lockObject = new Object();
        private static List<NewUser> users = new List<NewUser>();

        public NewParentController()
        {
            if (!once)
            {
                return;
            }
            using (SqlConnection connection = new SqlConnection("Server = uniontrackinternsql.database.windows.net; Database = ArchieSQL; User Id = uniontrack; Password = Kinsella9011;"))
            {
                SqlCommand command = new SqlCommand("select * from Login_Details", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine(String.Format("{0}:{1}", reader["Username"], reader["Password"]));

                        if (((String)(reader["Tenant"])).TrimEnd().Equals("Tenant1"))
                        {
                            NewUser tempUser = new NewUser();
                            tempUser.SetUser((string)reader["Username"]);
                            tempUser.SetPass((string)reader["Password"]);
                            users.Add(tempUser);
                        }
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
                once = !(once);
            }
        }

        public static bool AddUser(String user, String pass, String auth)
        {
            if (!String.Equals(auth, "SignUpToken"))
                return false;
            lock (lockObject)
            {
                NewUser tempUser = new NewUser();
                tempUser.SetUser(user);
                tempUser.SetPass(pass);
                users.Add(tempUser);
            }
            return SuperParentController.AddUser(user, pass, "Tenant1", auth);
        }

        public static NewUser GetUser(String u)
        {
            foreach (NewUser currentUser in users)
            {
                if (String.Equals(u, currentUser.GetFriendlyName()))
                {
                    return currentUser;
                }
            }
            return null;
        }

        [HttpPost]
        public void PostRemoveUser(String user, String auth)
        {
            Debug.WriteLine("Removing User: " + user);
            if (!auth.Equals("P4s9LnYKCquF4CVU"))
                return;

            Debug.WriteLine("here");
            Debug.WriteLine(NewLoginController.uniqueID + " user: " +  user);
            lock (lockObject) { 
                users.Remove(GetUser(user)); 
            }
            Char[] digits = new Char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            user = user.TrimEnd(digits);
            using (SqlConnection connection = new SqlConnection("Server=uniontrackinternsql.database.windows.net;Database=ArchieSQL;User Id=uniontrack;Password=Kinsella9011;"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(null, connection);
                // Create and prepare an SQL statement.
                command.CommandText =
                    "DELETE from Login_Details where Username='" + user + "'";
                Debug.WriteLine(command.ExecuteNonQuery());
            }
        }

        [HttpPost]
        public bool PostChangePassword(String user, String oldPass, String newPass)
        {
            NewUser user_ = GetUser(user);
            Debug.WriteLine(user_.getPass());
            Debug.WriteLine(oldPass);
            Debug.WriteLine(!String.Equals(user_.getPass().TrimEnd(), oldPass));
            if (!String.Equals(user_.getPass().TrimEnd(), oldPass))
                return false;
            user_.SetPass(newPass);
            //changes password using non-prepared statements
            using (SqlConnection connection = new SqlConnection("Server=uniontrackinternsql.database.windows.net;Database=ArchieSQL;User Id=uniontrack;Password=Kinsella9011;"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(null, connection);
                // Create and prepare an SQL statement.
                command.CommandText =
                    "update Login_Details set Password='" + newPass + "'" +" where Username='" + user + "'";
                Debug.WriteLine(command.ExecuteNonQuery());
            }
            return true;
        }

        public List<NewStoreItem> GetUserCart(String user)
        {
            
            foreach(NewUser currentUser in users)
            {
                Debug.WriteLine("in NewParent, GetUserCart() with friendly name: " + currentUser.GetFriendlyName());
                if (String.Equals(user, currentUser.GetFriendlyName()))
                {
                    return currentUser.GetCart();
                }
            }
            return null;
        }

        public Stack<NewStoreItem> GetUserStack(String u)
        {

            foreach (NewUser user in users)
            {
                if (String.Equals(user.GetFriendlyName(), u))
                    return user.GetStack();
            }
            return null;
        }

        public bool SetFriendlyName(String userName, String friendlyName)
        {

            lock (lockObject)
            {
                foreach (NewUser currentUser in users)
                {

                    if (String.Equals(currentUser.getUser().TrimEnd(), userName))
                    {
                        Debug.WriteLine("Successfully Wrote FriendlyName for t1");
                        currentUser.SetFriendlyName(friendlyName);
                        return true;
                    }
                }
                return false;
            }
        }
        public static List<NewUser> GetUsers()
        {
            return users;
        }
    }
}
