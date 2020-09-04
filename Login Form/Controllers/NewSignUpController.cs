using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Login_Form.Controllers
{
    public class NewSignUpController : NewParentController
    {
        [HttpPost]
        public bool AddToList(String user, String pass)
        {

            if (AddUser(user, pass, "SignUpToken") == false)
                throw new Exception("could not authenticate");
            System.Diagnostics.Debug.WriteLine("Sign user: " + user + " in new signup c()");
            return true;
        }
    }
}
