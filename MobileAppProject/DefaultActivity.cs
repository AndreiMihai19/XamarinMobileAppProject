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
using MobileAppProject.Classes;


namespace MobileAppProject
{
    [Activity(Label = "DefaultActivity")]
    public class DefaultActivity : Activity
    {
        private Button btnBack;
        private Button btnJobActivity;
        private Button btnHolidayActivity;
        private Button btnWeekendActivity;
        private Button btnManualActivity;
        private TextView tvCurentActivityLight;
        private TextView tvCurentActivityTemperature;
        private TextView tvCurentActivityDoor;
        private TextView tvDoorStatus;
        private TextView tvUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.default_activity);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            btnHolidayActivity = FindViewById<Button>(Resource.Id.XbtnHoliday);
            btnJobActivity = FindViewById<Button>(Resource.Id.XbtnJob);
            btnWeekendActivity = FindViewById<Button>(Resource.Id.XbtnWeekend);
            btnManualActivity = FindViewById<Button>(Resource.Id.XbtnManual);
            tvCurentActivityLight = FindViewById<TextView>(Resource.Id.textCurentActivityLight);
            tvCurentActivityTemperature = FindViewById<TextView>(Resource.Id.textCurentActivityTemperature);
            tvCurentActivityDoor = FindViewById<TextView>(Resource.Id.textCurentActivityDoor);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);

            btnBack.Click += btnBack_Clicked;
            btnJobActivity.Click += btnJob_Clicked;
            btnHolidayActivity.Click += btnHoliday_Clicked;
            btnWeekendActivity.Click += btnWeekend_Clicked;
            btnManualActivity.Click += btnManual_Clicked;
            // Create your application here
        
        UpdateDoorStatusUser();
        }

        private void UpdateDoorStatusUser()
        {
            if (Parameters.getDoorStatus() == 1)
            {
                tvDoorStatus.Text = "Close";
            }
            else
            {
                tvDoorStatus.Text = "Open";
            }
            tvUser.Text = User.getUser();
        }

        private void btnManual_Clicked(object sender, EventArgs e)
        {
            Activities.setNume("Manual");
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }

        private void btnWeekend_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(80);
            tvCurentActivityLight.Text = Parameters.getLight().ToString();
            Parameters.setTemperature(21);
            tvCurentActivityTemperature.Text = Parameters.getTemperature().ToString();
            Parameters.setDoorStatus(1);
            tvCurentActivityDoor.Text = Parameters.getDoorStatus().ToString();
            Activities.setNume("Weekend");
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }

        private void btnHoliday_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(75);
            tvCurentActivityLight.Text = Parameters.getLight().ToString();
            Parameters.setTemperature(20);
            tvCurentActivityTemperature.Text = Parameters.getTemperature().ToString();
            Parameters.setDoorStatus(1);
            tvCurentActivityDoor.Text = Parameters.getDoorStatus().ToString();
            Activities.setNume("Holiday");
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
        private void btnJob_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(40);
            tvCurentActivityLight.Text = Parameters.getLight().ToString();
            Parameters.setTemperature(16);
            tvCurentActivityTemperature.Text = Parameters.getTemperature().ToString();
            Parameters.setDoorStatus(0);
            tvCurentActivityDoor.Text = Parameters.getDoorStatus().ToString();
            Activities.setNume("Job");
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }
}