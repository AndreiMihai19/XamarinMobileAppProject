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

namespace MobileAppProject
{
    [Activity( MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText etUsername;
        private EditText etPassword;
        private Button btnInsert;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            etUsername = FindViewById<EditText>(Resource.Id.XetUsername);
            etPassword = FindViewById<EditText>(Resource.Id.XetPassword);
            btnInsert = FindViewById<Button>(Resource.Id.XbtnInsert);
            btnInsert.Click += BtnInsert_Click;
          
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            string androidID = Settings.Secure.GetString(ContentResolver, Settings.Secure.AndroidId);
            User.setIMEI(androidID);


            Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);

            MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=mobile_app;User Id=root;Password=;charset=utf8");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                 //   MySqlCommand cmd = new MySqlCommand("SELECT username,password FROM login WHERE username=@username AND password=@password", con);
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM login WHERE username = @username AND password = @password", con);
                    cmd.Parameters.AddWithValue("@username", etUsername.Text);
                    cmd.Parameters.AddWithValue("@password", etPassword.Text);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int count = Convert.ToInt32(result);

                        if (count > 0)
                        {

                            if (etUsername.Text == "admin")
                            {
                                User.setUser(etUsername.Text);
                                User.isAdmin = true;
                                Intent nextActivity = new Intent(this, typeof(AdminActivity));
                                StartActivity(nextActivity);
                            }
                            else
                            {
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
                con.Clone();
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}