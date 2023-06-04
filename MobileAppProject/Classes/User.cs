using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppProject.Classes
{
    internal class User
    {
        private static string username= null;
        private static string password = null;
        private static string deviceId = null;
        public static bool isAdmin = false;


        public static void setUser(string Username)
        {
            username = Username;
        }

        public static string getUser()
        {
            return username;
        }

        public static string getDeviceId()
        {
            return deviceId;
        }

        public static void setDeviceId(string deviceid) 
        {
            deviceId = deviceid;
        }
    }

    

}