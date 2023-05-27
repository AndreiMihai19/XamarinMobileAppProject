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
    internal class Parameters
    {
        public static int DoorStatus;
        public static float Temperature;
        public static int Light;


        public static int getDoorStatus()
        {
            return DoorStatus;
        }

        public static float getTemperature()
        {
            return Temperature;
        }

        public static int getLight()
        {
            return Light;
        }

        public static void setDoorStatus(int status)
        {
            DoorStatus = status;
        }

        public static void setTemperature(float temperature)
        {
            Temperature = temperature;
        }
        public static void setLight(int light)
        {
            Light = light;
        }

    }
}