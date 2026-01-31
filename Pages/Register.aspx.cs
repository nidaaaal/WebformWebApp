using Login.Helpers;
using MyWebApp.Helpers;
using System;
using System.Data.SqlClient;


namespace MyWebApp
{

    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void btnSubmit_Click(object sender,EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtusername.Text) ||
         string.IsNullOrWhiteSpace(password.Text) ||
         string.IsNullOrWhiteSpace(fn.Text) ||      
         string.IsNullOrWhiteSpace(ln.Text) ||       
         string.IsNullOrWhiteSpace(address.Text) ||
         string.IsNullOrWhiteSpace(zip.Text) ||
         string.IsNullOrWhiteSpace(phone.Text))
            {
                ShowError("Please fill all required fields.");
                return;
            }
            string securePass = BCrypt.Net.BCrypt.HashPassword(password.Text);

            var type = InputIdentifier.Identify(txtusername.Text);
            string email = null;
            string logPhone = null;

            if(type == InputIdentifier.InputType.Email)
            {
                email = txtusername.Text.ToLower();
            }
            else if(type == InputIdentifier.InputType.Phone)
            {
                logPhone = txtusername.Text;
            }
            else
            {
                ShowError("Invalid Email or Phone Number format.");
                return;
            }

            string day = Request.Form[ddlDay.UniqueID];
            string month = Request.Form[ddlMonth.UniqueID];
            string year = Request.Form[ddlYear.UniqueID];

            if (string.IsNullOrEmpty(day) || day == "0" ||
                string.IsNullOrEmpty(month) || month == "0" ||
                string.IsNullOrEmpty(year) || year == "0")
            {
                ShowError("Please select your Day, Month, and Year of Birth.");
                return;
            }

            DateTime parsedDOB;
            string dateString = $"{day}/{month}/{year}";

            if (!DateTime.TryParseExact(dateString, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDOB))
            {
                ShowError("The date provided is not valid.");
                return;
            }

            if (parsedDOB < new DateTime(1900, 1, 1) || parsedDOB > DateTime.Now)
            {
                ShowError("Please enter a realistic Year of Birth.");
                return;
            }



            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@email",(object)email??DBNull.Value),
                new SqlParameter("@login_phone",(object)logPhone??DBNull.Value),
                new SqlParameter("@hashed_password",securePass),
                new SqlParameter("@first_name",fn.Text),
                new SqlParameter("@last_name",ln.Text),
                new SqlParameter("@display_name",string.IsNullOrEmpty(dn.Text) ? (object)DBNull.Value : dn.Text),
                new SqlParameter("@date_of_birth",parsedDOB),
                new SqlParameter("@gender",rblGender.SelectedValue),
                new SqlParameter("@address",address.Text),
                new SqlParameter("@city",ddlCity.SelectedValue),
                new SqlParameter("@state",ddlState.SelectedValue),
                new SqlParameter("@zipcode",zip.Text),
                new SqlParameter("@phone", string.IsNullOrEmpty(phone.Text) ? (object)DBNull.Value :phone.Text),
                new SqlParameter("@mobile", string.IsNullOrEmpty(mobile.Text) ? (object)DBNull.Value : mobile.Text),
            };

            try
            {
                using (SqlDataReader reader = DbHelper.ExecuteSp("register_user", sp))
                {
                    if (reader.Read())
                    {
                        int resultCode = Convert.ToInt32(reader["ResultCode"]);
                        string message = reader["Message"].ToString();
                        if (resultCode == -1)
                        {
                            ShowError("Email or phone number already linked with Another Account !");
                        }
                        else if (resultCode == 1)
                        {
                            ShowSuccess(message);
                            Response.Redirect("Login.aspx");

                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                ShowError(ex.Message);
            }



        }

        protected void btnCancel_Click(object sender, EventArgs e)
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

        }


    }
}