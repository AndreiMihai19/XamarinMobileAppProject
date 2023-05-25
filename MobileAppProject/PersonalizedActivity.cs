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

namespace MobileAppProject
{
    [Activity(Label = "PersonalizedActivity")]
    public class PersonalizedActivity : Activity
    {
        private Button btnShowTimePicker;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.personalized_activity);

            btnShowTimePicker = FindViewById<Button>(Resource.Id.btnShowTimePicker);
            btnShowTimePicker.Click += BtnShowTimePicker_Click;

            NumberPicker numberPicker = FindViewById<NumberPicker>(Resource.Id.numberPicker1);
            numberPicker.MinValue = 16; 
            numberPicker.MaxValue = 25; 
            numberPicker.Value = 18; 
            numberPicker.WrapSelectorWheel = false;
 
            numberPicker.ValueChanged += NumberPicker_ValueChanged;

        }
        private void BtnShowTimePicker_Click(object sender, EventArgs e)
        {
            // Creați un dialog pentru afișarea TimePicker-ului
            TimePickerDialog timePickerDialog = new TimePickerDialog(this, OnTimeSet, DateTime.Now.Hour, DateTime.Now.Minute, true);
            timePickerDialog.Show();
        }

        private void OnTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            int selectedHour = e.HourOfDay;
            int selectedMinute = e.Minute;

            string formattedTime = string.Format("{0:D2}:{1:D2}", selectedHour, selectedMinute);

            btnShowTimePicker.Text = formattedTime;
        }

        private void NumberPicker_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            int selectedValue = e.NewVal;
            // Utilizați valoarea selectată cum doriți
        }
    }
}