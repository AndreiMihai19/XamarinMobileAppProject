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
using Android.Provider;
using System.Linq;
using System.Text;

namespace MobileAppProject
{
    [Activity(Label = "PersonalizedActivity")]
    public class PersonalizedActivity : Activity
    {
        private NumberPicker nrpTemperature;
        private TextView txtName;
        private SeekBar skbLight;
        private Button btnAddActivity;
        private Button btnBack;
        private int presetID;
        private int selectedValue;
        private int progressValue;
        private MySqlConnection connection = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
       // private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            connection.Open();

            SetContentView(Resource.Layout.personalized_activity);

            txtName = FindViewById<TextView>(Resource.Id.txtPersonalizedActivity);
            skbLight = FindViewById<SeekBar>(Resource.Id.seekBarPersonalizedActivity);
            nrpTemperature = FindViewById<NumberPicker>(Resource.Id.numberPicker1);
            btnAddActivity = FindViewById<Button>(Resource.Id.XbtnaddActivity);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);

            nrpTemperature = FindViewById<NumberPicker>(Resource.Id.numberPicker1);
            nrpTemperature.MinValue = 10;
            nrpTemperature.MaxValue = 35;
            nrpTemperature.Value = 20; 
            nrpTemperature.WrapSelectorWheel = false;

            skbLight.ProgressChanged += SeekBar_ProgressChanged;
            nrpTemperature.ValueChanged += NumberPicker_ValueChanged;
            btnAddActivity.Click += AddActivity_Clicked;
            btnBack.Click += btnBack_Clicked;
        }

        private void SeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            progressValue = e.Progress;
        }

        private void NumberPicker_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            selectedValue = e.NewVal;
            
        }

        private void AddActivity_Clicked(object sender, EventArgs e) 
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);

            MySqlCommand cmdId = new MySqlCommand("SELECT preset_id FROM Presets ORDER BY preset_id DESC LIMIT 1;", connection);
            object lastId = cmdId.ExecuteScalar();
            presetID = Convert.ToInt32(lastId);
            
            Activities.setPresetId(presetID + 1);
            Activities.setPresetName(txtName.Text);
            Activities.setDeviceId(Settings.Secure.GetString(ContentResolver, Settings.Secure.AndroidId));
            Activities.setOptionCode(selectedValue,progressValue);
            Activities.AddPreset(txtName.Text);

            MySqlCommand cmd = new MySqlCommand("INSERT INTO Presets(preset_id,preset_name,device_id,option_code) VALUES (@preset_id,@preset_name,@device_id,@option_code)", connection);
            cmd.Parameters.AddWithValue("@preset_id", Activities.getPresetId());
            cmd.Parameters.AddWithValue("@preset_name", Activities.getPresetName());
            cmd.Parameters.AddWithValue("@device_id", Activities.getDeviceId());
            cmd.Parameters.AddWithValue("@option_code", Activities.getOptionCode());
            cmd.ExecuteNonQuery();


            alertDialog.SetMessage($"Activity {txtName.Text} was created!");
            alertDialog.SetNeutralButton("Ok", delegate
            {
                alertDialog.Dispose();
            });
            alertDialog.Show();

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