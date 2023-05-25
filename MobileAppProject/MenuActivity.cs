using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileAppProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppProject
{
    [Activity(Label = "Activity1")]
    public class MenuActivity : Activity
    {
        private Button btnLight;
        private Button btnTemperature;
        private Button btnInsert;
        private Button btnDoor;
        private Button btnActivity;
        private TextView tvActivityName;
        private TextView tvDoorStatus;
        private TextView tvUser;
        private TextView tvLight;
        private TextView tvTemperature;
        private TextView tvDoor2;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.menu_activity);

            btnLight = FindViewById<Button>(Resource.Id.XbtnLight);
            btnTemperature = FindViewById<Button>(Resource.Id.XbtnTemperature);
            btnInsert = FindViewById<Button>(Resource.Id.XbtnBack);
            btnDoor = FindViewById<Button>(Resource.Id.XbtnDoor);
            btnActivity = FindViewById<Button>(Resource.Id.XbtnActivity);
            tvActivityName = FindViewById<TextView>(Resource.Id.textViewActivityName);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.textViewDoorStatus);
            tvUser = FindViewById<TextView>(Resource.Id.username);
            tvLight = FindViewById<TextView>(Resource.Id.tvLight);
            tvTemperature = FindViewById<TextView>(Resource.Id.tvTemperature);
            tvDoor2 = FindViewById<TextView>(Resource.Id.tvDoor);


            btnLight.Click += BtnLight_Clicked;
            btnInsert.Click += BtnBack_Click;
            btnTemperature.Click += BtnTemperature_Clicked;
            btnDoor.Click += BtnDoor_Clicked;
            btnActivity.Click += BtnActivity_Clicked;

            activityCheck();
            UpdateDoorStatusUser();

        }

        private void UpdateDoorStatusUser()
        {
            if (Parameters.getDoorStatus() == 1)
            {
                tvDoorStatus.Text = "Close";
                tvDoor2.Text = "Close";
            }
            else
            {
                tvDoorStatus.Text = "Open";
                tvDoor2.Text = "Open";
            }
            tvUser.Text = User.getUser().ToString();
            tvLight.Text = Parameters.getLight().ToString()+"%";
            tvTemperature.Text = Parameters.getTemperature().ToString()+ "°C";


        }

        private void activityCheck()
        {
            tvActivityName.Text = Activities.getNume();
        }

        private void BtnActivity_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityMenuSelection));
            StartActivity(nextActivity);
        }

        private void BtnDoor_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(DoorActivity));
            StartActivity(nextActivity);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {


            if (CheckAdmin.isadminActive==true)
            {
                Intent nextActivity = new Intent(this, typeof(AdminActivity));
                StartActivity(nextActivity);
            }
            else
            {
                Intent nextActivity = new Intent(this, typeof(MainActivity));
                StartActivity(nextActivity);
            }

        }

        private void BtnLight_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(LightActivity));
            StartActivity(nextActivity);
        }

        private void BtnTemperature_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(TemperatureActivity));
            StartActivity(nextActivity);
        }


    }

}