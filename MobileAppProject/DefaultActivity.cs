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
using Xamarin.KotlinX.Coroutines;
using MySql.Data.MySqlClient;
using static Android.Bluetooth.BluetoothClass;

namespace MobileAppProject
{
    [Activity(Label = "DefaultActivity")]
    public class DefaultActivity : Activity
    {
        private MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=homematicDB;User Id=root;Password=;charset=utf8");
        private Button btnBack;
        private Button btnJobActivity;
        private Button btnHolidayActivity;
        private Button btnWeekendActivity;
        private Button btnManualActivity;
        private TextView tvCurentActivityDoor;
        private TextView tvDoorStatus;
        private TextView tvUser;
        private int lastID;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.default_activity);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            btnHolidayActivity = FindViewById<Button>(Resource.Id.XbtnHoliday);
            btnJobActivity = FindViewById<Button>(Resource.Id.XbtnJob);
            btnWeekendActivity = FindViewById<Button>(Resource.Id.XbtnWeekend);
            btnManualActivity = FindViewById<Button>(Resource.Id.XbtnManual);
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

        private void btnManual_Clicked(object sender, EventArgs e)
        {
            Activities.setNume("Manual");
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }

        private void btnWeekend_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(80);
            Parameters.setTemperature(21);
            Parameters.setDoorStatus(1);
            Activities.setOptionCode(21, 80);
            tvCurentActivityDoor.Text = Activities.getOptionCode();
            Activities.setNume("Weekend");

            con.Open();

            string query = "UPDATE parameters SET temperature = @temperature,light_intensity = @light,opened_door = @door";
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@temperature", Parameters.getTemperature());
            command.Parameters.AddWithValue("@light", Parameters.getLight());
            command.Parameters.AddWithValue("@door", Parameters.getDoorStatus());
            command.ExecuteNonQuery();

            MySqlCommand cmdId = new MySqlCommand("SELECT action_id FROM actions ORDER BY action_id DESC LIMIT 1;", con);
            object lastId = cmdId.ExecuteScalar();
            lastID=Convert.ToInt32(lastId);
            Actions.setActionId(lastID+1);
            Actions.setActionType("Temperature");
            Actions.setValueAction(Parameters.getTemperature());
            Actions.setActionTime(DateTime.Now);


            MySqlCommand cmdTemperature = new MySqlCommand("INSERT INTO actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", con);
            cmdTemperature.Parameters.AddWithValue("@action_id",Actions.getActionId());
            cmdTemperature.Parameters.AddWithValue("@device_id",Actions.getDeviceId());
            cmdTemperature.Parameters.AddWithValue("@action_type",Actions.getActionType());
            cmdTemperature.Parameters.AddWithValue("@value_action",Actions.getValueAction());
            cmdTemperature.Parameters.AddWithValue("@date_time",Actions.getActionTime());
            cmdTemperature.ExecuteNonQuery();


            Actions.setActionId(lastID + 2);
            Actions.setActionType("Lumina");
            Actions.setValueAction(Parameters.getLight());
            Actions.setActionTime(DateTime.Now);


            MySqlCommand cmdLight = new MySqlCommand("INSERT INTO actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", con);
            cmdLight.Parameters.AddWithValue("@action_id", Actions.getActionId());
            cmdLight.Parameters.AddWithValue("@device_id", Actions.getDeviceId());
            cmdLight.Parameters.AddWithValue("@action_type", Actions.getActionType());
            cmdLight.Parameters.AddWithValue("@value_action", Actions.getValueAction());
            cmdLight.Parameters.AddWithValue("@date_time", Actions.getActionTime());
            cmdLight.ExecuteNonQuery();
            con.Close();
        }

        private void btnHoliday_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(75);
            Parameters.setTemperature(20);
            Parameters.setDoorStatus(1);
            Activities.setNume("Holiday");
            Activities.setOptionCode(20, 75);
            tvCurentActivityDoor.Text = Activities.getOptionCode();
            //Intent nextActivity = new Intent(this, typeof(MenuActivity));
            //StartActivity(nextActivity);


            con.Open();
            
            string query = "UPDATE parameters SET temperature = @temperature,light_intensity = @light,opened_door = @door";
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@temperature", Parameters.getTemperature());
            command.Parameters.AddWithValue("@light", Parameters.getLight());
            command.Parameters.AddWithValue("@door", Parameters.getDoorStatus());
            command.ExecuteNonQuery();

            MySqlCommand cmdId = new MySqlCommand("SELECT action_id FROM actions ORDER BY action_id DESC LIMIT 1;", con);
            object lastId = cmdId.ExecuteScalar();
            lastID = Convert.ToInt32(lastId);
            Actions.setActionId(lastID + 1);
            Actions.setActionType("Temperature");
            Actions.setValueAction(Parameters.getTemperature());
            Actions.setActionTime(DateTime.Now);


            MySqlCommand cmdTemperature = new MySqlCommand("INSERT INTO actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", con);
            cmdTemperature.Parameters.AddWithValue("@action_id", Actions.getActionId());
            cmdTemperature.Parameters.AddWithValue("@device_id", Actions.getDeviceId());
            cmdTemperature.Parameters.AddWithValue("@action_type", Actions.getActionType());
            cmdTemperature.Parameters.AddWithValue("@value_action", Actions.getValueAction());
            cmdTemperature.Parameters.AddWithValue("@date_time", Actions.getActionTime());
            cmdTemperature.ExecuteNonQuery();


            Actions.setActionId(lastID + 2);
            Actions.setActionType("Lumina");
            Actions.setValueAction(Parameters.getLight());
            Actions.setActionTime(DateTime.Now);


            MySqlCommand cmdLight = new MySqlCommand("INSERT INTO actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", con);
            cmdLight.Parameters.AddWithValue("@action_id", Actions.getActionId());
            cmdLight.Parameters.AddWithValue("@device_id", Actions.getDeviceId());
            cmdLight.Parameters.AddWithValue("@action_type", Actions.getActionType());
            cmdLight.Parameters.AddWithValue("@value_action", Actions.getValueAction());
            cmdLight.Parameters.AddWithValue("@date_time", Actions.getActionTime());
            cmdLight.ExecuteNonQuery();
            con.Close();

        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
        private void btnJob_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(40);
            Parameters.setTemperature(16);
            Parameters.setDoorStatus(0);
            Activities.setNume("Job");
            Activities.setOptionCode(16, 40);
            tvCurentActivityDoor.Text = Activities.getOptionCode();

            //Intent nextActivity = new Intent(this, typeof(MenuActivity));
            //StartActivity(nextActivity);



            con.Open();
            string query = "UPDATE parameters SET temperature = @temperature,light_intensity = @light,opened_door = @door";
            MySqlCommand command = new MySqlCommand(query, con);
            command.Parameters.AddWithValue("@temperature", Parameters.getTemperature());
            command.Parameters.AddWithValue("@light", Parameters.getLight());
            command.Parameters.AddWithValue("@door", Parameters.getDoorStatus());
            command.ExecuteNonQuery();


            MySqlCommand cmdId = new MySqlCommand("SELECT action_id FROM actions ORDER BY action_id DESC LIMIT 1;", con);
            object lastId = cmdId.ExecuteScalar();
            lastID = Convert.ToInt32(lastId);
            Actions.setActionId(lastID + 1);
            Actions.setActionType("Temperature");
            Actions.setValueAction(Parameters.getTemperature());
            Actions.setActionTime(DateTime.Now);


            MySqlCommand cmdTemperature = new MySqlCommand("INSERT INTO actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", con);
            cmdTemperature.Parameters.AddWithValue("@action_id", Actions.getActionId());
            cmdTemperature.Parameters.AddWithValue("@device_id", Actions.getDeviceId());
            cmdTemperature.Parameters.AddWithValue("@action_type", Actions.getActionType());
            cmdTemperature.Parameters.AddWithValue("@value_action", Actions.getValueAction());
            cmdTemperature.Parameters.AddWithValue("@date_time", Actions.getActionTime());
            cmdTemperature.ExecuteNonQuery();


            Actions.setActionId(lastID + 2);
            Actions.setActionType("Lumina");
            Actions.setValueAction(Parameters.getLight());
            Actions.setActionTime(DateTime.Now);


            MySqlCommand cmdLight = new MySqlCommand("INSERT INTO actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", con);
            cmdLight.Parameters.AddWithValue("@action_id", Actions.getActionId());
            cmdLight.Parameters.AddWithValue("@device_id", Actions.getDeviceId());
            cmdLight.Parameters.AddWithValue("@action_type", Actions.getActionType());
            cmdLight.Parameters.AddWithValue("@value_action", Actions.getValueAction());
            cmdLight.Parameters.AddWithValue("@date_time", Actions.getActionTime());
            cmdLight.ExecuteNonQuery();
            con.Close();
        }
    }
}