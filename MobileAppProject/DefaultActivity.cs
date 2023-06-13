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
using System.Data;

namespace MobileAppProject
{
    [Activity(Label = "DefaultActivity")]
    public class DefaultActivity : Activity
    {
     
        private Button btnBack;
        private Button btnSelectPreset;
        private TextView tvDoorStatus;
        private TextView tvUser;
        private Spinner presetSpinner;
        private int lastID;
        //private MySqlConnection connection = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);

            connection.Open();

            SetContentView(Resource.Layout.default_activity);

            btnSelectPreset = FindViewById<Button>(Resource.Id.XbtnSelectPreset);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);
            presetSpinner = FindViewById<Spinner>(Resource.Id.spinnerPresets);

            btnSelectPreset.Click += btnSelectPreset_Clicked;
            btnBack.Click += btnBack_Clicked;
     
            UpdateDoorStatusUser();

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Activities.GetPresetList());
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            presetSpinner.Adapter = adapter;


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

        private void btnSelectPreset_Clicked(object sender, EventArgs e)
        {
            string selectedPreset = presetSpinner.SelectedItem.ToString();

            MySqlCommand cmdPreset = new MySqlCommand("SELECT * FROM Presets WHERE preset_name=@preset_name", connection);
            cmdPreset.Parameters.AddWithValue("@preset_name", selectedPreset);

            MySqlDataReader reader = cmdPreset.ExecuteReader();
            reader.Read();

            int presetID = reader.GetInt32("preset_id");
            string presetName = reader.GetString("preset_name");
            string deviceID = reader.GetString("device_id");
            string optionCode = reader.GetString("option_code");

            reader.Close();

            string[] values = optionCode.Split(".");

            float temperature = Convert.ToSingle((string)values[0]);
            int light = Convert.ToInt32((string)values[1]);
           

            Parameters.setLight(light);
            Parameters.setTemperature(temperature);
            Parameters.setDoorStatus(0);
            Parameters.setCurrentPreset(selectedPreset);

            MySqlCommand command = new MySqlCommand("UPDATE Parameters SET temperature = @temperature,light_intensity = @light,opened_door = @door, current_preset= @current_preset", connection);
            command.Parameters.AddWithValue("@temperature", Parameters.getTemperature());
            command.Parameters.AddWithValue("@light", Parameters.getLight());
            command.Parameters.AddWithValue("@door", Parameters.getDoorStatus());
            command.Parameters.AddWithValue("@current_preset", Parameters.getCurrentPreset());
            command.ExecuteNonQuery();

            MySqlCommand cmdId = new MySqlCommand("SELECT action_id FROM Actions ORDER BY action_id DESC LIMIT 1;", connection);
            object lastId = cmdId.ExecuteScalar();
            lastID = Convert.ToInt32(lastId);
            Actions.setActionId(lastID + 1);
            Actions.setActionType("temperature");
            Actions.setValueAction(Parameters.getTemperature());
            Actions.setActionTime(DateTime.Now);

            MySqlCommand cmdTemperature = new MySqlCommand("INSERT INTO Actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", connection);
            cmdTemperature.Parameters.AddWithValue("@action_id", Actions.getActionId());
            cmdTemperature.Parameters.AddWithValue("@device_id", Actions.getDeviceId());
            cmdTemperature.Parameters.AddWithValue("@action_type", Actions.getActionType());
            cmdTemperature.Parameters.AddWithValue("@value_action", Actions.getValueAction());
            cmdTemperature.Parameters.AddWithValue("@date_time", Actions.getActionTime());
            cmdTemperature.ExecuteNonQuery();

            Actions.setActionId(lastID + 2);
            Actions.setActionType("light");
            Actions.setValueAction(Parameters.getLight());
            Actions.setActionTime(DateTime.Now);


            MySqlCommand cmdLight = new MySqlCommand("INSERT INTO Actions(action_id,device_id,action_type,value_action,date_time) VALUES (@action_id,@device_id,@action_type,@value_action,@date_time)", connection);
            cmdLight.Parameters.AddWithValue("@action_id", Actions.getActionId());
            cmdLight.Parameters.AddWithValue("@device_id", Actions.getDeviceId());
            cmdLight.Parameters.AddWithValue("@action_type", Actions.getActionType());
            cmdLight.Parameters.AddWithValue("@value_action", Actions.getValueAction());
            cmdLight.Parameters.AddWithValue("@date_time", Actions.getActionTime());
            cmdLight.ExecuteNonQuery();

        }



        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ActivityMenuSelection));
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