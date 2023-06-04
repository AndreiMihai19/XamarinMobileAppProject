using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileAppProject.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=homematicDB;User Id=root;Password=;charset=utf8");
        //private MySqlConnection connection = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

          //  string query = "SELECT * FROM Parameters";
            string query = "SELECT * FROM parameters";

            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Parameters.setTemperature(reader.GetFloat(1));
                Parameters.setLight(reader.GetInt32(2));
                Parameters.setDoorStatus(reader.GetInt32(3));
            }

            reader.Close();

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
            if (Parameters.getDoorStatus() == 0)
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


            if (User.isAdmin==true)
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

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }


    }

}