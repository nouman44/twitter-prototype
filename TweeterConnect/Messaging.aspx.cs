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
    public partial class Messaging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowConversations();
            ShowMessages();
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

        protected void SendMessage(object sender, EventArgs e)
        {
            myDAL objmyDAl = new myDAL();
            String currentUser = Session["CurrentUser"].ToString(), otherUser = Session["Message"].ToString();
            String body = txtMessage.Text;
            DateTime time = DateTime.Now;

            if (otherUser != "None")
            {

                int blockCheck = objmyDAl.CheckIsBlocked(otherUser, currentUser);

                if (blockCheck == 1)
                {
                    Response.Write("<script>alert('You cannot send a message to the user!');</script>");
                }
                else if (body.Length == 0)
                {
                    Response.Write("<script>alert('Please enter something in the message!');</script>");
                }
                else
                {
                    objmyDAl.CreateMessage(currentUser, otherUser, body, time);
                    Response.Redirect("Messaging.aspx");
                }
            }
            else
            {
                Response.Write("<script>alert('You need to select a conversation!');</script>");
            }
        }

        protected void UpdateMessageSession(object sender, EventArgs e)
        {
            String username = (sender as LinkButton).Text;
            username = username.Substring(1, username.Length - 1);

            Session["Message"] = username;
            Response.Redirect("Messaging.aspx");
        }

        protected void ShowMessages()
        {
            if (Session["Message"].ToString() != "None") 
            {
                myDAL objmyDAl = new myDAL();
                DataTable DT = new DataTable();

                String currentUser = Session["CurrentUser"].ToString();
                String secUser = Session["Message"].ToString();
                objmyDAl.GetMessages(currentUser, secUser, ref DT);
                int i = 0, index;

                String sender, receiver, fName = "", lName = "", msgTime, msgBody;
                Label lblFName, lblLName, lblBody, lblTime;

                foreach (DataRow DR in DT.Rows)
                {
                    sender = DT.Rows[i]["SenderName"].ToString();
                    receiver = DT.Rows[i]["ReceiverName"].ToString();
                    msgBody = DT.Rows[i]["Body"].ToString();
                    msgTime = DT.Rows[i]["MessageTime"].ToString();

                    index = msgTime.IndexOf('.');
                    msgTime = msgTime.Substring(0, index);
                    
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                    lblFName = new Label();
                    lblLName = new Label();
                    lblBody = new Label();
                    lblTime = new Label();

                    objmyDAl.GetName(sender, ref fName, ref lName);

                    lblFName.Attributes.Add("class", "msgDetails");
                    lblFName.Attributes.Add("runat", "server");
                    lblFName.Text = fName + " ";

                    lblLName.Attributes.Add("class", "msgDetails");
                    lblLName.Attributes.Add("runat", "server");
                    lblLName.Text = lName;

                    lblBody.Attributes.Add("runat", "server");
                    lblBody.Text = msgBody;

                    lblTime.Attributes.Add("class", "msgDetails");
                    lblTime.Attributes.Add("runat", "server");
                    lblTime.Text = "At " + msgTime;

                    lblLName.Attributes.Add("class", "msgDetails");
                    lblLName.Attributes.Add("runat", "server");
                    lblLName.Text = lName;

                    if (sender == currentUser)
                    {
                        lblBody.Attributes.Add("class", "userMsgBody");
                        createDiv.Attributes.Add("class", "userMsg");
                    }
                    else
                    {
                        lblBody.Attributes.Add("class", "otherMsgBody");
                        createDiv.Attributes.Add("class", "otherMsg");
                    }

                    System.Web.UI.HtmlControls.HtmlGenericControl clearDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    clearDiv.Attributes.Add("class", "clear");

                    createDiv.Controls.Add(lblFName);
                    createDiv.Controls.Add(lblLName);
                    createDiv.Controls.Add(new LiteralControl("<br />"));
                    createDiv.Controls.Add(lblBody);
                    createDiv.Controls.Add(new LiteralControl("<br />"));
                    createDiv.Controls.Add(lblTime);

                    messageBox.Controls.Add(createDiv);
                    messageBox.Controls.Add(clearDiv);

                    i++;
                }

            }
        }

        protected void ShowConversations()
        {
            myDAL objmyDAl = new myDAL();
            DataTable DT = new DataTable();

            String currentUser = Session["CurrentUser"].ToString();
            objmyDAl.GetConvos(currentUser, ref DT);
            int i = 0;

            LinkButton lnkUsername;
            String sender, receiver, fName = "", lName = "";
            Label lblFName, lblLName;

            foreach (DataRow DR in DT.Rows)
            {
                sender = DT.Rows[i]["SenderName"].ToString();
                receiver = DT.Rows[i]["ReceiverName"].ToString();

                System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createDiv.Attributes.Add("class", "convoUser");
                lnkUsername = new LinkButton();
                lblFName = new Label();
                lblLName = new Label();

                if (sender != currentUser) 
                {
                    lnkUsername.Text = "@" + sender;
                    objmyDAl.GetName(sender, ref fName, ref lName);
                }
                else
                {
                    lnkUsername.Text = "@" + receiver;
                    objmyDAl.GetName(receiver, ref fName, ref lName);
                }

                lblFName.Attributes.Add("class", "name");
                lblFName.Attributes.Add("runat", "server");
                lblFName.Text = fName + " ";

                lblLName.Attributes.Add("class", "name");
                lblLName.Attributes.Add("runat", "server");
                lblLName.Text = lName;

                lnkUsername.Attributes.Add("class", "userName");
                lnkUsername.Attributes.Add("runtat", "server");
                lnkUsername.Click += UpdateMessageSession;

                createDiv.Controls.Add(lblFName);
                createDiv.Controls.Add(lblLName);
                createDiv.Controls.Add(new LiteralControl("<br />"));
                createDiv.Controls.Add(lnkUsername);

                convos.Controls.Add(createDiv);
                convos.Controls.Add(new LiteralControl("<hr />"));

                i++;
            }
        }
    }
}