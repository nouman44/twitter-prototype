using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using TweeterConnect.DAL;

namespace TweeterConnect
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();
            String InUsername = username.Text;
            String InPassword = password.Text;
            String InFirstName = firstname.Text;
            String InLastName = lastname.Text;
            DateTime InBDate = bDate.SelectedDate;
            String InEmail = email.Text;
            String InCity = city.Text;
            String InPhone = phone.Text;
            String InCountry = country.Text;
            String InGender;

            bool isChecked = rdFemale.Checked;
            if (isChecked)
                InGender = rdFemale.Text;
            else
                InGender = rdMale.Text;

            userMsg.Text = "";

            int found;
            found = objmyDAl.DoSignUp(InUsername, InPassword,InFirstName,InLastName,InBDate,InEmail,InCity,InPhone,InCountry,InGender);

            if (found == 1)
                Response.Redirect("Login.aspx");
            else if (found == 0)
                userMsg.Text = "This username already exists!";
        }
    }
}