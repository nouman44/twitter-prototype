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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();
            String InputUser = username.Text;
            String Password = password.Text;
            int found;
            found = objmyDAl.VerifyLogin(InputUser, Password);

            userMsg.Text = "";
            passMsg.Text = "";

            if (username.Text == "admin") 
            {
                if(password.Text=="admin")
                    Response.Redirect("AdminPanel.aspx");
                else
                    passMsg.Text = "The password is incorrect!";
            }
            else if (found == 2)
            {
                Session["CurrentUser"] = username.Text;
                Session["UserProfile"] = Session["CurrentUser"];
                Response.Redirect("HomePage.aspx");
            }
            else if (found == 0)
            {
                userMsg.Text = "Username does not exist!";
            }
            else if (found == 1)
            {
                passMsg.Text = "The password is incorrect!";
            }

        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx");
        }

    }
}