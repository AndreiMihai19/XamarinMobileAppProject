using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppProject.Classes
{
    class ListOfUsers
    {
        private static List<UserCredentials> users=new List<UserCredentials>();

        public static void AddUser(UserCredentials user)
        {
            users.Add(user);
        }

        public static void RemoveUser(UserCredentials user)
        {
            users.Remove(user);
        }

        public static List<UserCredentials> GetList()
        {
            return users;
        }
    }
}