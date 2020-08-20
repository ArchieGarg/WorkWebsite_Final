using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Login_Form.Controllers
{
    public class ParentController : SuperParentController
    {
        private static bool once = true;
        private static List<Models.User> users = new List<Models.User>();

        public ParentController()
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

                        if (((String)(reader["Tenant"])).TrimEnd().Equals("Tenant2"))
                        {
                            Models.User newUser = new Models.User();
                            newUser.SetUser((string)reader["Username"]);
                            newUser.SetPass((string)reader["Password"]);
                            ParentController.getUsers().Add(newUser);
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

        public static List<Models.User> getUsers()
        {
            return users;
        }

        public static bool AddUser(String user, String pass, String auth)
        {

            if (!String.Equals(auth, "SignUpToken"))
                return false;
            Models.User newUser = new Models.User();
            newUser.SetUser(user);
            newUser.SetPass(pass);
            users.Add(newUser);
            SuperParentController.AddUser(user, pass,"Tenant2", auth);
            return true;
        }

        public static bool SetFriendlyName(String userName, String friendlyName)
        {

            foreach (Models.User currentUser in users)
            {

                if (String.Equals(currentUser.getUser().TrimEnd(), userName))
                {
                    Debug.WriteLine("Successfully Wrote Friendly Name for t2");
                    currentUser.SetFiendlyName(friendlyName);
                    return true;
                }
            }
            return false;
        }
    }
}
