using Login_Form.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Login_Form.Controllers
{
    public class NewLoginController : NewParentController
    {
        public static int uniqueID = 1;

        public HttpResponseMessage Validate(String user, String pass)
        {
            Debug.WriteLine("User: " + user + " pass: " + pass);
            Object[] userDetails = DoesExist(user, pass);
            if ((bool)userDetails[0] == true)
            {
                Debug.WriteLine("Exists");
                NewUser userInQuestion = (NewUser)userDetails[1];
                userInQuestion.isLoggedIn = true;
                var resp = Request.CreateResponse();
                resp.Headers.Add("location", "/temp.html");
                resp.Headers.Add("message", "Success");
                resp.Headers.Add("username", user + uniqueID);
                SetFriendlyName(user, user + uniqueID);
                uniqueID++;
                return resp;
            }
            else
            {
               
                var resp = Request.CreateResponse();
                Models.NewUser userInQuestion = (Models.NewUser)userDetails[1];
                if (userInQuestion != null)
                {

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
            foreach (NewUser currentUser in GetUsers())
            {
                Debug.WriteLine(currentUser.getUser().TrimEnd() + " " + currentUser.getPass().TrimEnd());
                if (String.Equals(currentUser.getUser().TrimEnd(), user) && String.Equals(currentUser.getPass().TrimEnd(), pass))
                {
                    toRet[0] = true;
                    toRet[1] = currentUser;
                    return toRet;
                }
                else if (String.Equals(currentUser.getUser().TrimEnd(), user))
                {

                    toRet[0] = false;
                    toRet[1] = currentUser;
                    return toRet;
                }
            }
            Debug.WriteLine("here");
            toRet[0] = false;
            toRet[1] = null;
            return toRet;
        }

        [HttpGet]
        public String Ready(String u)
        {
            foreach (NewUser currentUser in GetUsers())
            {

                if (String.Equals(currentUser.GetFriendlyName(), u))
                {
                    if (currentUser.numTimesWrong == 5)
                    {
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
            foreach (NewUser currentUser in GetUsers())
            {

                if (String.Equals(currentUser.GetFriendlyName(), u))
                {
                    currentUser.numTimesWrong = 0;
                }
            }
        }

        [HttpGet]
        public bool CanAccess(String u)
        {
            Debug.WriteLine("in canAccess()");
            foreach (NewUser currentUser in GetUsers())
            {

                Debug.WriteLine("friendly name: " + currentUser.GetFriendlyName());
                if (String.Equals(currentUser.GetFriendlyName(), u))
                {
                    Debug.WriteLine("user found!");
                    return currentUser.isLoggedIn;
                }
            }
            return false;
        }

        [HttpGet]
        public HttpResponseMessage LogOut(String u)
        {

            NewUser currentUserPerson = null;
            foreach (NewUser currentUser in GetUsers())
            {

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
