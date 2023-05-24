using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MobileAppProject.Classes;

namespace MobileAppProject
{
    [Activity(Label = "CreateUsersActivity")]
    public class CreateUsersActivity : Activity
    {
        private EditText etUsername;
        private EditText etPassword;
        private Button btnAdd;
        private Button btnBack;
        private TextView tvDoorStatus;
        private TextView tvUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.create_users_activity);

            etUsername = FindViewById<EditText>(Resource.Id.XcreateUsername);
            etPassword = FindViewById<EditText>(Resource.Id.XcreatePassword);
            btnAdd = FindViewById<Button>(Resource.Id.XbtnaddUser);
            btnBack = FindViewById<Button> (Resource.Id.XbtnBack);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);

            btnAdd.Click += BtnAddUser_Click;
            btnBack.Click += BtnBack_Click;
            UpdateDoorStatusUser();
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

        private async void BtnAddUser_Click(object sender, EventArgs e)
        {

            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);

            MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=mobile_app;User Id=root;Password=;charset=utf8");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO login(username,password) VALUES (@nume,@parola)", con);
                    cmd.Parameters.AddWithValue("@nume", etUsername.Text);
                    cmd.Parameters.AddWithValue("@parola", etPassword.Text);
                    cmd.ExecuteNonQuery();

                }

                alertDialog.SetMessage($"{etUsername.Text} was created!");
                alertDialog.SetNeutralButton("Ok", delegate
                {
                    alertDialog.Dispose();
                });
                alertDialog.Show();
            }
            catch (MySqlException ex)
            {
                alertDialog.SetMessage($"We have an error here!");
                alertDialog.SetNeutralButton("Ok", delegate
                {
                    alertDialog.Dispose();
                });

                alertDialog.Show();

            }
            finally
            {
                con.Clone();
            }

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {

            Intent nextActivity = new Intent(this, typeof(AdminActivity));
            StartActivity(nextActivity);
        }

    }
}