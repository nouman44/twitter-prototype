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
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowUserStats();
            ShowTrendingTweets();
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentUser");
            Session.Remove("UserProfile");
            Session.Remove("ProfileViewType");
            Session.Remove("Search");
            Session.Remove("Message");
            Response.Redirect("Login.aspx");
        }

        protected void NavHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        protected void NavProfile_Click(object sender, EventArgs e)
        {
            Session["UserProfile"] = Session["CurrentUser"];
            Session["ProfileViewType"] = "TweetView";
            Response.Redirect("Profile.aspx");
        }

        protected void NavMessages_Click(object sender, EventArgs e)
        {
            Session["Message"] = "None";
            Response.Redirect("Messaging.aspx");
        }

        protected void NavBlocking_Click(object sender, EventArgs e)
        {
            Response.Redirect("Blocking.aspx");
        }

        protected void NavSearch_Click(object sender, EventArgs e)
        {
            Session["Search"] = txtSearch.Text;
            Response.Redirect("Search.aspx");
        }



        protected void ShowUserStats()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["UserProfile"].ToString();
            String currentUser = Session["CurrentUser"].ToString();
            String firstName = "", lastName = "";
            int noTweets = 0, noFollowing = 0, noFollowers = 0;

            objmyDAl.GetName(username, ref firstName, ref lastName);
            objmyDAl.GetUserStats(username, ref noTweets, ref noFollowing, ref noFollowers);

            txtUsername.Text = "@" + Session["UserProfile"].ToString();
            txtFName.Text = firstName.ToString();
            txtLName.Text = lastName.ToString();

            int blockCheck = objmyDAl.CheckIsBlocked(username, currentUser);
            if (blockCheck == 0)
            {
                txtTweets.Text = noTweets.ToString();
                txtFollowers.Text = noFollowers.ToString();
                txtFollowing.Text = noFollowing.ToString();
            }
            else
            {
                txtTweets.Text = "-";
                txtFollowers.Text = "-";
                txtFollowing.Text = "-";
            }
        }

        protected void SearchHashtag(object sender, EventArgs e)
        {
            String hashString = (sender as LinkButton).Text;

            Session["Search"] = hashString;
            Response.Redirect("Search.aspx");
        }

        protected void ShowTrendingTweets()
        {
            myDAL objmyDAl = new myDAL();
            DataTable DT = new DataTable();

            objmyDAl.ShowTrendingTweets(ref DT);
            int i = 0;
            String hashTag;
            LinkButton lnkHashtag;

            foreach (DataRow DR in DT.Rows)
            {
                hashTag = DT.Rows[i]["Hashtag"].ToString();

                lnkHashtag = new LinkButton();
                lnkHashtag.Attributes.Add("class", "trendHashtag");
                lnkHashtag.Attributes.Add("runtat", "server");
                lnkHashtag.Click += SearchHashtag;
                lnkHashtag.Text = hashTag;

                trending.Controls.Add(lnkHashtag);
                trending.Controls.Add(new LiteralControl("<br />"));
                trending.Controls.Add(new LiteralControl("<br />"));

                i++;
            }
        }


    }
}