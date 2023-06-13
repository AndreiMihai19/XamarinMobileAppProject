using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util.Logging;
using MobileAppProject.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MobileAppProject
{
    [Activity(Label = "DoorActivity")]
    public class DoorActivity : Activity
    {
        private Button btnBack;
        private Button btnOpen;
        private Button btnClose;
        private TextView tvDoorStatus;
        private TextView tvUser;
        //private MySqlConnection connection = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            connection.Open();

            SetContentView(Resource.Layout.door_activity);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            btnOpen = FindViewById<Button>(Resource.Id.XbtnOpenDoor);
            btnClose = FindViewById<Button>(Resource.Id.XbtnCloseDoor);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);

            btnBack.Click += btnBack_Clicked;
            btnOpen.Click += btnOpen_Clicked;
            btnClose.Click += btnClose_Clicked;

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

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Parameters.setDoorStatus(0);
            Parameters.setCurrentPreset("manual");
            UpdateDoorStatusUser() ;

            string query = "UPDATE Parameters SET opened_door = @door, current_preset=@current_preset";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@door", Parameters.getDoorStatus());
            command.Parameters.AddWithValue("@current_preset", Parameters.getCurrentPreset());
            command.ExecuteNonQuery();
        }

        private void btnOpen_Clicked(object sender, EventArgs e)
        {
            Parameters.setDoorStatus(1);
            Parameters.setCurrentPreset("manual");
            UpdateDoorStatusUser() ;

            string query = "UPDATE Parameters SET opened_door = @door, current_preset=@current_preset";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@door", Parameters.getDoorStatus());
            command.Parameters.AddWithValue("@current_preset", Parameters.getCurrentPreset());
            command.ExecuteNonQuery();
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
