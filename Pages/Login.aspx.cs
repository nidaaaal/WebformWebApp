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
        protected async void btnLogin_Click(object sender, EventArgs e_)
        {
            var type = InputIdentifier.Identify(txtUsername.Text);

            if (type == InputIdentifier.InputType.Invalid)
            {
                ShowError("Invalid Email or Phone Number format.");

                return;
            }


            
            try
            {
                using (var conn = DbHelper.CreateConnection())
                {
                    string sql = "SELECT id,username,hashed_password FROM auth.credentials WHERE username = @username";

                   await conn.OpenAsync();

                    Credential credential = new Credential();


                    using (SqlCommand command =  new SqlCommand(sql, conn))
                    {

                        command.Parameters.AddWithValue("@username", txtUsername.Text);

                        using (SqlDataReader read = await command.ExecuteReaderAsync())
                        {

                            if (!read.HasRows)
                            {
                                ShowError("User not exist!");
                                return;
                            }



                            while (await read.ReadAsync())
                            {
                                credential.Id = (int)read["id"];
                                credential.UserName = Convert.ToString(read["username"]);
                                credential.HashedPassword = Convert.ToString(read["hashed_password"]);
                            }

                        }

                        if (credential == null || !BCrypt.Net.BCrypt.Verify(textPassword.Text, credential.HashedPassword))
                        {
                            ShowError("Invalid Username or Password.");
                            return;
                        }

                            DateTime now = DateTime.Now;
                            string newsql = "UPDATE auth.credentials SET login_at = @now WHERE id = @id;";

                            using (SqlCommand newcmd = new SqlCommand(newsql, conn))
                            {
                                newcmd.Parameters.AddWithValue("@now",now);
                                newcmd.Parameters.AddWithValue("@id",credential.Id);

                                await newcmd.ExecuteNonQueryAsync();

                                Session["user_id"] = credential.Id;
                            

                                ShowSuccess("Login successfull");
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
            string safeMessage = HttpUtility.JavaScriptStringEncode(message);

            string script = $"alert('{safeMessage}');";

            ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", script, true);
        }

        private void ShowSuccess(string message)
        {
            string safeMessage = HttpUtility.JavaScriptStringEncode(message);
            string script = $"alert('{safeMessage}');";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", script, true);
        }
    }
}