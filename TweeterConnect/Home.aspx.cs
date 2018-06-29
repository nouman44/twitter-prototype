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
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowUserStats();
            ShowFollowedTweets();
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentUser");
            Response.Redirect("Login.aspx");
        }

        protected void ShowUserStats()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["CurrentUser"].ToString();
            string firstName = "", lastName = "";
            int noTweets = 0, noFollowing = 0, noFollowers = 0;

            objmyDAl.GetName(username, ref firstName, ref lastName);
            objmyDAl.GetUserStats(username, ref noTweets, ref noFollowing, ref noFollowers);

            txtUsername.Text = "@" + Session["CurrentUser"].ToString();
            txtFName.Text = firstName.ToString();
            txtLName.Text = lastName.ToString();
            txtTweets.Text = noTweets.ToString();
            txtFollowers.Text = noFollowers.ToString();
            txtFollowing.Text = noFollowing.ToString();
        }

        protected void PostTweet_Click(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["CurrentUser"].ToString();
            DateTime date = DateTime.Now.Date;
            DateTime time = DateTime.Now;
            String tweet = txtPost.Text;

            objmyDAl.PostTweet(username, tweet, date, time);

            txtPost.Text = "";
        }

     
        protected void ShowFollowedTweets()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["CurrentUser"].ToString();
            DataTable DT = new DataTable();

            objmyDAl.ShowFollowedTweets(username, ref DT);
            int i = 0;
            String pUsername, pFName = "", pLName = "", pTweet, pTweetDate, pTweetTime;
            Label lblFName, lblLName, lblDateTime;
            LinkButton lnkUsername;
            int index;

            foreach (DataRow DR in DT.Rows)
            {
                pUsername = DT.Rows[i]["UserName"].ToString();
                objmyDAl.GetName(pUsername, ref pFName, ref pLName);
                pTweet = DT.Rows[i]["Tweet"].ToString();
                pTweetDate = DT.Rows[i]["TweetDate"].ToString();
                pTweetTime = DT.Rows[i]["TweetTime"].ToString();

                index = pTweetDate.IndexOf(' ');
                pTweetDate = pTweetDate.Substring(0, index);

                index = pTweetTime.IndexOf('.');
                pTweetTime = pTweetTime.Substring(0, index);

                System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createDiv.Attributes.Add("class", "verticalBox");

                lblFName = new Label();
                lblLName = new Label();
                lblDateTime = new Label();
                lnkUsername = new LinkButton();

                lblFName.Attributes.Add("class", "tweetName");
                lblFName.Attributes.Add("runat","server");
                lblFName.Text = pFName + " ";

                lblLName.Attributes.Add("class", "tweetName");
                lblLName.Attributes.Add("runat", "server");
                lblLName.Text = pLName + "  ";

                lnkUsername.Attributes.Add("class", "tweetUser");
                lnkUsername.Attributes.Add("runat", "server");
                lnkUsername.Text = "@" + pUsername;

                System.Web.UI.HtmlControls.HtmlGenericControl tweetPara = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
                tweetPara.InnerText = pTweet;

                lblDateTime.Attributes.Add("class", "tweetDateTime");
                lblDateTime.Attributes.Add("runat", "server");
                lblDateTime.Text = pTweetDate + " at " + pTweetTime;

                createDiv.Controls.Add(lblFName);
                createDiv.Controls.Add(lblLName);
                createDiv.Controls.Add(lnkUsername);
                createDiv.Controls.Add(new LiteralControl("<hr />"));
                createDiv.Controls.Add(tweetPara);
                createDiv.Controls.Add(lblDateTime);

                contentBox.Controls.Add(createDiv);

                i++;
            }
            
        }
    }
}