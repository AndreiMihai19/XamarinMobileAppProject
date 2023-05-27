using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using MobileAppProject.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using Android.Provider;
using Android.Views;

namespace MobileAppProject
{
    [Activity( MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText etUsername;
        private EditText etPassword;
        private Button btnInsert;
        private Button btnIntra;
        private string currentDeviceID; 
        private MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=homematicDB;User Id=root;Password=;charset=utf8");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            currentDeviceID = Settings.Secure.GetString(ContentResolver, Settings.Secure.AndroidId);

            SetContentView(Resource.Layout.activity_main);

            etUsername = FindViewById<EditText>(Resource.Id.XetUsername);
            etPassword = FindViewById<EditText>(Resource.Id.XetPassword);
            btnInsert = FindViewById<Button>(Resource.Id.XbtnInsert);
            btnIntra = FindViewById<Button>(Resource.Id.XbtnIntra);
            btnInsert.Click += BtnInsert_Click;
            btnIntra.Click += BtnIntra_Click;
          
        }

        private void BtnIntra_Click(object sender, EventArgs e)
        {
            User.setUser(etUsername.Text);
          
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {

            var toast = Toast.MakeText(this, "Please wait...", ToastLength.Short);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();

            Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
               
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE email = @username AND passwrd = @password", con);
                    MySqlCommand cmdStatus = new MySqlCommand("SELECT is_admin FROM users WHERE email = @username AND passwrd = @password", con);
                    MySqlCommand cmdID = new MySqlCommand("SELECT device_id FROM users WHERE email=@username AND passwrd=@password", con);
                    cmd.Parameters.AddWithValue("@username", etUsername.Text);
                    cmd.Parameters.AddWithValue("@password", etPassword.Text);
                    cmdStatus.Parameters.AddWithValue("@username", etUsername.Text);
                    cmdStatus.Parameters.AddWithValue("@password", etPassword.Text);
                    cmdID.Parameters.AddWithValue("@username", etUsername.Text);
                    cmdID.Parameters.AddWithValue("@password", etPassword.Text);

                    object result = cmd.ExecuteScalar();
                    object status = cmdStatus.ExecuteScalar();
                    object did=cmdID.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int count = Convert.ToInt32(result);
                        bool admin = Convert.ToBoolean(status);
                        string device_id = Convert.ToString(did);

                        if (currentDeviceID == device_id)
                        {
                            if (count > 0)
                            {

                                if (admin == true)
                                {
                                    User.setUser(etUsername.Text);
                                    User.isAdmin = true;
                                    Intent nextActivity = new Intent(this, typeof(AdminActivity));
                                    StartActivity(nextActivity);
                                }
                                else
                                {
                                    CheckFirstLogin(device_id.Length);

                                    User.setUser(etUsername.Text);
                                    Intent nextActivity = new Intent(this, typeof(MenuActivity));
                                    StartActivity(nextActivity);
                                }
                            }
                            else
                            {
                                alertDialog.SetMessage($"{etUsername.Text} or password is not correct!");
                                alertDialog.SetNeutralButton("Ok", delegate
                                {
                                    alertDialog.Dispose();
                                });
                                alertDialog.Show();
                            }
                        }
                        else
                        {
                            alertDialog.SetMessage($"This account is not assigned to your device!");
                            alertDialog.SetNeutralButton("Ok", delegate
                            {
                                alertDialog.Dispose();
                            });

                            alertDialog.Show();
                        }
                    }
                }

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
                con.Close();
            }

        }

        private void CheckFirstLogin(int length)
        {
            if (length != 16)
            {
                User.setDeviceId(currentDeviceID);

                string query = "UPDATE users SET device_id = @deviceid WHERE email=@username AND passwrd=@password";
                MySqlCommand cmdsetID = new MySqlCommand(query, con);
                cmdsetID.Parameters.AddWithValue("@username", etUsername.Text);
                cmdsetID.Parameters.AddWithValue("@password", etPassword.Text);
                cmdsetID.Parameters.AddWithValue("@deviceid", User.getDeviceId());
                cmdsetID.ExecuteNonQuery();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}