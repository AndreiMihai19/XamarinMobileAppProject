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
using System.Data;

namespace MobileAppProject
{
    [Activity(Label = "LightActivity")]
    public class LightActivity : Activity
    {
        private SeekBar lightseekBar;
        private Button btnBack;
        private TextView tvDoorStatus;
        private TextView tvUser;
        private TextView tvLight;
        private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=homematicDB;User Id=root;Password=;charset=utf8");
        //private MySqlConnection connection = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            connection.Open();  

            SetContentView(Resource.Layout.light_activity);

            lightseekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);
            tvLight = FindViewById<TextView>(Resource.Id.tvLight);


            lightseekBar.ProgressChanged+= SeekBar_ProgressChanged;
            btnBack.Click += btnBack_Clicked;
            UpdateDoorStatusUser();
            lightseekBar.Progress = Parameters.getLight();

        }

        private void SeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            int progress = e.Progress;
            tvLight.Text = progress.ToString()+"%";
            Parameters.setLight(progress);

               // string query = "UPDATE Parameters SET light_intensity = @light";
                string query = "UPDATE parameters SET light_intensity = @light";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@light", Parameters.getLight());
                command.ExecuteNonQuery();

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