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
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++"+etUsername.Text.ToString());
            btnInsert.Click += BtnInsert_Click;
          
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            string androidID = Settings.Secure.GetString(ContentResolver, Settings.Secure.AndroidId);
            User.setIMEI(androidID);
            if (etUsername.Text =="admin")
            {
                User.setUser(etUsername.Text);
                User.isAdmin= true;
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}