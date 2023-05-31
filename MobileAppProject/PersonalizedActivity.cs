﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppProject
{
    [Activity(Label = "PersonalizedActivity")]
    public class PersonalizedActivity : Activity
    {
        private NumberPicker numberPicker;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.personalized_activity);

            numberPicker = FindViewById<NumberPicker>(Resource.Id.numberPicker1);
            numberPicker.MinValue = 16; 
            numberPicker.MaxValue = 25; 
            numberPicker.Value = 18; 
            numberPicker.WrapSelectorWheel = false;
 
            numberPicker.ValueChanged += NumberPicker_ValueChanged;

        }
        
        private void NumberPicker_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            int selectedValue = e.NewVal;
            // Utilizați valoarea selectată cum doriți
        }
    }
}