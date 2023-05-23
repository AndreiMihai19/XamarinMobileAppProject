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
        private static string nume;



        public static String getNume()
        {
            return nume;
        }

        public static void setNume(string Nume)
        {
            nume = Nume;
        }
    }

    
}