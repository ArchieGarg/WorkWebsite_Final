using Login_Form.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace Login_Form.Controllers
{
    public class HomeController : ParentController
    {

        [HttpPost]
        public bool PostAdd(String u, String n, int q, double c, String d)
        {
            foreach (Models.User currentUser in getUsers())
            {

                if (String.Equals(currentUser.GetFriendlyName(), u))
                {
                    currentUser.Add(n, q, c, d);
                    return true;
                }
            }
            Debug.WriteLine("user not found in home controller()!");
            return false;
        }

        [HttpGet]
        public bool GetRemove(String u, int i)
        {
            foreach (Models.User currentUser in getUsers())
            {

                if (String.Equals(currentUser.GetFriendlyName(), u))
                {
                    currentUser.Remove(i);
                    return true;
                }
            }
            return false;
        }

        [HttpGet]
        public NewStoreItem[] GetAll(String u)
        {
            foreach (Models.User currentUser in getUsers())
            {

                if (String.Equals(currentUser.GetFriendlyName(), u))
                {
                    return currentUser.GetAll();
                }
            }
            return null;
        }
    }
}
