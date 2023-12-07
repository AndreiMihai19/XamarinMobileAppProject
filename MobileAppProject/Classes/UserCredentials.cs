using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Org.Apache.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppProject.Classes
{
    class UserCredentials
    {
        private string deviceid;
        private string username;
        private string password;
        private string firstname;
        private string lastname;
        private string cnp;

        public UserCredentials(string deviceid, string username, string password, string firstname, string lastname, string cnp)
        {
            this.deviceid = deviceid;
            this.username = username;
            this.password = password;
            this.firstname = firstname;
            this.lastname = lastname;
            this.cnp = cnp;
        }
        public string GetDeviceID()
        {
            return deviceid;
        }

        public void SetDeviceID(string devid)
        {
            deviceid = devid;
        }

        public string GetUsername()
        {
            return username;
        }

        public void SetUsername(string usrnm)
        {
            username = usrnm;  
        }

        public string GetPassword()
        {
            return password;
        }

        public void SetPassword(string pswrd)
        {
            password = pswrd;
        }

        public string GetFirstName()
        {
            return firstname;
        }

        public void SetFirstName(string firstnm)
        {
            firstname = firstnm;
        }

        public string GetLastName()
        {
            return lastname;
        }

        public void SetLastName(string lastnm)
        {
            lastname = lastnm;
        }

        public string GetCNP()
        {
            return cnp;
        }

        public void SetCNP(string cnpp)
        {
            cnp = cnpp;
        }
    }


}