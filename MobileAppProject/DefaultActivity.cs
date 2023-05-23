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
//using MobileAppProject.Classes.Parameters;


namespace MobileAppProject
{
    [Activity(Label = "DefaultActivity")]
    public class DefaultActivity : Activity
    {
        private Button btnBack;
        private Button btnJobActivity;
        private Button btnHolidayActivity;
        private Button btnWeekendActivity;
        private TextView tvCurentActivity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.default_activity);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            btnHolidayActivity = FindViewById<Button>(Resource.Id.XbtnHoliday);
            btnJobActivity = FindViewById<Button>(Resource.Id.XbtnJob);
            btnWeekendActivity = FindViewById<Button>(Resource.Id.XbtnWeekend);
            tvCurentActivity = FindViewById<TextView>(Resource.Id.textCurentActivity);

            btnBack.Click += btnBack_Clicked;
            btnJobActivity.Click += btnJob_Clicked;
            btnHolidayActivity.Click += btnHoliday_Clicked;
            btnWeekendActivity.Click += btnWeekend_Clicked;
            // Create your application here
        }

        private void btnWeekend_Clicked(object sender, EventArgs e)
        {
           // tvCurentActivity.Text = Parameters.getLight().ToString();
        }

        private void btnHoliday_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
        private void btnJob_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }
}