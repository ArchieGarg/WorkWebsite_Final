using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_Form.Models
{
    public class NewUser
    {
        private String user;
        private String pass;
        private String friendlyName;
        private List<NewStoreItem> cart = new List<NewStoreItem>();
        private Stack<NewStoreItem> pastDeletions = new Stack<NewStoreItem>();
        private int currentIndex = 0;
        public bool isLoggedIn = false;
        public int numTimesWrong = 0;

        public String getUser()
        {
            return user;
        }

        public String getPass()
        {
            return pass;
        }

        public String GetFriendlyName()
        {
            return friendlyName;
        }

        public void SetUser(String user)
        {
            this.user = user;
        }

        public void SetPass(String pass)
        {

            this.pass = pass;
        }

        public void SetFriendlyName(String username)
        {
            friendlyName = username;
        }
        public bool Add(String n, int q, double c)
        {

            cart.Add(new NewStoreItem { name = n, quantity = q, cost = c,});
            currentIndex++;
            return true;
        }

        public bool Remove(int i)
        {

            if (i < 0 || i > currentIndex)
                return false;

            cart.RemoveAt(i);
            currentIndex--;
            return true;
        }

        public List<NewStoreItem> GetCart()
        {
            return cart;
        }

        public Stack<NewStoreItem> GetStack()
        {
            return pastDeletions;
        }

        public IEnumerable<NewStoreItem> GetAll()
        {
            return cart;
        }

    }
}