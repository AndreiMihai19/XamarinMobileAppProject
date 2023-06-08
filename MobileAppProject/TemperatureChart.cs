using Android.App;
using Android.Content;
using Android.Hardware.Camera2.Params;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MikePhil.Charting.Charts;
using MikePhil.Charting.Components;
using MikePhil.Charting.Data;
using MikePhil.Charting.Formatter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Lang;



namespace MobileAppProject
{
    [Activity(Label = "TemperatureChart")]
    public class TemperatureChart : Activity
    {
        private LineChart lineChart;
        private Button btnBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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

            LineDataSet lineDataSet = new LineDataSet(entries, "Temperatura (°C)");
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
            Intent nextActivity = new Intent(this, typeof(MainActivity));
            StartActivity(nextActivity);
        }



        /*
    private List<Entry> GetChartData()
    {
        List<Entry> entries = new List<Entry>
        {
            new Entry(0, 10),
            new Entry(1, 15),
            new Entry(2, 8),
            new Entry(3, 20),
            new Entry(4, 12),
            new Entry(5, 18),
            new Entry(6, 7)
        };

        return entries;
    }
    */

        private List<DataPoint> GetDataPoints()
        {
            List<DataPoint> dataPoints = new List<DataPoint>
    {
        new DataPoint { Time = new TimeSpan(8, 23, 15), Value = 10 },
        new DataPoint { Time = new TimeSpan(9, 31, 43), Value = 15 },
        new DataPoint { Time = new TimeSpan(10, 04, 12), Value = 8 },
        new DataPoint { Time = new TimeSpan(11, 00, 59), Value = 20 },
        new DataPoint { Time = new TimeSpan(12, 02, 54), Value = 12 },
        new DataPoint { Time = new TimeSpan(15, 15, 23), Value = 18 },
        new DataPoint { Time = new TimeSpan(18, 15, 14), Value = 7 }
    };

            return dataPoints;
        }
        public class DataPoint
        {
            public TimeSpan Time { get; set; }
            public float Value { get; set; }
        }
    }


}