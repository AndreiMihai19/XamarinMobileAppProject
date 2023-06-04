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
        private EditText etDeviceId;
        private EditText etUsername;
        private EditText etPassword;
        private EditText etFirstName;
        private EditText etLastName;
        private EditText etCNP;
        private Button btnAdd;
        private Button btnBack;
        private TextView tvDoorStatus;
        private TextView tvUser;
        private MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=homematicDB;User Id=root;Password=;charset=utf8");
       // private MySqlConnection con = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.create_users_activity);

            etUsername = FindViewById<EditText>(Resource.Id.XcreateUsername);
            etPassword = FindViewById<EditText>(Resource.Id.XcreatePassword);
            etFirstName = FindViewById<EditText>(Resource.Id.XcreateFirstName); 
            etLastName = FindViewById<EditText>(Resource.Id.XcreateLastName); 
            etCNP = FindViewById<EditText>(Resource.Id.XcreateCNP); 
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

        private async void BtnAddUser_Click(object sender, EventArgs e)
        {

            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                  //  MySqlCommand cmd = new MySqlCommand("INSERT INTO Users(device_id,passwrd,email,first_name,last_name,is_admin,cnp) VALUES (@device_id,@password,@email,@first_name,@last_name,@is_admin,@cnp)", con);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO users(device_id,passwrd,email,first_name,last_name,is_admin,cnp) VALUES (@device_id,@password,@email,@first_name,@last_name,@is_admin,@cnp)", con);
              
                    Random random = new Random();

                    string deviceID = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 15)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                    cmd.Parameters.AddWithValue("@device_id", deviceID);

                    HashConfiguration hashConfig = new HashConfiguration();
                    var hashPassword = hashConfig.HashPassword(etPassword.Text);
                 
                    cmd.Parameters.AddWithValue("@password", hashPassword);
                    cmd.Parameters.AddWithValue("@email", etUsername.Text);
                    cmd.Parameters.AddWithValue("@first_name", etFirstName.Text);
                    cmd.Parameters.AddWithValue("@last_name", etLastName.Text);
                    cmd.Parameters.AddWithValue("@is_admin", 0);
                    cmd.Parameters.AddWithValue("@cnp", etCNP.Text);
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
                //alertDialog.SetMessage($"We have an error here!");
                alertDialog.SetMessage(ex.ToString());
                alertDialog.SetNeutralButton("Ok", delegate
                {
                    alertDialog.Dispose();
                });

                alertDialog.Show();

            }
            finally
            {
                con.Close();
            }

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {

            Intent nextActivity = new Intent(this, typeof(AdminActivity));
            StartActivity(nextActivity);
        }

    }
}