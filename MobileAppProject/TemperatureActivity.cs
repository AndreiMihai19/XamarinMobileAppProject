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
  
        private MySqlConnection connection = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
      //  private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
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
            if (Parameters.getTemperature() < 35)
            {
                Parameters.setTemperature((float)(Parameters.getTemperature() + 0.5));
                updateTemperature();
            }

            Parameters.setCurrentPreset("manual");

            string query = "UPDATE Parameters SET temperature = @temperature, current_preset=@current_preset";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@temperature", Parameters.getTemperature());
            command.Parameters.AddWithValue("@current_preset", Parameters.getCurrentPreset());
            command.ExecuteNonQuery();

        }

        private void btnMinus_Clicked(object sender, EventArgs e)
        {
            if (Parameters.getTemperature() > 10)
            {
                Parameters.setTemperature((float)(Parameters.getTemperature() - 0.5));
                updateTemperature();
            }

            Parameters.setCurrentPreset("manual");

            string query = "UPDATE Parameters SET temperature = @temperature, current_preset=@current_preset";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@temperature", Parameters.getTemperature());
            command.Parameters.AddWithValue("@current_preset", Parameters.getCurrentPreset());
            command.ExecuteNonQuery();
        }

        private void updateTemperature()
        {
            tvTemperature.Text = Parameters.getTemperature().ToString();
        }


        private void UpdateDoorStatusUser()
        {
            if (Parameters.getDoorStatus() == 0)
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
            int lastID;
          //  MySqlCommand cmdId = new MySqlCommand("SELECT action_id FROM actions ORDER BY action_id DESC LIMIT 1;", connection);
            MySqlCommand cmdId = new MySqlCommand("SELECT action_id FROM Actions ORDER BY action_id DESC LIMIT 1;", connection);
            object lastId = cmdId.ExecuteScalar();
            lastID = Convert.ToInt32(lastId);
            Actions.setActionId(lastID + 1);
            Actions.setActionType("temperature");
            Actions.setValueAction(Parameters.getTemperature());
            Actions.setActionTime(DateTime.Now);


          //  MySqlCommand cmdTemperature = new MySqlCommand("INSERT INTO actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", connection);
            MySqlCommand cmdTemperature = new MySqlCommand("INSERT INTO Actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", connection);
            cmdTemperature.Parameters.AddWithValue("@action_id", Actions.getActionId());
            cmdTemperature.Parameters.AddWithValue("@device_id", Actions.getDeviceId());
            cmdTemperature.Parameters.AddWithValue("@action_type", Actions.getActionType());
            cmdTemperature.Parameters.AddWithValue("@value_action", Actions.getValueAction());
            cmdTemperature.Parameters.AddWithValue("@date_time", Actions.getActionTime());
            cmdTemperature.ExecuteNonQuery();

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