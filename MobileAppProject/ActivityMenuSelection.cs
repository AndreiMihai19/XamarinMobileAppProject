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
using Android.Graphics.Drawables;

namespace MobileAppProject
{
    [Activity(Label = "ActivityMenuSelection")]
    public class ActivityMenuSelection : Activity
    {
        private Button btnBack;
        private Button btnDefault;
        private Button btnPersonalized;
        private TextView tvDoorStatus;
        private TextView tvUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_menu);

            btnDefault = FindViewById<Button>(Resource.Id.XbtnDefault);
            btnPersonalized = FindViewById<Button>(Resource.Id.XbtnPersonalized);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);

            btnBack.Click += btnBack_Clicked;
            btnDefault.Click += btnDefault_Clicked;
            btnPersonalized.Click += btnPersonalized_Clicked;
            UpdateDoorStatusUser();

            GradientDrawable buttonBackground = new GradientDrawable();
            buttonBackground.SetColor(Android.Graphics.Color.White); // Setează culoarea fundalului butonului
            buttonBackground.SetStroke(2, Android.Graphics.Color.Black); // Setează grosimea și culoarea marginii
            buttonBackground.SetCornerRadius(5); 

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
            tvUser.Text=User.getUser();

        }

        private void btnPersonalized_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(PersonalizedActivity));
            StartActivity(nextActivity);
        }

        private void btnDefault_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(DefaultActivity));
            StartActivity(nextActivity);
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }
}