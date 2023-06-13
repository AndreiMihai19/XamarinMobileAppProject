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

      //private MySqlConnection connection = new MySqlConnection("Server=34.30.254.246;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        private MySqlConnection connection = new MySqlConnection("Server=34.118.112.126;Port=3306;database=HomeAutomation;User Id=root;Password=1234;charset=utf8");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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
            layoutParamsTitleDID.Weight = 1; 
            textViewTitleDID.LayoutParameters = layoutParamsTitleDID;
            textViewTitleDID.SetBackgroundResource(Resource.Drawable.inner_border);

            TextView textViewTitleU = new TextView(this)
            {
                Text = "USERNAME",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleU = new TableRow.LayoutParams();
            layoutParamsTitleU.Weight = 1;
            textViewTitleU.LayoutParameters = layoutParamsTitleU;
            textViewTitleU.SetBackgroundResource(Resource.Drawable.inner_border);


            TextView textViewTitleFN = new TextView(this)
            {
                Text = "FIRST NAME",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleFN = new TableRow.LayoutParams();
            layoutParamsTitleFN.Weight = 1; 
            textViewTitleFN.LayoutParameters = layoutParamsTitleFN;
            textViewTitleFN.SetBackgroundResource(Resource.Drawable.inner_border);

            TextView textViewTitleLN = new TextView(this)
            {
                Text = "LAST NAME",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleLN = new TableRow.LayoutParams();
            layoutParamsTitleLN.Weight = 1; 
            textViewTitleLN.LayoutParameters = layoutParamsTitleLN;
            textViewTitleLN.SetBackgroundResource(Resource.Drawable.inner_border);

            TextView textViewTitleCNP = new TextView(this)
            {
                Text = "CNP",
                TextSize = 11,
                Gravity = GravityFlags.Center
            };
            TableRow.LayoutParams layoutParamsTitleCNP = new TableRow.LayoutParams();
            layoutParamsTitleCNP.Weight = 1;
            textViewTitleCNP.LayoutParameters = layoutParamsTitleCNP;
            textViewTitleCNP.SetBackgroundResource(Resource.Drawable.inner_border);


            tableRowOne.AddView(textViewTitleDID);
            tableRowOne.AddView(textViewTitleU);
            tableRowOne.AddView(textViewTitleFN);
            tableRowOne.AddView(textViewTitleLN);
            tableRowOne.AddView(textViewTitleCNP);

            tableLayout.AddView(tableRowOne);

            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users", connection);

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
                                Weight = 1 

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
                                Weight = 1 
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
                                Weight = 1 
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
                                Weight = 1 
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
                                Weight = 1 
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
                         reader.Close();
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
                connection.Close();
            }

            btnBack.Click += btnBack_Clicked;

        }

        private void TableRow_Click(object sender, EventArgs e)
        {
            TableRow selectedRow = (TableRow)sender;

            TextView usernameTextView = (TextView)selectedRow.GetChildAt(1);
            string selectedUsername = usernameTextView.Text;
            string selectedPassword = null;
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT passwrd FROM Users WHERE email=@Username", connection);
                    cmd.Parameters.AddWithValue("@Username", selectedUsername);

                    object result = cmd.ExecuteScalar();
                    selectedPassword = (string)result;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());

            }
            finally
            {
                connection.Close();
            }

            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);

            builder.SetTitle("Editor");

            builder.SetPositiveButton("Modify user", (s, args) =>
            {
                Android.App.AlertDialog.Builder builderModify = new Android.App.AlertDialog.Builder(this);
                builderModify.SetTitle($"Set user credentials for {selectedUsername}");

                LinearLayout layout = new LinearLayout(this);
                layout.Orientation = Orientation.Vertical;

                TextView message1 = new TextView(this);
                message1.Text = "Username";
                layout.AddView(message1);

                EditText editText1 = new EditText(this);
                layout.AddView(editText1);

                TextView message2 = new TextView(this);
                message2.Text = "Password";
                message2.InputType = Android.Text.InputTypes.TextVariationPassword;
                layout.AddView(message2);

                EditText editText2 = new EditText(this);
                layout.AddView(editText2);

                builderModify.SetView(layout);

                builderModify.SetPositiveButton("Modify", (s, args) =>
                {
                    EditUserAndPassword(selectedUsername, selectedPassword, editText1.Text, editText2.Text);
                });

                builderModify.SetNeutralButton("Exit", (s, args) =>
                {

                });

                Android.App.AlertDialog dialogModify = builderModify.Create();
                dialogModify.Show();
            });
            builder.SetNegativeButton("Delete user", (s, args) =>
            {
                Android.App.AlertDialog.Builder builderDelete = new Android.App.AlertDialog.Builder(this);

                builderDelete.SetTitle($"Are you sure you want to delete {selectedUsername} user?");

                builderDelete.SetPositiveButton("Yes", (s, args) =>
                {
                    DeleteUserAndPassword(selectedUsername);
                });

                builderDelete.SetNegativeButton("No", (s, args) =>
                {

                });

                Android.App.AlertDialog dialogDelete= builderDelete.Create();
                dialogDelete.Show();


            });
            builder.SetNeutralButton("Exit", (s, args) =>
            {
              
            });

            Android.App.AlertDialog dialog = builder.Create();
            dialog.Show();
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
            tvUser.Text = User.getUser();
        }

        private void DeleteUserAndPassword(string username)
        {
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Users WHERE email=@Username", connection);

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
                connection.Close();
            }

            var toast = Toast.MakeText(this, "Please wait...", ToastLength.Short);

            toast.SetGravity(GravityFlags.Center, 0, 0);

            toast.Show();

            Recreate();
        }

        private void EditUserAndPassword(string oldUsername,string oldPassword,string newUsername, string newPassword)
        {
            HashConfiguration hashConfig = new HashConfiguration();
            var hashPassword = hashConfig.HashPassword(newPassword);

            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                 
                    MySqlCommand cmdUsr = new MySqlCommand("UPDATE Users SET email=@newUsername WHERE email=@oldUsername", connection);
                    MySqlCommand cmdPwd = new MySqlCommand("UPDATE Users SET passwrd=@newPassword WHERE passwrd=@oldPassword", connection);
                   
                    cmdUsr.Parameters.AddWithValue("@newUsername", newUsername);
                    cmdPwd.Parameters.AddWithValue("@newPassword", hashPassword);
                    cmdUsr.Parameters.AddWithValue("@oldUsername", oldUsername);
                    cmdPwd.Parameters.AddWithValue("@oldPassword", oldPassword);

                    cmdUsr.ExecuteNonQueryAsync();
                    cmdPwd.ExecuteNonQueryAsync();

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());

            }
            finally
            {
                connection.Close();
            }

            var toast =  Toast.MakeText(this, "Please wait...", ToastLength.Short);
            toast.SetGravity(GravityFlags.Center, 0, 0);

            toast.Show();


            Recreate();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(AdminActivity));
            StartActivity(nextActivity);
        }
    }
}