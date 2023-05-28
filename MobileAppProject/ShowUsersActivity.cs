using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.Widget;
using Google.Android.Material.Snackbar;
using MobileAppProject.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.CalendarContract;

namespace MobileAppProject
{
    [Activity(Label = "ShowUsersActivity")]
    public class ShowUsersActivity : Activity
    {
        private TableLayout tableLayout;
        private TextView tvDoorStatus;
        private TextView tvUser;
        private Button btnBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.show_users_activity);

            tableLayout = FindViewById<TableLayout>(Resource.Id.tblLayout);
            tvDoorStatus = FindViewById<TextView>(Resource.Id.door_status);
            tvUser = FindViewById<TextView>(Resource.Id.username);
            btnBack = FindViewById<Button>(Resource.Id.XbtnBack);
            UpdateDoorStatusUser();

            TableRow tableRowOne = new TableRow(this);
            tableRowOne.Clickable = true;
            tableRowOne.SetBackgroundResource(Resource.Drawable.inner_border);
             
            TextView textViewTitleDID = new TextView(this)
            {
                Text = "DEVICE ID",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleDID = new TableRow.LayoutParams();
            layoutParamsTitleDID.Weight = 1; // Se extinde pe tot spațiul disponibil
            textViewTitleDID.LayoutParameters = layoutParamsTitleDID;
            textViewTitleDID.SetBackgroundResource(Resource.Drawable.inner_border);

            TextView textViewTitleU = new TextView(this)
            {
                Text = "USERNAME",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleU = new TableRow.LayoutParams();
            layoutParamsTitleU.Weight = 1; // Se extinde pe tot spațiul disponibil
            textViewTitleU.LayoutParameters = layoutParamsTitleU;
            textViewTitleU.SetBackgroundResource(Resource.Drawable.inner_border);


            TextView textViewTitleFN = new TextView(this)
            {
                Text = "FIRST NAME",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleFN = new TableRow.LayoutParams();
            layoutParamsTitleFN.Weight = 1; // Se extinde pe tot spațiul disponibil
            textViewTitleFN.LayoutParameters = layoutParamsTitleFN;
            textViewTitleFN.SetBackgroundResource(Resource.Drawable.inner_border);

            TextView textViewTitleLN = new TextView(this)
            {
                Text = "LAST NAME",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleLN = new TableRow.LayoutParams();
            layoutParamsTitleLN.Weight = 1; // Se extinde pe tot spațiul disponibil
            textViewTitleLN.LayoutParameters = layoutParamsTitleLN;
            textViewTitleLN.SetBackgroundResource(Resource.Drawable.inner_border);

            TextView textViewTitleCNP = new TextView(this)
            {
                Text = "CNP",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleCNP = new TableRow.LayoutParams();
            layoutParamsTitleCNP.Weight = 1; // Se extinde pe tot spațiul disponibil
            textViewTitleCNP.LayoutParameters = layoutParamsTitleCNP;
            textViewTitleCNP.SetBackgroundResource(Resource.Drawable.inner_border);


            tableRowOne.AddView(textViewTitleDID);
            tableRowOne.AddView(textViewTitleU);
            tableRowOne.AddView(textViewTitleFN);
            tableRowOne.AddView(textViewTitleLN);
            tableRowOne.AddView(textViewTitleCNP);

            tableLayout.AddView(tableRowOne);



            MySqlConnection con = new MySqlConnection("Server=34.118.112.126;Port=3306;database=homematicDB;User Id=root;Password=;charset=utf8");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM users", con);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string device_id= reader.GetString("device_id");
                            string username = reader.GetString("email");
                            string first_name = reader.GetString("first_name");
                            string last_name = reader.GetString("last_name");
                            string cnp = reader.GetString("CNP");

                            TableRow tableRow = new TableRow(this);
                            tableRow.Clickable = true;

                            TableRow.LayoutParams layoutParams = new TableRow.LayoutParams(TableRow.LayoutParams.WrapContent, TableRow.LayoutParams.WrapContent);
                            layoutParams.LeftMargin = 40; 
                            layoutParams.RightMargin = 0; 
                            tableRow.LayoutParameters = layoutParams;
                            tableRow.SetBackgroundResource(Resource.Drawable.inner_border);

                            TextView txtDeviceID = new TextView(this)
                            {
                                Text = device_id,
                                TextSize = 10,
                                Gravity = GravityFlags.Center
                            };

                            var layoutParamsDeviceID = new TableRow.LayoutParams
                            {
                                Weight = 1 // Se extinde pe tot spațiul disponibil

                            };
                            txtDeviceID.LayoutParameters = layoutParamsDeviceID;
                            txtDeviceID.SetBackgroundResource(Resource.Drawable.inner_border);

                            TextView txtUsername = new TextView(this)
                            {
                                Text = username,
                                TextSize = 10,
                                Gravity = GravityFlags.Center
                            };

                            var layoutParamsUsername = new TableRow.LayoutParams
                            {
                                Weight = 1 // Se extinde pe tot spațiul disponibil
                            };
                            txtUsername.LayoutParameters = layoutParamsUsername;
                            txtUsername.SetBackgroundResource(Resource.Drawable.inner_border);

                            TextView txtFirstName = new TextView(this)
                            {
                                Text = first_name,
                                TextSize = 10,
                                Gravity = GravityFlags.Center
                            };
                            var layoutParamsFirstName = new TableRow.LayoutParams
                            {
                                Weight = 1 // Se extinde pe tot spațiul disponibil
                            };
                            txtFirstName.LayoutParameters = layoutParamsFirstName;
                            txtFirstName.SetBackgroundResource(Resource.Drawable.inner_border);

                            TextView txtLastName = new TextView(this)
                            {
                                Text = last_name,
                                TextSize = 10,
                                Gravity = GravityFlags.Center
                            };
                            var layoutParamsLastName = new TableRow.LayoutParams
                            {
                                Weight = 1 // Se extinde pe tot spațiul disponibil
                            };
                            txtLastName.LayoutParameters = layoutParamsLastName;
                            txtLastName.SetBackgroundResource(Resource.Drawable.inner_border);

                            TextView txtCNP = new TextView(this)
                            {
                                Text = cnp,
                                TextSize = 10,
                                Gravity = GravityFlags.Center
                            };
                            var layoutParamsCNP = new TableRow.LayoutParams
                            {
                                Weight = 1 // Se extinde pe tot spațiul disponibil
                            };
                            txtCNP.LayoutParameters = layoutParamsCNP;
                            txtCNP.SetBackgroundResource(Resource.Drawable.inner_border);


                            tableRow.AddView(txtDeviceID);
                            tableRow.AddView(txtUsername);
                            tableRow.AddView(txtFirstName);
                            tableRow.AddView(txtLastName);
                            tableRow.AddView(txtCNP);

                            tableRow.Click += TableRow_Click;

                            tableLayout.AddView(tableRow);
                        }
                      //  reader.Close();
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

            btnBack.Click += btnBack_Clicked;

        }

        private void TableRow_Click(object sender, EventArgs e)
        {
            TableRow selectedRow = (TableRow)sender;

            TextView usernameTextView = (TextView)selectedRow.GetChildAt(0);
            TextView passwordTextView = (TextView)selectedRow.GetChildAt(1);
            string selectedUsername = usernameTextView.Text;
            string selectedPassword = usernameTextView.Text;

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


                Android.App.AlertDialog dialog1 = builder.Create();
                dialog1.Show();
            });
            builder.SetNegativeButton("Delete user", (s, args) =>
            {
                 DeleteUserAndPassword(selectedUsername);
            });
            builder.SetNeutralButton("Exit", (s, args) =>
            {
              
            });

            // Afișarea ferestrei pop-up
            Android.App.AlertDialog dialog = builder.Create();
            dialog.Show();
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

            var toast = Toast.MakeText(this, "Please wait...", ToastLength.Short);

            toast.SetGravity(GravityFlags.Center, 0, 0);

            toast.Show();

            Recreate();
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

            //var toast =  Toast.MakeText(this, "Please wait...", ToastLength.Short);
            //toast.SetGravity(GravityFlags.Center, 0, 0);

            //toast.Show();


            Recreate();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(AdminActivity));
            StartActivity(nextActivity);
        }
    }
}