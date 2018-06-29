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
    public partial class AdminPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGrid();
            Message.Text = "You can delete users from the Tweeter";
        }

        public void loadGrid()
        {
            myDAL objDAL = new myDAL();
            editUsers.DataSource = objDAL.GetAllUsers();
            editUsers.DataBind();
        }

        protected void RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = editUsers.Rows[e.RowIndex];

            String Username = row.Cells[1].Text;

            myDAL objDAL = new myDAL();

            if (objDAL.DeleteUser(Username) == 1)
            {
                Message.Text = "User \"" + Username + "\" Deleted";
                loadGrid();
            }
            else
                Message.Text = "Error Deleting User!";
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}