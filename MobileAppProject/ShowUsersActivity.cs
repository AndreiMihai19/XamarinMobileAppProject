using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Android.Provider.CalendarContract;

namespace MobileAppProject
{
    [Activity(Label = "ShowUsersActivity")]
    public class ShowUsersActivity : Activity
    {
        private TableLayout tableLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.show_users_activity);

            tableLayout = FindViewById<TableLayout>(Resource.Id.tblLayout);

            MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=mobile_app;User Id=root;Password=;charset=utf8");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM login", con);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string username = reader.GetString("username");
                            string password = reader.GetString("password");

                            TableRow tableRow = new TableRow(this);
                            tableRow.Clickable = true;
                            tableRow.SetBackgroundResource(Android.Resource.Drawable.ListSelectorBackground);

                            TextView textView1 = new TextView(this);
                            textView1.Text = username;
                            textView1.Gravity = GravityFlags.Center; // Aliniere la stânga
                            TableRow.LayoutParams layoutParams1 = new TableRow.LayoutParams();
                            layoutParams1.Weight = 1; // Se extinde pe tot spațiul disponibil
                            textView1.LayoutParameters = layoutParams1;

                            TextView textView2 = new TextView(this);
                            textView2.Text = password;
                            textView2.Gravity = GravityFlags.Center; // Aliniere la dreapta
                            TableRow.LayoutParams layoutParams2 = new TableRow.LayoutParams();
                            layoutParams2.Weight = 1; // Se extinde pe tot spațiul disponibil
                            textView2.LayoutParameters = layoutParams2;

                            tableRow.AddView(textView1);
                            tableRow.AddView(textView2);

                            tableRow.Click += TableRow_Click;

                            tableLayout.AddView(tableRow);
                        }

                    }

                    cmd.ExecuteNonQuery();

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());

            }
            finally
            {
                con.Close();
            }

        }

        private void TableRow_Click(object sender, EventArgs e)
        {
            TableRow selectedRow = (TableRow)sender;
            string selectedUsername = selectedRow.Text;

            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Editor");
            builder.SetMessage("Ați selectat un rând.");
            builder.SetPositiveButton("Modify user", (s, args) =>
            {
                // Acțiunea când se apasă butonul "OK"
            });
            builder.SetNegativeButton("Delete user", (s, args) =>
            {
                DeleteUserAndPassword(selectedUsername);
            });
            builder.SetNeutralButton("Exit", (s, args) =>
            {
                // Acțiunea când se apasă butonul "OK"
            });

            // Afișarea ferestrei pop-up
            Android.App.AlertDialog dialog = builder.Create();
            dialog.Show();
        }

        private void DeleteUserAndPassword(string username)
        {
            MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=mobile_app;User Id=root;Password=;charset=utf8");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM USERS WHERE username=@username", con);

                    cmd.Parameters.AddWithValue("@Username", username);

                    cmd.ExecuteNonQuery();

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());

            }
            finally
            {
                con.Close();
            }


        }
    }
}