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
    public partial class ViewDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowUserDetails();
        }

        protected void UpdateProfileSession(object sender, EventArgs e)
        {
            String username = (sender as LinkButton).Text;
            username = username.Substring(1, username.Length - 1);

            Session["UserProfile"] = username;
            Session["ProfileViewType"] = "TweetView";
            Response.Redirect("Profile.aspx");
        }

        protected void ShowUserDetails()
        {
            myDAL objmyDAl = new myDAL();

            String username = "", firstName = "", lastName = "", email = "", city = "", country = "", gender = "", privacy = "", phone = "", bDate = "";
            DateTime birthDate = DateTime.Now;

            username = Session["UserProfile"].ToString();

            objmyDAl.GetUserDetails(username, ref firstName, ref lastName, ref birthDate, ref email, ref city, ref phone, ref country, ref gender, ref privacy);

            lnkUsername.Text = "@" + username;
            lblFName.Text = firstName;
            lblLName.Text = lastName;

            if (gender == "M")
                lblGender.Text = "Male";
            else
                lblGender.Text = "Female";

            if (privacy == "Public") 
            {
                lblEmail.Text = email;
                lblPhone.Text = phone;
                lblCity.Text = city;
                lblCountry.Text = country;

                bDate = birthDate.ToString();
                int index = bDate.IndexOf(' ');
                bDate = bDate.Substring(0, index);
                lblBDate.Text = bDate;
            }
            else
            {
                tagEmail.Visible = false;
                tagPhone.Visible = false;
                tagCity.Visible = false;
                tagCountry.Visible = false;
                tagBDate.Visible = false;

                lblEmail.Attributes.Add("class", "hidden");
                lblPhone.Attributes.Add("class", "hidden");
                lblCity.Attributes.Add("class", "hidden");
                lblCountry.Attributes.Add("class", "hidden");
                lblBDate.Attributes.Add("class", "hidden");
            }
        }




    }
}