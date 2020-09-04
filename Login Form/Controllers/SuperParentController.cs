using Login_Form.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Login_Form.Controllers
{
    public class SuperParentController : ApiController
    {

        public static bool AddUser(String user, String pass, String tenant, String auth)
        {

            if (!String.Equals(auth, "SignUpToken"))
                return false;
            using (SqlConnection connection = new SqlConnection("Server=uniontrackinternsql.database.windows.net;Database=ArchieSQL;User Id=uniontrack;Password=Kinsella9011;"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(null, connection);

                // Create and prepare an SQL statement.
                command.CommandText =
                    "INSERT INTO Login_Details (Username, Password, Tenant) " +
                    "VALUES (@username, @password, @Tenant)";

                SqlParameter idParam = new SqlParameter("@username", SqlDbType.NVarChar, 100);
                SqlParameter descParam = new SqlParameter("@password", SqlDbType.NVarChar, 100);
                SqlParameter tenantParam = new SqlParameter("@Tenant", SqlDbType.NVarChar, 100);

                idParam.Value = user;
                descParam.Value = pass;
                tenantParam.Value = tenant;

                command.Parameters.Add(idParam);
                command.Parameters.Add(descParam);
                command.Parameters.Add(tenantParam);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                command.ExecuteNonQuery();
            }
            return true;
        }
    }
}
