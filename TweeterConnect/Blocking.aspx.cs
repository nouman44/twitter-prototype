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
    public partial class Blocking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserProfile"] = Session["CurrentUser"];
            ShowBlockedUsers();
        }

        protected void UnblockUser_Click(object sender, EventArgs e)
        {
            IEnumerator myEnum = (sender as Button).Parent.Controls.GetEnumerator();
            Label myLabel;
            String bUsername = "", currentUser = Session["CurrentUser"].ToString();
            int i = 0;

            while (myEnum.MoveNext())
            {
                if (myEnum.Current is Label)
                {
                    if (i == 2)
                    {
                        myLabel = (Label)myEnum.Current;
                        bUsername = myLabel.Text;
                    }
                    i++;
                }
            }

            myDAL objmyDAl = new myDAL();
            bUsername = bUsername.Substring(1, bUsername.Length - 1);

            objmyDAl.UnblockUser(bUsername, currentUser);

            Response.Redirect("Blocking.aspx");
        }

        protected void ShowBlockedUsers()
        {
            myDAL objmyDAl = new myDAL();

            String username = Session["CurrentUser"].ToString();
            DataTable DT = new DataTable();

            objmyDAl.GetBlockedUsers(username, ref DT);

            String bUsername, bFName = "", bLName = "", currentUser = Session["CurrentUser"].ToString();
            int i = 0;

            Label lblFName, lblLName, lblUsername;
            Button btnUnblock;


            foreach (DataRow DR in DT.Rows)
            {
                bUsername = DT.Rows[i]["BlockedName"].ToString();

                objmyDAl.GetName(bUsername, ref bFName, ref bLName);

                System.Web.UI.HtmlControls.HtmlGenericControl blockedDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                blockedDiv.Attributes.Add("class", "blockedBox");

                lblFName = new Label();
                lblLName = new Label();
                lblUsername = new Label();

                lblFName.Attributes.Add("class", "blockName");
                lblFName.Attributes.Add("runat", "server");
                lblFName.Text = bFName + " ";

                lblLName.Attributes.Add("class", "blockName");
                lblLName.Attributes.Add("runat", "server");
                lblLName.Text = bLName + "  ";

                lblUsername.Attributes.Add("class", "blockName");
                lblUsername.Attributes.Add("runat", "server");
                lblUsername.Text = "@" + bUsername;

                blockedDiv.Controls.Add(lblFName);
                blockedDiv.Controls.Add(lblLName);
                blockedDiv.Controls.Add(lblUsername);

                btnUnblock = new Button();
                btnUnblock.Attributes.Add("class", "btn btn-default btnBlock");
                btnUnblock.Attributes.Add("runat", "server");
                btnUnblock.Click += UnblockUser_Click;
                btnUnblock.Text = "Unblock";

                blockedDiv.Controls.Add(btnUnblock);

                blockBox.Controls.Add(blockedDiv);

                i++;
            }
        }

    }
}