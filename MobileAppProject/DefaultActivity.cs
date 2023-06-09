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
using Xamarin.KotlinX.Coroutines;

namespace MobileAppProject
{
    [Activity(Label = "DefaultActivity")]
    public class DefaultActivity : Activity
    {
        private Button btnBack;
        private Button btnJobActivity;
        private Button btnHolidayActivity;
        private Button btnWeekendActivity;
        private Button btnManualActivity;
        private TextView tvCurentActivityDoor;
        private TextView tvDoorStatus;
        private TextView tvUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.default_activity);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            btnHolidayActivity = FindViewById<Button>(Resource.Id.XbtnHoliday);
            btnJobActivity = FindViewById<Button>(Resource.Id.XbtnJob);
            btnWeekendActivity = FindViewById<Button>(Resource.Id.XbtnWeekend);
            btnManualActivity = FindViewById<Button>(Resource.Id.XbtnManual);
            tvCurentActivityDoor = FindViewById<TextView>(Resource.Id.textCurentActivityDoor);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);

            btnBack.Click += btnBack_Clicked;
            btnJobActivity.Click += btnJob_Clicked;
            btnHolidayActivity.Click += btnHoliday_Clicked;
            btnWeekendActivity.Click += btnWeekend_Clicked;
            btnManualActivity.Click += btnManual_Clicked;
            // Create your application here
        
        UpdateDoorStatusUser();
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

        private void btnManual_Clicked(object sender, EventArgs e)
        {
            Activities.setNume("Manual");
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }

        private void btnWeekend_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(80);
            Parameters.setTemperature(21);
            Parameters.setDoorStatus(1);
            Activities.setOptionCode(21, 80);
            tvCurentActivityDoor.Text = Activities.getOptionCode();
            Activities.setNume("Weekend");
            //Intent nextActivity = new Intent(this, typeof(MenuActivity));
            //StartActivity(nextActivity);

            //Device_id l-am setat deja in pagina de login!!!
            //TODO:  extragere din baza de date a coloanei action_id si incrementarea ei apoi ->Actions.setActionId(id-ul incrementat);
            Actions.setActionType("Temperatura");
            Actions.setValueAction(21);
            Actions.setActionTime(DateTime.Now);
            //TODO:  introducere date din Action in tabela "Actions" in baza de date


            //TODO:  extragere din baza de date a coloanei action_id si incrementarea ei apoi ->Actions.setActionId(id-ul incrementat);
            Actions.setActionType("Lumina");
            Actions.setValueAction(80);
            Actions.setActionTime(DateTime.Now);
            //TODO:  introducere date din Action in tabela "Actions" in baza de date
        }

        private void btnHoliday_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(75);
            Parameters.setTemperature(20);
            Parameters.setDoorStatus(1);
            Activities.setNume("Holiday");
            Activities.setOptionCode(20, 75);
            tvCurentActivityDoor.Text = Activities.getOptionCode();
            //Intent nextActivity = new Intent(this, typeof(MenuActivity));
            //StartActivity(nextActivity);

            //Device_id l-am setat deja in pagina de login!!!
            //TODO:  extragere din baza de date a coloanei action_id si incrementarea ei apoi ->Actions.setActionId(id-ul incrementat);
            Actions.setActionType("Temperatura");
            Actions.setValueAction(21);
            Actions.setActionTime(DateTime.Now);
            //TODO:  introducere date din Action in tabela "Actions" in baza de date

            //TODO:  extragere din baza de date a coloanei action_id si incrementarea ei apoi ->Actions.setActionId(id-ul incrementat);
            Actions.setActionType("Lumina");
            Actions.setValueAction(80);
            Actions.setActionTime(DateTime.Now);
            //TODO:  introducere date din Action in tabela "Actions" in baza de date

        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }
        private void btnJob_Clicked(object sender, EventArgs e)
        {
            Parameters.setLight(40);
            Parameters.setTemperature(16);
            Parameters.setDoorStatus(0);
            Activities.setNume("Job");
            Activities.setOptionCode(16, 40);
            tvCurentActivityDoor.Text = Activities.getOptionCode();

            //Intent nextActivity = new Intent(this, typeof(MenuActivity));
            //StartActivity(nextActivity);


            //Device_id l-am setat deja in pagina de login!!!
            //TODO:  extragere din baza de date a coloanei action_id si incrementarea ei apoi ->Actions.setActionId(id-ul incrementat);
            Actions.setActionType("Temperatura");
            Actions.setValueAction(21);
            Actions.setActionTime(DateTime.Now);
            //TODO:  introducere date din Action in tabela "Actions" in baza de date

            //TODO:  extragere din baza de date a coloanei action_id si incrementarea ei apoi ->Actions.setActionId(id-ul incrementat);
            Actions.setActionType("Lumina");
            Actions.setValueAction(80);
            Actions.setActionTime(DateTime.Now);
            //TODO:  introducere date din Action in tabela "Actions" in baza de date
        }
    }
}