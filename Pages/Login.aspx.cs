using BCrypt.Net;
using Login.Helpers;
using MyApp.Models;
using MyWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace MyWebApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void btnLogin_Click(object sender, EventArgs e_)
        {
            var type = InputIdentifier.Identify(txtUsername.Text);
            string phone = null;
            string email = null;

            if (type == InputIdentifier.InputType.Phone)
            {
                phone = txtUsername.Text;
            }
            else if(type == InputIdentifier.InputType.Email)
            {
                email = txtUsername.Text;
            }
            else
            {
                ShowError("Invalid Email or Phone Number format.");

                return;
            }


            
            try
            {
                using (var conn = DbHelper.CreateConnection())
                {
                    string sql = "SELECT * FROM auth.credentials WHERE (email IS NOT NULL AND email = @email) OR (login_phone IS NOT NULL AND login_phone = @login_phone)";

                    conn.Open();

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {

                        command.Parameters.AddWithValue("@email", (object)email ?? DBNull.Value);

                        command.Parameters.AddWithValue("@login_phone", (object)phone ?? DBNull.Value);

                        using (SqlDataReader read = command.ExecuteReader())
                        {

                            if (!read.HasRows)
                            {
                                ShowError("User not exist!");
                                return;
                            }


                            Credential credential = new Credential();

                            while (read.Read())
                            {
                                credential.Id = (Guid)read["id"];
                                credential.Email = read["email"] == DBNull.Value ? null : (string)read["email"];
                                credential.LoginPhone = read["login_phone"] == DBNull.Value ? null : (string)read["login_phone"];
                                credential.HashedPassword = Convert.ToString(read["hashed_password"]);
                            }

                            bool validate = BCrypt.Net.BCrypt.Verify(textPassword.Text, credential.HashedPassword);

                            if (validate)
                            {
                                ShowSuccess("Login successfull");

                            }
                            else
                            {
                                ShowError("invalid credentials!");

                            }
                        }
                    }
                }
            }
            catch(Exception e) 
            {

                ShowError(e.Message);
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e_)
        {
          
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
        private void ShowSuccess(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            lblError.BackColor = System.Drawing.Color.Green;

        }
    }
}