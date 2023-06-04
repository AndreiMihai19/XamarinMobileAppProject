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
        private TextView tvUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.admin_activity);

            btnCreateUsers = FindViewById<Button>(Resource.Id.XbtncreateUsers);
            btnMenu = FindViewById<Button>(Resource.Id.XbtnMenu);
            btnShowUsers = FindViewById<Button>(Resource.Id.XbtnshowUsers);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);

            btnCreateUsers.Click += BtnCreateUsers_Click;
            btnMenu.Click += BtnMenu_Click;
            btnShowUsers.Click += BtnShowUsers_Click;
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

        private void BtnCreateUsers_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(CreateUsersActivity));
            StartActivity(nextActivity);

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            User.isAdmin = false;

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