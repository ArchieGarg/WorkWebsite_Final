using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_Form.Models
{
    public class User
    {
        private String user;
        private String pass;
        private String friendlyName;
        private List<NewStoreItem> items = new List<NewStoreItem>();
        private int currentIndex = 0;
        private String[] fruitsAndVegetables = new string[] { "Apple", "Carrot", "Banana", "Celery", "Tomatoe", "Tomato","Potato","Potatoe","Potata" };
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

        public void SetFiendlyName(String username)
        {
            friendlyName = username;
        }
        public bool Add(String n, int q, double c, String d)
        {

            if (currentIndex == 10)
            {
                return false;
            }

            String img = null;
            for (int item = 0; item < fruitsAndVegetables.Length; item++)
            {
                if (String.Equals(n, fruitsAndVegetables[item], StringComparison.OrdinalIgnoreCase))
                {
                    img = "images/" + fruitsAndVegetables[item] + ".png";
                    break;
                }
            }
            items.Add(new NewStoreItem { name = n, quantity = q, cost = c, description = d, image = img });
            currentIndex++;
            Console.WriteLine(currentIndex);
            return true;
        }

        public bool Remove(int i)
        {

            if (i < 0 || i > currentIndex)
                return false;

            items.RemoveAt(i);
            currentIndex--;
            return true;
        }

        public NewStoreItem[] GetAll()
        {
            return items.ToArray();
        }

    }
}