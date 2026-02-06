using Login.Helpers;
using MyWebApp.Helpers;
using MyWebApp.Models;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
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
                txtDob.Text = userData.DateOfBirth.ToString("dd/MM/yyyy");
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
            IsEditing = true;
            ToggleFieldMode(true);


        }
        private bool IsEditing
        {
            get => ViewState["IsEditing"] as bool? ?? false;
            set => ViewState["IsEditing"] = value;
        }

        private void ToggleFieldMode(bool enable)
        {
            txtFn.ReadOnly = !enable;   
            txtLn.ReadOnly = !enable;
            txtDn.ReadOnly = !enable;
            txtDob.ReadOnly = !enable;
            txtGender.ReadOnly = !enable;
            txtAddress.ReadOnly = !enable;
            txtCity.ReadOnly = !enable;
            txtState.ReadOnly = !enable;
            txtZip.ReadOnly = !enable;
            txtPhone.ReadOnly = !enable;
            txtMobile.ReadOnly = !enable;

            btnUpdate.Visible = enable;
            btnCancel.Visible = enable;
        }

        protected async void btn_Update(object sender, EventArgs e)
        {
            int id = Session["userId"] != null ? Convert.ToInt32(Session["userId"]) : 0;

            if (id == 0) return; 
            

            if (string.IsNullOrWhiteSpace(txtFn.Text) ||
               string.IsNullOrWhiteSpace(txtLn.Text) ||
               string.IsNullOrWhiteSpace(txtDob.Text) ||
               string.IsNullOrWhiteSpace(txtGender.Text) ||
               string.IsNullOrWhiteSpace(txtAddress.Text) ||
               string.IsNullOrWhiteSpace(txtZip.Text) ||
               string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                ShowError("Please fill all required fields.");
                return;
            }

            if (!DateTime.TryParseExact(
                    txtDob.Text.Trim(),
                    new[] { "dd/MM/yyyy", "d/M/yyyy", "dd/MM/yy" },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDOB))
            {
                ShowError("Please enter Date of Birth in dd/MM/yyyy format.");
                return;
            }

            if (parsedDOB < new DateTime(1900, 1, 1) || parsedDOB > DateTime.Today)
            {
                ShowError("Please enter a realistic Date of Birth.");
                return;
            }

            if (!int.TryParse(txtZip.Text.Trim(), out int parsedZip))
            {
                ShowError("Zipcode must be a valid number.");
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out int age))
            {
                ShowError("Age must be a valid number.");
                return;
            }

            int calculatedAge = DateTime.Today.Year - parsedDOB.Year;
            if (parsedDOB.Date > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (txtGender.Text.Trim().ToLower() != "male" && txtGender.Text.Trim().ToLower() != "female")
            {
                ShowError("Please enter valid Gender");
                return;
            }

            bool genderValue = txtGender.Text.Trim().ToLower() == "male" ? true : false;

            var paramiters = new SqlParameter[]
         {
                new SqlParameter("@id",id),
                new SqlParameter("@first_name",txtFn.Text),
                new SqlParameter("@last_name",txtLn.Text),
                new SqlParameter("@display_name",string.IsNullOrEmpty(txtDn.Text) ? (object)DBNull.Value : txtDn.Text),
                new SqlParameter("@date_of_birth",parsedDOB),
                new SqlParameter("@age",calculatedAge),
                new SqlParameter("@gender",genderValue),
                new SqlParameter("@address",txtAddress.Text),
                new SqlParameter("@city",string.IsNullOrEmpty(txtCity.Text) ? (object)DBNull.Value : txtCity.Text),
                new SqlParameter("@state",string.IsNullOrEmpty(txtState.Text) ? (object)DBNull.Value : txtState.Text),
                new SqlParameter("@zipcode",parsedZip),
                new SqlParameter("@phone",txtPhone.Text),
                new SqlParameter("@mobile",string.IsNullOrEmpty(txtMobile.Text) ? (object)DBNull.Value : txtMobile.Text)
         };

            var read = await DbHelper.ExecuteSp("app.profile_update", paramiters);

            await read.ReadAsync();

            int ResultCode = Convert.ToInt32(read["ResultCode"]);
            string Message = Convert.ToString(read["Message"]) ?? "";

            if(ResultCode == -1)
            {
                ShowError(Message);
            }
            if (ResultCode == 1)
            {

                ShowSuccess(Message);

                await Load_Userdata();

                ToggleFieldMode(true);

            }



        }

        protected async void btn_Cancel(object sender, EventArgs e)
        {
            IsEditing = false;
            ToggleFieldMode(false);
            await Load_Userdata();

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

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorPopup", script, true);
        }

        private void ShowSuccess(string message)
        {
            string safeMessage = HttpUtility.JavaScriptStringEncode(message);
            string script = $"alert('{safeMessage}');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessPopup", script, true);
        }
    }
}