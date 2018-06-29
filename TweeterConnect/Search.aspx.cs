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
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String searchString = Session["Search"].ToString();

            if (searchString[0] == '@')
                ShowUsers();
            else if (searchString[0] == '#')
                ShowTweets();
            else
                ShowError();

        }

        protected void UpdateProfileSession(object sender, EventArgs e)
        {
            String username = (sender as LinkButton).Text;
            username = username.Substring(1, username.Length - 1);

            Session["UserProfile"] = username;
            Session["ProfileViewType"] = "TweetView";
            Response.Redirect("Profile.aspx");
        }


        protected void Retweet_Click(object sender, EventArgs e)
        {
            IEnumerator myEnum = (sender as Button).Parent.Controls.GetEnumerator();
            Label myLabel;
            int i = 0, tweetID = 0;

            while (myEnum.MoveNext())
            {
                if (myEnum.Current is Label)
                {
                    myLabel = (Label)myEnum.Current;

                    if (i == 2)
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

        protected void ShowUsers()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["Search"].ToString();
            username = username.Substring(1, username.Length - 1);
            DataTable DT = new DataTable();

            int found = objmyDAl.SearchUsers(username, ref DT);

            System.Web.UI.HtmlControls.HtmlGenericControl resultUser = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            resultUser.Attributes.Add("class", "verticalBox");

            if (found == 0)
            {
                resultUser.Controls.Add(new LiteralControl("<h3>No Results Found</h3>"));
                resultUser.Controls.Add(new LiteralControl("<hr />"));
                searchResults.Controls.Add(resultUser);
            }
            else
            {
                String sUsername, sFName = "", sLName = "", currentUser = Session["CurrentUser"].ToString();
                int i = 0;

                Label lblFName, lblLName;
                LinkButton lnkUsername;

                resultUser.Controls.Add(new LiteralControl("<h3>Results for User Search</h3>"));
                resultUser.Controls.Add(new LiteralControl("<hr />"));
                searchResults.Controls.Add(resultUser);


                foreach (DataRow DR in DT.Rows)
                {
                    sUsername = DT.Rows[i]["UserName"].ToString();
                    sFName = DT.Rows[i]["FirstName"].ToString();
                    sLName = DT.Rows[i]["LastName"].ToString();

                    System.Web.UI.HtmlControls.HtmlGenericControl userDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    userDiv.Attributes.Add("class", "userBox");

                    lblFName = new Label();
                    lblLName = new Label();
                    lnkUsername = new LinkButton();

                    lblFName.Attributes.Add("class", "searchName");
                    lblFName.Attributes.Add("runat", "server");
                    lblFName.Text = sFName + " ";

                    lblLName.Attributes.Add("class", "searchName");
                    lblLName.Attributes.Add("runat", "server");
                    lblLName.Text = sLName + "  ";

                    lnkUsername.Attributes.Add("class", "searchUser");
                    lnkUsername.Attributes.Add("runat", "server");
                    lnkUsername.Click += UpdateProfileSession;
                    lnkUsername.Text = "@" + sUsername;

                    userDiv.Controls.Add(lblFName);
                    userDiv.Controls.Add(lblLName);
                    userDiv.Controls.Add(lnkUsername);

                    searchResults.Controls.Add(userDiv);

                    i++;
                }
            }

        }

        protected void ShowTweets()
        {
            myDAL objmyDAl = new myDAL();

            String hashString = Session["Search"].ToString();
            DataTable DT = new DataTable();

            int found = objmyDAl.SearchHashtags(hashString, ref DT);

            System.Web.UI.HtmlControls.HtmlGenericControl resultHash = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            resultHash.Attributes.Add("class", "verticalBox");

            if (found == 0)
            {
                resultHash.Controls.Add(new LiteralControl("<h3>No Results Found</h3>"));
                resultHash.Controls.Add(new LiteralControl("<hr />"));
                searchResults.Controls.Add(resultHash);
            }
            else
            {
                resultHash.Controls.Add(new LiteralControl("<h3>Results for Hashtag Search</h3>"));
                resultHash.Controls.Add(new LiteralControl("<hr />"));
                searchResults.Controls.Add(resultHash);

                int i = 0;
                String pUsername, pFName = "", pLName = "", pTweet, pTweetDate, pTweetTime, pTweetID, currentUser = Session["CurrentUser"].ToString();
                Label lblFName, lblLName, lblDate, lblTweetID, lblTime;
                LinkButton lnkUsername;
                Button btnRetweet;
                int index;

                foreach (DataRow DR in DT.Rows)
                {
                    pTweetID = DT.Rows[i]["TweetID"].ToString();
                    pUsername = DT.Rows[i]["UserName"].ToString();
                    objmyDAl.GetName(pUsername, ref pFName, ref pLName);
                    pTweet = DT.Rows[i]["Tweet"].ToString();
                    pTweetDate = DT.Rows[i]["TweetDate"].ToString();
                    pTweetTime = DT.Rows[i]["TweetTime"].ToString();

                    int blockCheck = objmyDAl.CheckIsBlocked(pUsername, currentUser);

                    if (blockCheck == 0)
                    {
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

                        lblTweetID.Text = pTweetID;
                        lblFName.Text = pFName + " ";
                        lblLName.Text = pLName + "  ";
                        lnkUsername.Text = "@" + pUsername;
                        tweetPara.InnerText = pTweet;
                        lblDate.Text = pTweetDate + " at ";
                        lblTime.Text = pTweetTime;

                        createDiv.Controls.Add(lblFName);
                        createDiv.Controls.Add(lblLName);
                        createDiv.Controls.Add(lblTweetID);
                        createDiv.Controls.Add(lnkUsername);
                        createDiv.Controls.Add(new LiteralControl("<hr />"));
                        createDiv.Controls.Add(tweetPara);
                        createDiv.Controls.Add(lblDate);
                        createDiv.Controls.Add(lblTime);

                        createDiv.Controls.Add(new LiteralControl("<br />"));
                        createDiv.Controls.Add(new LiteralControl("<br />"));
                        createDiv.Controls.Add(btnRetweet);

                        createDiv.Attributes.Add("class", "verticalBox");
                        searchResults.Controls.Add(createDiv);
                    }

                    i++;
                }
            }
        }

        protected void ShowError()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl resultHash = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            resultHash.Attributes.Add("class", "verticalBox");

            resultHash.Controls.Add(new LiteralControl("<h3>Please start search with # for hashtags or @ for users</h3>"));
            resultHash.Controls.Add(new LiteralControl("<hr />"));
            searchResults.Controls.Add(resultHash);
        }
    }
}