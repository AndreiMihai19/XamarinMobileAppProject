using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Provider;

namespace MobileAppProject
{
    [Activity(Label = "GetIMEI")]

    public class GetIMEI : Activity
    {
        private Button btnBack;
        private Button btnIMEI;
        private TextView tvIMEI;
        //Activity activity = this;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.get_imei);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            btnIMEI = FindViewById<Button>(Resource.Id.XbtnImei);
            tvIMEI = FindViewById<TextView>(Resource.Id.tvImei);


            btnBack.Click += btn_back_Clicked;
            btnIMEI.Click += btn_imei_Clicked;

            // Create your application here
        }

            
        
            
        private void btn_imei_Clicked(object sender, EventArgs e)
        {
            Vibrator vibrator = (Vibrator)GetSystemService(VibratorService);
            vibrator.Vibrate(100); // 100 milisecunde

            // Pentru o vibrație personalizată
            long[] pattern = { 0, 100}; // Durate în milisecunde între pauze și vibrații
            vibrator.Vibrate(VibrationEffect.CreateWaveform(pattern, -1)); // -1 pentru a se repeta o singură dată, sau un indice pentru a specifica un punct de început

            // Pentru a verifica dacă dispozitivul suportă vibrația
            bool hasVibrator = vibrator.HasVibrator;

            string androidID = Settings.Secure.GetString(ContentResolver, Settings.Secure.AndroidId);
            tvIMEI.Text = androidID;
        }

        private void btn_back_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
    }
}