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

namespace MobileAppProject
{
    [Activity(Label = "DoorActivity")]
    public class DoorActivity : Activity
    {
        private Button btnBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.door_activity);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);

            btnBack.Click += btnBack_Clicked;

            // Create your application here
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }
}