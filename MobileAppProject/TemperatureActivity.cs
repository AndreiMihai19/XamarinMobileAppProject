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
using MySql.Data.MySqlClient;
using static Android.Renderscripts.Sampler;
using System.Data;

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
        private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=homematicDB;User Id=root;Password=;charset=utf8");


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            connection.Open();

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

            string query = "UPDATE parameters SET temperature = @temperature";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@temperature", Parameters.getTemperature());
            command.ExecuteNonQuery();

        }

        private void btnMinus_Clicked(object sender, EventArgs e)
        {
            if (Parameters.getTemperature() > 15)
            {
                Parameters.setTemperature(Parameters.getTemperature() - 1);
                updateTemperature();
            }

            string query = "UPDATE parameters SET temperature = @temperature";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@temperature", Parameters.getTemperature());
            command.ExecuteNonQuery();
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