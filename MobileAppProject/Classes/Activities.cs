using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Awt.Font;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppProject.Classes
{
    internal class Activities
    {
        private static int preset_id;
        private static string nume;
        private static string device_id;
        private static string option_code;



        public static String getNume()
        {
            return nume;
        }

        public static void setNume(string Nume)
        {
            nume = Nume;
        }

        public static int getPresetId()
        {
            return preset_id;
        }

        public static void setPresetId(int id)
        {
            preset_id = id;
        }

        public static String getDeviceId()
        {
            return device_id;
        }

        public static void setDeviceId(string deviceId)
        {
            device_id = deviceId;
        }
        public static String getOptionCode()
        {
            return option_code;
        }

        public static void setOptionCode(int temperature, int light)
        {
            string cod;
            if((temperature<30)&&(temperature>10)) 
            {
                cod= temperature.ToString();
                if((light>=0)&&(light<10)) 
                {
                    cod += "00"+light.ToString();
                }
                else
                {
                    if ((light >9) && (light < 100))
                    {
                        cod += "0" + light.ToString();
                    }
                    else
                    {
                        if (light ==100)
                        {
                            cod += light.ToString();
                        }
                    }
                }
            }
            else
            {
                cod = null;
            }

            option_code = cod;
        }

    }

    
}