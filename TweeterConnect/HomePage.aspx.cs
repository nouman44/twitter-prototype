using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using TweeterConnect.DAL;

namespace TweeterConnect
{
    public partial class HomePage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserProfile"] = Session["CurrentUser"];
            ShowFollowedTweets();
        }

        protected void PostTweet_Click(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["CurrentUser"].ToString();
            DateTime date = DateTime.Now.Date;
            DateTime time = DateTime.Now;
            String tweet = txtPost.Text;

            if (tweet.Length == 0)
            {
                Response.Write("<script>alert('Please enter something in the tweet!');</script>");
            }
            else
            {
                objmyDAl.PostTweet(username, tweet, date, time);

                txtPost.Text = "";
                Response.Write("<script>alert('Succesfully Tweeted!');</script>");
            }

        }

        protected void Retweet_Click(object sender, EventArgs e)
        {
            IEnumerator myEnum = (sender as Button).Parent.Controls.GetEnumerator();
            Label myLabel;
            int i = 0, tweetID = 0;

            while (myEnum.MoveNext())
            {
                if(myEnum.Current is Label)
                {
                    myLabel = (Label)myEnum.Current;

                    if(i==2)
                    {
                        myLabel = (Label)myEnum.Current;
                        tweetID = Convert.ToInt32(myLabel.Text);
                    }
                    i++;
                }
            }

            myDAL objmyDAl = new myDAL();

            String username = Session["CurrentUser"].ToString();
            DateTime date = DateTime.Now.Date;
            DateTime time = DateTime.Now;

            objmyDAl.Retweet(username, date, time, tweetID);

            Response.Write("<script>alert('Succesfully Retweeted!');</script>");
        }


        protected void ShowFollowedTweets()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["CurrentUser"].ToString();
            DataTable DT = new DataTable();

            objmyDAl.ShowFollowedTweets(username, ref DT);
            int i = 0;
            String pUsername, pFName = "", pLName = "", pTweet, pTweetDate, pTweetTime, pTweetID;
            String rUsername = "", rFName = "", rLName = "", rTweet = "", rpTweetDate = "";
            DateTime rTweetDate = DateTime.Now.Date, rTweetTime = DateTime.Now;
            Label lblFName, lblLName, lblDate, lblTweetID, lblTime;
            Label lblRFName, lblRLName, lblRDate, lblRTweetID, lblRTime, lblTag;
            LinkButton lnkUsername, lnkRUsername;
            Button btnRetweet;
            int index, originalTweetID;

            foreach (DataRow DR in DT.Rows)
            {
                pTweetID = DT.Rows[i]["TweetID"].ToString();
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

                lblFName = new Label();
                lblLName = new Label();
                lblDate = new Label();
                lblTime = new Label();
                lnkUsername = new LinkButton();
                lblTweetID = new Label();
                btnRetweet = new Button();

                lblTweetID.Attributes.Add("class", "hidden");
                lblTweetID.Attributes.Add("runat", "server");

                lblFName.Attributes.Add("class", "tweetName");
                lblFName.Attributes.Add("runat", "server");

                lblLName.Attributes.Add("class", "tweetName");
                lblLName.Attributes.Add("runat", "server");

                lnkUsername.Attributes.Add("class", "tweetUser");
                lnkUsername.Attributes.Add("runat", "server");
                lnkUsername.Click += UpdateProfileSession;

                System.Web.UI.HtmlControls.HtmlGenericControl tweetPara = new System.Web.UI.HtmlControls.HtmlGenericControl("P");

                lblDate.Attributes.Add("class", "tweetDateTime");
                lblDate.Attributes.Add("runat", "server");
                lblDate.Text = pTweetDate + " at ";

                lblTime.Attributes.Add("class", "tweetDateTime");
                lblTime.Attributes.Add("runat", "server");
                lblTime.Text = pTweetTime;

                btnRetweet.Attributes.Add("class", "btn btn-default");
                btnRetweet.Attributes.Add("runat", "server");
                btnRetweet.Click += Retweet_Click;
                btnRetweet.Text = "Retweet";

                originalTweetID = objmyDAl.CheckRetweet(Convert.ToInt32(pTweetID));

                if (originalTweetID == 0) //is not a retweet
                {
                    lblTweetID.Text = pTweetID;
                    lblFName.Text = pFName + " ";
                    lblLName.Text = pLName + "  ";
                    lnkUsername.Text = "@" + pUsername;
                    tweetPara.InnerText = pTweet;
                    lblDate.Text = pTweetDate + " at ";
                    lblTime.Text = pTweetTime;
                }
                else //is a retweet
                {
                    objmyDAl.GetTweet(originalTweetID, ref rUsername, ref rTweet, ref rTweetDate, ref rTweetTime);
                    objmyDAl.GetName(rUsername, ref rFName, ref rLName);

                    rpTweetDate = rTweetDate.ToString();
                    index = rpTweetDate.IndexOf(' ');
                    rpTweetDate = rpTweetDate.Substring(0, index);

                    lblTweetID.Text = originalTweetID.ToString();
                    lblFName.Text = rFName + " ";
                    lblLName.Text = rLName + "  ";
                    lnkUsername.Text = "@" + rUsername;
                    tweetPara.InnerText = rTweet;
                    lblDate.Text = rpTweetDate;
                }

                createDiv.Controls.Add(lblFName);
                createDiv.Controls.Add(lblLName);
                createDiv.Controls.Add(lblTweetID);
                createDiv.Controls.Add(lnkUsername);
                createDiv.Controls.Add(new LiteralControl("<hr />"));
                createDiv.Controls.Add(tweetPara);
                createDiv.Controls.Add(lblDate);

                if (originalTweetID == 0)
                    createDiv.Controls.Add(lblTime);

                createDiv.Controls.Add(new LiteralControl("<br />"));
                createDiv.Controls.Add(new LiteralControl("<br />"));
                createDiv.Controls.Add(btnRetweet);

                if (originalTweetID == 0)  //not a retweet
                {
                    createDiv.Attributes.Add("class", "verticalBox");
                    tweetBox.Controls.Add(createDiv);
                }
                else //is a retweet
                {
                    createDiv.Attributes.Add("class", "verticalBox retweet");
                    System.Web.UI.HtmlControls.HtmlGenericControl outerDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    outerDiv.Attributes.Add("class", "verticalBox");

                    lblRFName = new Label();
                    lblRLName = new Label();
                    lblRDate = new Label();
                    lblRTweetID = new Label();
                    lblRTime = new Label();
                    lnkRUsername = new LinkButton();
                    lblTag = new Label();

                    lblRTweetID.Attributes.Add("class", "hidden");
                    lblRTweetID.Attributes.Add("runat", "server");

                    lblRFName.Attributes.Add("class", "tweetName");
                    lblRFName.Attributes.Add("runat", "server");

                    lblRLName.Attributes.Add("class", "tweetName");
                    lblRLName.Attributes.Add("runat", "server");

                    lnkRUsername.Attributes.Add("class", "tweetUser");
                    lnkRUsername.Attributes.Add("runat", "server");
                    lnkRUsername.Click += UpdateProfileSession;

                    lblTag.Text = "Retweeted: ";

                    lblRDate.Attributes.Add("class", "tweetDateTime");
                    lblRDate.Attributes.Add("runat", "server");
                    lblRDate.Text = pTweetDate + " at ";

                    lblRTime.Attributes.Add("class", "tweetDateTime");
                    lblRTime.Attributes.Add("runat", "server");
                    lblRTime.Text = pTweetTime;

                    lblRTweetID.Text = pTweetID;
                    lblRFName.Text = pFName + " ";
                    lblRLName.Text = pLName + "  ";
                    lnkRUsername.Text = "@" + pUsername;

                    outerDiv.Controls.Add(lblRFName);
                    outerDiv.Controls.Add(lblRLName);
                    outerDiv.Controls.Add(lblRTweetID);
                    outerDiv.Controls.Add(lnkRUsername);
                    outerDiv.Controls.Add(new LiteralControl("<br />"));
                    outerDiv.Controls.Add(new LiteralControl("<br />"));
                    outerDiv.Controls.Add(lblTag);
                    outerDiv.Controls.Add(new LiteralControl("<br />"));
                    outerDiv.Controls.Add(createDiv);
                    outerDiv.Controls.Add(new LiteralControl("<br />"));
                    outerDiv.Controls.Add(lblRDate);
                    outerDiv.Controls.Add(lblRTime);

                    tweetBox.Controls.Add(outerDiv);
                }


                i++;
            }

        }

        protected void UpdateProfileSession(object sender, EventArgs e)
        {
            String username = (sender as LinkButton).Text;
            username = username.Substring(1, username.Length-1);

            Session["UserProfile"] = username;
            Session["ProfileViewType"] = "TweetView";
            Response.Redirect("Profile.aspx");
        }
    }
}