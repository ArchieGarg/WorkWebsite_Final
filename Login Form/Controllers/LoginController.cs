using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Runtime.Remoting.Messaging;
using Login_Form.Models;
using System.Diagnostics;

namespace Login_Form.Controllers
{
    public class LoginController : ParentController
    {

        private static int uniqueID = 1;

        public HttpResponseMessage Validate(String user, String pass)
        {
            Console.WriteLine("In");
            Object[] userDetails = DoesExist(user, pass);
            if ((bool)userDetails[0] == true){
                Debug.WriteLine("Tenant 2 login ok");
                Models.User userInQuestion = (Models.User)userDetails[1];
                Debug.WriteLine(userInQuestion.getUser());
                userInQuestion.isLoggedIn = true;
                var resp = Request.CreateResponse();
                resp.Headers.Add("location", "/index.html");
                resp.Headers.Add("message", "Success");
                resp.Headers.Add("username", user + uniqueID);
                SetFriendlyName(user ,user + uniqueID);
                uniqueID++;
                return resp;
            }
            else
            {
                var resp = Request.CreateResponse();
                Models.User userInQuestion = (Models.User)userDetails[1];
                if (userInQuestion != null) {
                    
                    userInQuestion.numTimesWrong++;

                    if (userInQuestion.numTimesWrong == 5)
                    {
                        resp.Headers.Add("message", "LockedOut");
                        return resp;
                    }
                    else
                    {
                        resp.Headers.Add("message", "Failure");
                        return resp;
                    }
                }
                else
                {
                    resp.Headers.Add("message", "Failure");
                    return resp;
                }
            }
        }

        private Object[] DoesExist(String user, String pass)
        {

            Object[] toRet = new Object[2];
            foreach(Models.User currentUser in getUsers())
            {

                if (String.Equals(currentUser.getUser().TrimEnd(), user) && String.Equals(currentUser.getPass().TrimEnd(), pass))
                {
                    toRet[0] = true;
                    toRet[1] = currentUser;
                    return toRet;
                }else if (String.Equals(currentUser.getUser(), user))
                {

                    toRet[0] = false;
                    toRet[1] = currentUser;
                    return toRet;
                }
            }
            toRet[0] = false;
            toRet[1] = null;
            return toRet;
        }

        [HttpGet]
        public String Ready(String u)
        {
            foreach(Models.User currentUser in getUsers())
            {

                if (String.Equals(currentUser.getUser(), u))
                {
                    if(currentUser.numTimesWrong == 5){
                        return "No";
                    }
                    else
                    {
                        return "yes";
                    }
                }
            }
            return "yes";
        }

        [HttpGet]
        public void Reset(String u)
        {
            foreach (Models.User currentUser in getUsers())
            {

                if (String.Equals(currentUser.getUser(), u))
                {
                    currentUser.numTimesWrong = 0;
                }
            }
        }

        [HttpGet]
        public bool CanAccess(String u)
        {
            foreach (Models.User currentUser in getUsers())
            {

                Debug.WriteLine("in t2, friendly name is: " + currentUser.GetFriendlyName() + " user is: " + u);
                if (String.Equals(currentUser.GetFriendlyName(), u))
                {
                    return currentUser.isLoggedIn;
                }
            }
            return false;
        }

        [HttpGet]
        public HttpResponseMessage LogOut(String u)
        {

            Models.User currentUserPerson = null;
            foreach (Models.User currentUser in getUsers())
            {
                Debug.WriteLine("in login curr user: " + currentUser.GetFriendlyName());
                if (String.Equals(currentUser.GetFriendlyName(), u))
                {
                    currentUserPerson = currentUser;  
                }
            }
            currentUserPerson.isLoggedIn = false;
            var resp = Request.CreateResponse();
            resp.Headers.Add("location", "/index.html");
            resp.Headers.Add("message", "Success, you are now logged out!");
            return resp;
        }
    }
}
