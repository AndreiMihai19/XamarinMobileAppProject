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
        private static int DoorStatus;
        private static float Temperature;
        private static int Light;
        private static string CurrentPreset;

        public static float getTemperature()
        {
            return Temperature;
        }

        public static void setTemperature(float temperature)
        {
            Temperature = temperature;
        }

        public static int getLight()
        {
            return Light;
        }

        public static void setLight(int light)
        {
            Light = light;
        }

        public static void setDoorStatus(int status)
        {
            DoorStatus = status;
        }


        public static int getDoorStatus()
        {
            return DoorStatus;
        }

        public static string getCurrentPreset()
        {
            return CurrentPreset;
        }

        public static void setCurrentPreset(string preset) 
        {
            CurrentPreset = preset;
        }

    }
}