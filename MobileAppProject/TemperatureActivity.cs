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
    [Activity(Label = "Activity1")]
    public class TemperatureActivity : Activity
    {
        private Button btnBack;
        private Button btnPlus;
        private Button btnMinus;
        private TextView tvDoorStatus;
        private TextView tvUser;
        private TextView tvTemperature;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.temperature_activity);

            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            btnMinus = FindViewById<Button>(Resource.Id.XbtnMinus);
            btnPlus = FindViewById<Button>(Resource.Id.XbtnPlus);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);
            tvTemperature = FindViewById<TextView>(Resource.Id.temperatureValue);



            btnBack.Click += btnBack_Clicked;
            btnMinus.Click += btnMinus_Clicked;
            btnPlus.Click += btnPlus_Clicked;
            UpdateDoorStatusUser();
            updateTemperature();

        }

        private void btnPlus_Clicked(object sender, EventArgs e)
        {
            if (Parameters.getTemperature() < 27)
            {
                Parameters.setTemperature(Parameters.getTemperature() + 1);
                updateTemperature();
            }
        }

        private void btnMinus_Clicked(object sender, EventArgs e)
        {
            if (Parameters.getTemperature() > 15)
            {
                Parameters.setTemperature(Parameters.getTemperature() - 1);
                updateTemperature();
            }
        }

        private void updateTemperature()
        {
            tvTemperature.Text = Parameters.getTemperature().ToString();
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

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }


}