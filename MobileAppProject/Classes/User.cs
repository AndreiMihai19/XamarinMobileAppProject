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
    internal class User
    {
        private static string username="ion";
        private static string password;
        private static string IMEI = "";
        public static bool isAdmin = false;


        public static void setUser(string Username)
        {
            username = Username;
        }

        public static string getUser()
        {
            return username;
        }

        public static string getIMEI()
        {
            return IMEI;
        }

        public static void setIMEI(string imei) 
        {
            IMEI = imei;
        }


    }

    

}