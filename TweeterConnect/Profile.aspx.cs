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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();

            String currentUser = Session["CurrentUser"].ToString(), userProfile = Session["UserProfile"].ToString();
            int privacyType = objmyDAl.CheckIsFollower(userProfile, currentUser);

            if (Session["CurrentUser"].ToString() == Session["UserProfile"].ToString())
                privacyType = 1;

            String blocked = Session["UserProfile"].ToString();
            String blocker = Session["CurrentUser"].ToString();
            int blockCheck = objmyDAl.CheckIsBlocked(blocked, blocker);

            if (blockCheck == 1)
                privacyType = 2;

            ShowProfileBox(privacyType);

            if (privacyType == 1 && Session["ProfileViewType"].ToString() == "TweetView")
                ShowUserTweets();
            else if (privacyType == 1 && Session["ProfileViewType"].ToString() == "FollowerView")
                ShowUserFollowers();
            else if (privacyType == 1 && Session["ProfileViewType"].ToString() == "FollowingView")
                ShowUserFollowing();
        }

        protected void TweetView_Click(object sender, EventArgs e)
        {
            Session["ProfileViewType"] = "TweetView";
            Response.Redirect("Profile.aspx");
        }

        protected void FollowerView_Click(object sender, EventArgs e)
        {
            Session["ProfileViewType"] = "FollowerView";
            Response.Redirect("Profile.aspx");
        }

        protected void FollowingView_Click(object sender, EventArgs e)
        {
            Session["ProfileViewType"] = "FollowingView";
            Response.Redirect("Profile.aspx");
        }

        protected void FollowUser_Click(object sender, EventArgs e)
        {
            IEnumerator myEnum = (sender as Button).Parent.Controls.GetEnumerator();
            LinkButton myLink;
            String fUsername = "", currentUser = Session["CurrentUser"].ToString();

            while (myEnum.MoveNext())
            {
                if (myEnum.Current is LinkButton)
                {
                    myLink = (LinkButton)myEnum.Current;
                    fUsername = myLink.Text;
                }
            }

            myDAL objmyDAl = new myDAL();
            fUsername = fUsername.Substring(1, fUsername.Length - 1);

            objmyDAl.FollowUser(currentUser, fUsername);

            Response.Write("<script>alert('You Followed the user!');</script>");
            Response.Redirect("Profile.aspx");
        }

        protected void UnfollowUser_Click(object sender, EventArgs e)
        {
            IEnumerator myEnum = (sender as Button).Parent.Controls.GetEnumerator();
            LinkButton myLink;
            String fUsername = "", currentUser = Session["CurrentUser"].ToString();

            while (myEnum.MoveNext())
            {
                if (myEnum.Current is LinkButton)
                {
                    myLink = (LinkButton)myEnum.Current;
                    fUsername = myLink.Text;
                }
            }

            myDAL objmyDAl = new myDAL();
            fUsername = fUsername.Substring(1, fUsername.Length - 1);

            objmyDAl.UnfollowUser(currentUser, fUsername);

            Response.Write("<script>alert('You Unfollowed the user!');</script>");
            Response.Redirect("Profile.aspx");
        }

        protected void FollowUser(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();
            String currentUser = Session["CurrentUser"].ToString(), profUser = Session["UserProfile"].ToString();

            objmyDAl.FollowUser(currentUser, profUser);

            Response.Write("<script>alert('You Followed the user!');</script>");
            Response.Redirect("Profile.aspx");
        }

        protected void UnfollowUser(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();
            String currentUser = Session["CurrentUser"].ToString(), profUser = Session["UserProfile"].ToString();

            objmyDAl.UnfollowUser(currentUser, profUser);

            Response.Write("<script>alert('You Unfollowed the user!');</script>");
            Response.Redirect("Profile.aspx");
        }

        protected void ViewProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewDetails.aspx");
        }

        protected void UpdateProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateDetails.aspx");
        }

        protected void BlockUser_Click(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();

            String blocked = Session["UserProfile"].ToString();
            String blocker = Session["CurrentUser"].ToString();

            objmyDAl.BlockUser(blocked, blocker);

            Response.Redirect("HomePage.aspx");
        }

        protected void DeleteTweet_Click(object sender, EventArgs e)
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

            objmyDAl.DeleteTweet(tweetID);

            Response.Write("<script>alert('Succesfully Deleted!');</script>");
            ShowUserTweets();
        }

        protected void MessageUser_Click(object sender, EventArgs e)
        {
            Session["Message"] = Session["UserProfile"].ToString();
            Response.Redirect("Messaging.aspx");
        }

        protected void ShowProfileBox(int privacyType)
        {
            Label lblFName, lblLName, lblUsername, lblTweets, lblFollowers, lblFollowing, tagFollowers, tagFollowing, tagTweets;
            Button btnFollow, btnUnfollow, btnBlock, btnView, btnUpdate, btnMessage;
            LinkButton lnkTweets, lnkFollowers, lnkFollowing;
            System.Web.UI.HtmlControls.HtmlGenericControl spanTweet = new System.Web.UI.HtmlControls.HtmlGenericControl("SPAN");
            System.Web.UI.HtmlControls.HtmlGenericControl spanFollower = new System.Web.UI.HtmlControls.HtmlGenericControl("SPAN");
            System.Web.UI.HtmlControls.HtmlGenericControl spanFollowing = new System.Web.UI.HtmlControls.HtmlGenericControl("SPAN");

            myDAL objmyDAl = new myDAL();

            String username = Session["UserProfile"].ToString();
            String firstName = "", lastName = "";
            int noTweets = 0, noFollowing = 0, noFollowers = 0;

            objmyDAl.GetName(username, ref firstName, ref lastName);
            objmyDAl.GetUserStats(username, ref noTweets, ref noFollowing, ref noFollowers);

            lblFName = new Label();
            lblLName = new Label();
            lblUsername = new Label();
            tagFollowers = new Label();
            tagFollowing = new Label();
            tagTweets = new Label();

            lblFName.Attributes.Add("class", "profTweetName");
            lblFName.Attributes.Add("runat", "server");
            lblFName.Text = firstName + " ";

            lblLName.Attributes.Add("class", "profTweetName");
            lblLName.Attributes.Add("runat", "server");
            lblLName.Text = lastName + "  ";

            lblUsername.Attributes.Add("class", "profTweetUser");
            lblUsername.Attributes.Add("runat", "server");
            lblUsername.Text = "@" + username;

            tagTweets.Text = noTweets.ToString();
            tagFollowers.Text = noFollowers.ToString();
            tagFollowing.Text = noFollowing.ToString();

            spanTweet.Attributes.Add("class", "statBlock");
            spanFollower.Attributes.Add("class", "statBlock");
            spanFollowing.Attributes.Add("class", "statBlock");

            System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            createDiv.Attributes.Add("class", "verticalBox");

            createDiv.Controls.Add(lblFName);
            createDiv.Controls.Add(lblLName);
            createDiv.Controls.Add(lblUsername);

            if (privacyType == 0) //Current user not following user
            {
                lblTweets = new Label();
                lblFollowers = new Label();
                lblFollowing = new Label();
                btnFollow = new Button();
                btnBlock = new Button();
                btnMessage = new Button();

                lblTweets.Attributes.Add("class", "statLabel");
                lblFollowers.Attributes.Add("class", "statLabel");
                lblFollowing.Attributes.Add("class", "statLabel");
                lblTweets.Text = "TWEETS |  ";
                lblFollowing.Text = "FOLLOWING |  ";
                lblFollowers.Text = " FOLLOWERS";

                btnFollow.Attributes.Add("class", "btn btn-default btnProf");
                btnFollow.Attributes.Add("runat", "server");
                btnFollow.Click += FollowUser;
                btnFollow.Text = "Follow";

                btnMessage.Attributes.Add("class", "btn btn-default btnProf");
                btnMessage.Attributes.Add("runat", "server");
                btnMessage.Click += MessageUser_Click;
                btnMessage.Text = "Message";

                btnBlock.Attributes.Add("class", "btn btn-default btnProf");
                btnBlock.Attributes.Add("runat", "server");
                btnBlock.Click += BlockUser_Click;
                btnBlock.Text = "Block";

                createDiv.Controls.Add(btnBlock);
                createDiv.Controls.Add(btnMessage);
                createDiv.Controls.Add(btnFollow);
                createDiv.Controls.Add(new LiteralControl("<hr />"));

                spanTweet.Controls.Add(lblTweets);
                spanTweet.Controls.Add(tagTweets);
                spanFollowing.Controls.Add(lblFollowing);
                spanFollowing.Controls.Add(tagFollowing);
                spanFollower.Controls.Add(lblFollowers);
                spanFollower.Controls.Add(tagFollowers);

                createDiv.Controls.Add(spanTweet);
                createDiv.Controls.Add(spanFollowing);
                createDiv.Controls.Add(spanFollower);

            }
            else if (privacyType == 1) //Current user following user or own profile
            {
                lnkTweets = new LinkButton();
                lnkFollowers = new LinkButton();
                lnkFollowing = new LinkButton();

                lnkTweets.Attributes.Add("class", "statLabel");
                lnkFollowers.Attributes.Add("class", "statLabel");
                lnkFollowing.Attributes.Add("class", "statLabel");
                lnkTweets.Text = "TWEETS |";
                lnkFollowing.Text = "FOLLOWING |";
                lnkFollowers.Text = "FOLLOWERS";

                lnkFollowers.Click += FollowerView_Click;
                lnkFollowing.Click += FollowingView_Click;
                lnkTweets.Click += TweetView_Click;

                if (Session["CurrentUser"].ToString() != Session["UserProfile"].ToString())
                {
                    btnUnfollow = new Button();
                    btnBlock = new Button();
                    btnView = new Button();
                    btnMessage = new Button();

                    btnUnfollow.Attributes.Add("class", "btn btn-default btnProf");
                    btnUnfollow.Attributes.Add("runat", "server");
                    btnUnfollow.Click += UnfollowUser;
                    btnUnfollow.Text = "Unfollow";

                    btnBlock.Attributes.Add("class", "btn btn-default btnProf");
                    btnBlock.Attributes.Add("runat", "server");
                    btnBlock.Click += BlockUser_Click;
                    btnBlock.Text = "Block";

                    btnMessage.Attributes.Add("class", "btn btn-default btnProf");
                    btnMessage.Attributes.Add("runat", "server");
                    btnMessage.Click += MessageUser_Click;
                    btnMessage.Text = "Message";

                    btnView.Attributes.Add("class", "btn btn-default btnProf");
                    btnView.Attributes.Add("runat", "server");
                    btnView.Click += ViewProfile_Click;
                    btnView.Text = "View Details";

                    createDiv.Controls.Add(btnBlock);
                    createDiv.Controls.Add(btnView);
                    createDiv.Controls.Add(btnMessage);
                    createDiv.Controls.Add(btnUnfollow);

                }
                else
                {
                    btnUpdate = new Button();

                    btnUpdate.Attributes.Add("class", "btn btn-default btnProf");
                    btnUpdate.Attributes.Add("runat", "server");
                    btnUpdate.Click += UpdateProfile_Click;
                    btnUpdate.Text = "Update Details";

                    createDiv.Controls.Add(btnUpdate);
                }
                createDiv.Controls.Add(new LiteralControl("<hr />"));

                spanTweet.Controls.Add(lnkTweets);
                spanTweet.Controls.Add(tagTweets);
                spanFollowing.Controls.Add(lnkFollowing);
                spanFollowing.Controls.Add(tagFollowing);
                spanFollower.Controls.Add(lnkFollowers);
                spanFollower.Controls.Add(tagFollowers);

                createDiv.Controls.Add(spanTweet);
                createDiv.Controls.Add(new LiteralControl("&nbsp"));
                createDiv.Controls.Add(spanFollowing);
                createDiv.Controls.Add(new LiteralControl("&nbsp"));
                createDiv.Controls.Add(spanFollower);


            }
            else if (privacyType == 2)  //Current user is blocked
            {
                createDiv.Controls.Add(new LiteralControl("<hr />"));
                System.Web.UI.HtmlControls.HtmlGenericControl blockMsg = new System.Web.UI.HtmlControls.HtmlGenericControl("P");
                blockMsg.InnerHtml = "You cannot view this profile due to privacy concerns";
                createDiv.Controls.Add(blockMsg);
                createDiv.Controls.Add(new LiteralControl("<hr />"));
            }

            profBox.Controls.Add(createDiv);
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
            ShowUserTweets();
        }


        protected void ShowUserTweets()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["UserProfile"].ToString();
            DataTable DT = new DataTable();

            objmyDAl.ShowUserTweets(username, ref DT);
            int i = 0;
            String pUsername, pFName = "", pLName = "", pTweet, pTweetDate, pTweetTime, pTweetID;
            String rUsername = "", rFName = "", rLName = "", rTweet = "", rpTweetDate = "";
            DateTime rTweetDate = DateTime.Now.Date, rTweetTime = DateTime.Now;
            Label lblFName, lblLName, lblDate, lblTweetID, lblTime;
            Label lblRFName, lblRLName, lblRDate, lblRTweetID, lblRTime, lblTag;
            LinkButton lnkUsername, lnkRUsername;
            int index, originalTweetID;
            Button btnDelete, btnRetweet;

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
                btnDelete = new Button();
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

                btnDelete.Attributes.Add("class", "btn btn-default");
                btnDelete.Attributes.Add("runat", "server");
                btnDelete.Click += DeleteTweet_Click;
                btnDelete.Text = "Delete";

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

                if (Session["CurrentUser"].ToString() == Session["UserProfile"].ToString())
                {
                    createDiv.Controls.Add(btnDelete);
                    createDiv.Controls.Add(new LiteralControl("&nbsp"));
                }

                createDiv.Controls.Add(btnRetweet);

                if (originalTweetID == 0)  //not a retweet
                {
                    createDiv.Attributes.Add("class", "verticalBox");
                    profBox.Controls.Add(createDiv);
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
                    if (Session["CurrentUser"].ToString() == Session["UserProfile"].ToString())
                    {
                        outerDiv.Controls.Add(new LiteralControl("<br />"));
                        outerDiv.Controls.Add(new LiteralControl("<br />"));
                        outerDiv.Controls.Add(btnDelete);
                    }

                    profBox.Controls.Add(outerDiv);
                }


                i++;
            }

        }

        protected void ShowUserFollowers()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["UserProfile"].ToString();
            DataTable DT = new DataTable();

            objmyDAl.GetUserFollowers(username, ref DT);

            String fUsername, fFName = "", fLName = "", currentUser = Session["CurrentUser"].ToString();
            int i = 0, privacyType;

            Label lblFName, lblLName;
            LinkButton lnkUsername;
            Button btnFollow, btnUnfollow;


            foreach (DataRow DR in DT.Rows)
            {
                fUsername = DT.Rows[i]["FUserName"].ToString();

                objmyDAl.GetName(fUsername, ref fFName, ref fLName);
                privacyType = objmyDAl.CheckIsFollower(fUsername, currentUser);

                System.Web.UI.HtmlControls.HtmlGenericControl followerDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                followerDiv.Attributes.Add("class", "followBox");

                lblFName = new Label();
                lblLName = new Label();
                lnkUsername = new LinkButton();

                lblFName.Attributes.Add("class", "tweetName");
                lblFName.Attributes.Add("runat", "server");
                lblFName.Text = fFName + " ";

                lblLName.Attributes.Add("class", "tweetName");
                lblLName.Attributes.Add("runat", "server");
                lblLName.Text = fLName + "  ";

                lnkUsername.Attributes.Add("class", "tweetUser");
                lnkUsername.Attributes.Add("runat", "server");
                lnkUsername.Click += UpdateProfileSession;
                lnkUsername.Text = "@" + fUsername;

                followerDiv.Controls.Add(lblFName);
                followerDiv.Controls.Add(lblLName);
                followerDiv.Controls.Add(lnkUsername);

                int blockCheck = objmyDAl.CheckIsBlocked(fUsername, currentUser);

                if (blockCheck != 1 && (fUsername != currentUser))
                {
                    if (privacyType == 0)
                    {
                        btnFollow = new Button();
                        btnFollow.Attributes.Add("class", "btn btn-default btnProf");
                        btnFollow.Attributes.Add("runat", "server");
                        btnFollow.Click += FollowUser_Click;
                        btnFollow.Text = "Follow";

                        followerDiv.Controls.Add(btnFollow);
                    }
                    else if (privacyType == 1)
                    {
                        btnUnfollow = new Button();
                        btnUnfollow.Attributes.Add("class", "btn btn-default btnProf");
                        btnUnfollow.Attributes.Add("runat", "server");
                        btnUnfollow.Click += UnfollowUser_Click;
                        btnUnfollow.Text = "Unfollow";

                        followerDiv.Controls.Add(btnUnfollow);
                    }
                }

                profBox.Controls.Add(followerDiv);

                i++;
            }

        }

        protected void ShowUserFollowing()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["UserProfile"].ToString();
            DataTable DT = new DataTable();

            objmyDAl.GetUserFollowing(username, ref DT);

            String fUsername, fFName = "", fLName = "", currentUser = Session["CurrentUser"].ToString();
            int i = 0, privacyType;

            Label lblFName, lblLName;
            LinkButton lnkUsername;
            Button btnFollow, btnUnfollow;


            foreach (DataRow DR in DT.Rows)
            {
                fUsername = DT.Rows[i]["FUserName"].ToString();

                objmyDAl.GetName(fUsername, ref fFName, ref fLName);
                privacyType = objmyDAl.CheckIsFollower(fUsername, currentUser);

                System.Web.UI.HtmlControls.HtmlGenericControl followerDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                followerDiv.Attributes.Add("class", "followBox");

                lblFName = new Label();
                lblLName = new Label();
                lnkUsername = new LinkButton();

                lblFName.Attributes.Add("class", "tweetName");
                lblFName.Attributes.Add("runat", "server");
                lblFName.Text = fFName + " ";

                lblLName.Attributes.Add("class", "tweetName");
                lblLName.Attributes.Add("runat", "server");
                lblLName.Text = fLName + "  ";

                lnkUsername.Attributes.Add("class", "tweetUser");
                lnkUsername.Attributes.Add("runat", "server");
                lnkUsername.Click += UpdateProfileSession;
                lnkUsername.Text = "@" + fUsername;

                followerDiv.Controls.Add(lblFName);
                followerDiv.Controls.Add(lblLName);
                followerDiv.Controls.Add(lnkUsername);

                int blockCheck = objmyDAl.CheckIsBlocked(fUsername, currentUser);

                if (blockCheck != 1 && (fUsername != currentUser))
                {
                    if (privacyType == 0)
                    {
                        btnFollow = new Button();
                        btnFollow.Attributes.Add("class", "btn btn-default btnProf");
                        btnFollow.Attributes.Add("runat", "server");
                        btnFollow.Click += FollowUser_Click;
                        btnFollow.Text = "Follow";

                        followerDiv.Controls.Add(btnFollow);
                    }
                    else if (privacyType == 1)
                    {
                        btnUnfollow = new Button();
                        btnUnfollow.Attributes.Add("class", "btn btn-default btnProf");
                        btnUnfollow.Attributes.Add("runat", "server");
                        btnUnfollow.Click += UnfollowUser_Click;
                        btnUnfollow.Text = "Unfollow";

                        followerDiv.Controls.Add(btnUnfollow);
                    }
                }

                profBox.Controls.Add(followerDiv);

                i++;
            }

        }

        protected void UpdateProfileSession(object sender, EventArgs e)
        {
            String username = (sender as LinkButton).Text;
            username = username.Substring(1, username.Length - 1);

            Session["UserProfile"] = username;
            Session["ProfileViewType"] = "TweetView";
            Response.Redirect("Profile.aspx");
        }
    }
}