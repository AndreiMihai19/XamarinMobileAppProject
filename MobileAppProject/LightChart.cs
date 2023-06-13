using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Data;
using MobileAppProject.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MobileAppProject
{
    [Activity(Label = "LightChart")]
    public class LightChart : Activity
    {
        private MySqlConnection connection = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
       // private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        private LineChart lineChart;
        private Button btnBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            connection.Open();
            SetContentView(Resource.Layout.temperature_chart);

            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            lineChart = FindViewById<LineChart>(Resource.Id.line_chart);

            List<DataPoint> dataPoints = GetDataPoints();
            dataPoints.Sort((x, y) => TimeSpan.Compare(x.Time, y.Time));

            List<Entry> entries = new List<Entry>();
            foreach (DataPoint dataPoint in dataPoints)
            {
                Entry entry = new Entry((float)dataPoint.Time.TotalHours, dataPoint.Value);
                entries.Add(entry);
            }

            LineDataSet lineDataSet = new LineDataSet(entries, "Light (%)");
            LineData lineData = new LineData(lineDataSet);


            // Personalizarea graficului
            lineChart.AxisLeft.Enabled = true;
            lineChart.AxisRight.Enabled = false;
            lineChart.Legend.Enabled = true;


            // Setarea datelor pentru grafic
            lineChart.Data = lineData;
            lineChart.Invalidate();



            btnBack.Click += btnBack_Clicked;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(MenuActivity));
            StartActivity(nextActivity);
        }


        private List<DataPoint> GetDataPoints()
        {
            DateTime twentyFourHoursAgo = DateTime.Now.AddHours(-24);

            MySqlCommand cmdTemp = new MySqlCommand("SELECT * FROM Actions WHERE device_id=@device_id AND action_type= @action_type AND date_time >= @twentyFourHoursAgo", connection);
            cmdTemp.Parameters.AddWithValue("@device_id", User.getDeviceId());
            cmdTemp.Parameters.AddWithValue("@action_type", "light");
            cmdTemp.Parameters.AddWithValue("@twentyFourHoursAgo", twentyFourHoursAgo);

            MySqlDataReader reader = cmdTemp.ExecuteReader();

            List<DataPoint> dataPoints = new List<DataPoint>();


            while (reader.Read())
            {
                string date_time = reader.GetString("date_time");
                float value_action = reader.GetFloat("value_action");

                DateTime dateTime = DateTime.ParseExact(date_time, "dd.MM.yyyy HH:mm:ss", null);
                int hour = dateTime.Hour;
                int minute = dateTime.Minute;
                int second = dateTime.Second;

                DataPoint newDataPoint = new DataPoint();
                newDataPoint.Time = new TimeSpan(hour, minute, second);
                newDataPoint.Value = value_action;
                dataPoints.Add(newDataPoint);
            }
            reader.Close();

            return dataPoints;
        }



        public class DataPoint
        {
            public TimeSpan Time { get; set; }
            public float Value { get; set; }
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}