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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.door_activity);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            btnOpen = FindViewById<Button>(Resource.Id.XbtnOpenDoor);
            btnClose = FindViewById<Button>(Resource.Id.XbtnCloseDoor);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);

            btnBack.Click += btnBack_Clicked;
            btnOpen.Click += btnOpen_Clicked;
            btnClose.Click += btnClose_Clicked;

            UpdateDoorStatus();
           
            // Create your application here
        }
       
        private void UpdateDoorStatus()
        {
            if (Parameters.getDoorStatus() == 1)
            {
                tvDoorStatus.Text = "Close";
            }
            else
            {
                tvDoorStatus.Text = "Open";
            }
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Parameters.setDoorStatus(1);
            UpdateDoorStatus() ;
        }

        private void btnOpen_Clicked(object sender, EventArgs e)
        {
            Parameters.setDoorStatus(0);
            UpdateDoorStatus() ;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }


        
    }


}
