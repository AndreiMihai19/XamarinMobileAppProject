using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileAppProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileAppProject
{
    [Activity(Label = "AdminActivity")]
    public class AdminActivity : Activity
    {
        private Button btnCreateUsers;
        private Button btnShowUsers;
        private Button btnMenu;
        private Button btnBack;
        private TextView tvDoorStatus;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.admin_activity);

            // Create your application here

            btnCreateUsers = FindViewById<Button>(Resource.Id.XbtncreateUsers);
            btnMenu = FindViewById<Button>(Resource.Id.XbtnMenu);
            btnShowUsers = FindViewById<Button>(Resource.Id.XbtnshowUsers);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);

            btnCreateUsers.Click += BtnCreateUsers_Click;
            btnMenu.Click += BtnMenu_Click;
            btnShowUsers.Click += BtnShowUsers_Click;
            btnBack.Click += BtnBack_Click;
            UpdateDoorStatus();

        }

        private void BtnCreateUsers_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(CreateUsersActivity));
            StartActivity(nextActivity);

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            CheckAdmin.isadminActive = false;

            Intent nextActivity = new Intent(this, typeof(MainActivity));
            StartActivity(nextActivity);

        }
        private void BtnShowUsers_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ShowUsersActivity));
            StartActivity(nextActivity);
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }
}