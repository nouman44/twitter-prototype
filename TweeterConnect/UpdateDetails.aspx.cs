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
    public partial class UpdateDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void UpdateProfileSession(object sender, EventArgs e)
        {
            String username = (sender as LinkButton).Text;
            username = username.Substring(1, username.Length - 1);

            Session["UserProfile"] = username;
            Session["ProfileViewType"] = "TweetView";
            Response.Redirect("Profile.aspx");
        }

        protected void ShowUserDetails(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();

            String username = "", firstName = "", lastName = "", email = "", city = "", country = "", gender = "", privacy = "", phone = "";
            DateTime birthDate = DateTime.Now;

            username = Session["CurrentUser"].ToString();

            objmyDAl.GetUserDetails(username, ref firstName, ref lastName, ref birthDate, ref email, ref city, ref phone, ref country, ref gender, ref privacy);

            lnkUsername.Text = "@" + username;
            txtFName.Text = firstName;
            txtLName.Text = lastName;

            if (gender == "M")
                rdMale.Checked = true;
            else
                rdFemale.Checked = true;

            if (privacy == "Public")
                rdPublic.Checked = true;
            else
                rdPrivate.Checked = true;

            txtPhone.Text = phone;
            txtCity.Text = city;
            txtCountry.Text = country;

            bDate.SelectedDate = birthDate;
        }

        protected void UpdateUserDetails(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();

            String username = "", firstName = "", lastName = "", city = "", country = "", gender = "", privacy = "", phone = "";
            DateTime birthDate;

            username = Session["CurrentUser"].ToString();
            firstName = txtFName.Text;
            lastName = txtLName.Text;
            city = txtCity.Text;
            country = txtCountry.Text;
            phone = txtPhone.Text;
            birthDate = bDate.SelectedDate;
          
            bool isChecked = rdFemale.Checked;
            if (isChecked)
                gender = rdFemale.Text;
            else
                gender = rdMale.Text;

            isChecked = rdPublic.Checked;
            if (isChecked)
                privacy = rdPublic.Text;
            else
                privacy = rdPrivate.Text;

            objmyDAl.UpdateUserDetails(username, firstName, lastName, birthDate, city, phone, country, gender, privacy);
            Response.Write("<script>alert('Successfully Updated!');</script>");
            Response.Redirect("Profile.aspx");
        }
    }
}