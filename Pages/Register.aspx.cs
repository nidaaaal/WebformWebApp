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

        protected async void btnSubmit_Click(object sender,EventArgs e)
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


            var type = InputIdentifier.Identify(txtusername.Text);

            if(InputIdentifier.InputType.Invalid == type)
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

            int parsedZip;
            if (!int.TryParse(zip.Text, out parsedZip))
            {
                ShowError("Zipcode must be a valid number.");
                return;
            }

            int age;
            if (!int.TryParse(txtAge.Text, out age))
            {
                ShowError("Zipcode must be a valid number.");
                return;
            }

            bool genderBit = (rblGender.SelectedValue.ToLower() == "male" || rblGender.SelectedValue == "0");

            string securePass = BCrypt.Net.BCrypt.HashPassword(password.Text);

          


            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@username",txtusername.Text),
                new SqlParameter("@hashed_password",securePass),
                new SqlParameter("@first_name",fn.Text),
                new SqlParameter("@last_name",ln.Text),
                new SqlParameter("@display_name",string.IsNullOrEmpty(dn.Text) ? (object)DBNull.Value : dn.Text),
                new SqlParameter("@date_of_birth",parsedDOB),
                new SqlParameter("@age",age),
                new SqlParameter("@gender",genderBit),
                new SqlParameter("@address",address.Text),
                new SqlParameter("@city",ddlCity.SelectedValue),
                new SqlParameter("@state",ddlState.SelectedValue),
                new SqlParameter("@zipcode",parsedZip),
                new SqlParameter("@phone", string.IsNullOrEmpty(phone.Text) ? (object)DBNull.Value :phone.Text),
                new SqlParameter("@mobile", string.IsNullOrEmpty(mobile.Text) ? (object)DBNull.Value : mobile.Text),
            };

            try
            {
                using (SqlDataReader reader = await DbHelper.ExecuteSp("auth.register_user", sp))
                {
                    if (await reader.ReadAsync())
                    {
                        int resultCode = Convert.ToInt32(reader["ResultCode"]);
                        string message = reader["Message"].ToString();

                        System.Diagnostics.Debug.WriteLine(message);


                        if (resultCode == -1)
                        {
                            ShowError("Email or phone number already linked with Another Account !");
                        }
                        else if (resultCode == 1)
                        {
                            ShowSuccess(message);
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
            string safeMessage = System.Web.HttpUtility.JavaScriptStringEncode(message);

            string script = $"alert('{safeMessage}');";

            ClientScript.RegisterStartupScript(this.GetType(), "ErrorPopup", script, true);
        }

        private void ShowSuccess(string message)
        {
            string safeMessage = System.Web.HttpUtility.JavaScriptStringEncode(message);
            string script = $"alert('{safeMessage}');";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessPopup", script, true);
        }


    }
}