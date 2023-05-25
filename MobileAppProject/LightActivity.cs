using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileAppProject.Classes;

namespace MobileAppProject
{
    [Activity(Label = "LightActivity")]
    public class LightActivity : Activity
    {
        private SeekBar lightseekBar;
        private Button btnBack;
        private TextView tvDoorStatus;
        private TextView tvUser;
        private TextView tvLight;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.light_activity);

            lightseekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);
            tvLight = FindViewById<TextView>(Resource.Id.tvLight);


            lightseekBar.ProgressChanged+= SeekBar_ProgressChanged;
            btnBack.Click += btnBack_Clicked;
           UpdateDoorStatusUser();
            lightseekBar.Progress = Parameters.getLight();

        }

        private void SeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            int progress = e.Progress;
            tvLight.Text = progress.ToString()+"%";
            Parameters.setLight(progress);
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

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }
}