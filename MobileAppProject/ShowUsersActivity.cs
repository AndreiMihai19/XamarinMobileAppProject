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
            TextView usernameTextView = (TextView)selectedRow.GetChildAt(0);
            TextView passwordTextView = (TextView)selectedRow.GetChildAt(1);
            string selectedUsername = usernameTextView.Text;
            string selectedPassword = passwordTextView.Text;

            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);

            builder.SetTitle("Editor");

            builder.SetPositiveButton("Modify user", (s, args) =>
            {
                builder.SetTitle("Set user credentials");

                LinearLayout layout = new LinearLayout(this);
                layout.Orientation = Orientation.Vertical;

                TextView message1 = new TextView(this);
                message1.Text = "Username";
                layout.AddView(message1);

                // Adăugarea primului EditText în layout
                EditText editText1 = new EditText(this);
                layout.AddView(editText1);

                TextView message2 = new TextView(this);
                message2.Text = "Password";
                message2.InputType = Android.Text.InputTypes.TextVariationPassword;
                layout.AddView(message2);

                // Adăugarea celui de-al doilea EditText în layout
                EditText editText2 = new EditText(this);
                layout.AddView(editText2);

                builder.SetView(layout);

                builder.SetPositiveButton("Modify", (dialog, which) =>
                {
                    EditUserAndPassword(selectedUsername, selectedPassword, editText1.Text, editText2.Text);
                });

                builder.SetNegativeButton("Anulare", (dialog, which) =>
                {
                });

                // Afișarea dialogului alert
                Android.App.AlertDialog dialog1 = builder.Create();
                dialog1.Show();
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

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM login WHERE username=@Username", con);

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

        private void EditUserAndPassword(string oldUsername,string oldPassword,string newUsername, string newPassword)
        {
            MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=mobile_app;User Id=root;Password=;charset=utf8");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("UPDATE login SET username=@newUsername,password=@newPassword WHERE username=@oldUsername AND password=@oldPassword", con);
                    cmd.Parameters.AddWithValue("@newUsername", newUsername);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.Parameters.AddWithValue("@oldUsername", oldUsername);
                    cmd.Parameters.AddWithValue("@oldPassword", oldPassword);

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