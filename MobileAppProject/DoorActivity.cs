using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util.Logging;
using MobileAppProject.Classes;
using System;
using System.Collections.Generic;
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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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
           
            // Create your application here
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

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Parameters.setDoorStatus(1);
            UpdateDoorStatusUser() ;
        }

        private void btnOpen_Clicked(object sender, EventArgs e)
        {
            Parameters.setDoorStatus(0);
            UpdateDoorStatusUser() ;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }


        
    }


}
