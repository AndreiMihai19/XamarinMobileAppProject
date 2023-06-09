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
    internal class Actions
    {
        static int action_id=0;
        static string device_id="nespecificat";
        static string action_type="nespecificata";
        static float value_action = 0;
        static DateTime action_time= DateTime.Now;

        public static void setActionId(int id)
        {
            action_id = id;
        }
        public static int getActionId()
        {
            return action_id;
        }

        public static void setDeviceId(string device)
        {
            device_id = device;
        }
        public static string getDeviceId()
        {
            return device_id;
        }

        public static void setActionType(string type)
        {
            action_type = type;
        }
        public static string getActionType()
        {
            return action_type;
        }

        public static void setValueAction(float value)
        {
            value_action = value;
        }
        public static float getValueAction()
        {
            return value_action;
        }

        public static void setActionTime(DateTime time)
        {
            action_time = time;
        }
        public static DateTime getActionTime()
        {
            return action_time;
        }
    }
}