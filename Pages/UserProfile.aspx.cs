using Login.Helpers;
using MyWebApp.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace MyWebApp
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                    await Load_Userdata();
               
            }

        }

        protected async Task Load_Userdata()
        {
            int id = Convert.ToInt32(Session["userId"]);

            var conn = DbHelper.CreateConnection();

            string sql = "SELECT * FROM app.users WHERE id = @id;";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                await conn.OpenAsync();

                cmd.Parameters.AddWithValue("@id", id);

                var read = await cmd.ExecuteReaderAsync();

                if (!await read.ReadAsync()) return;

                Users userData = new Users
                {
                    FirstName = Convert.ToString(read["first_name"]),
                    LastName = Convert.ToString(read["last_name"]),
                    DisplayName = read["display_name"] == DBNull.Value ? null : Convert.ToString(read["display_name"]),
                    DateOfBirth = Convert.ToDateTime(read["date_of_birth"]),
                    Gender = Convert.ToBoolean(read["gender"]),
                    Age = Convert.ToInt32(read["age"]),
                    Address = Convert.ToString(read["address"]),
                    City = read["city"] == DBNull.Value ? null : Convert.ToString(read["city"]),
                    State = read["state"] == DBNull.Value ? null : Convert.ToString(read["state"]),
                    ZipCode = Convert.ToInt32(read["zipcode"]),
                    Phone = Convert.ToString(read["phone"]),
                    Mobile = read["mobile"] == DBNull.Value ? null : Convert.ToString(read["mobile"])

                };

                Session["username"] = userData.DisplayName;

                txtFn.Text = userData.FirstName;
                txtLn.Text = userData.LastName;
                txtDn.Text = userData.DisplayName ?? "";
                txtDob.Text = userData.DateOfBirth.ToShortDateString();
                txtGender.Text = userData.Gender ? "Male" : "Female";
                txtAge.Text = userData.Age.ToString();
                txtAddress.Text = userData.Address;
                txtCity.Text = userData.City;
                txtState.Text = userData.State;
                txtZip.Text = userData.ZipCode.ToString();
                txtPhone.Text = userData.Phone.ToString();
                txtMobile.Text = userData.Mobile.ToString();
            }

        }

        protected void btnClick_Edit(object sender, EventArgs e)
        {

        }

        protected void btnClick_Image(object sender, EventArgs e)
        {

        }

        protected void btnClick_password(object sender, EventArgs e)
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