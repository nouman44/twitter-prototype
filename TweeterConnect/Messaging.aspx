<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Messaging.aspx.cs" Inherits="TweeterConnect.Messaging" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Messages</title>
        <link rel="stylesheet" href="Content/bootstrap.css" />
        <link rel="stylesheet" href="Styles/MessagingStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <nav class="navbar navbar-default">
            <div class="container-fluid">
                <div class="navbar-header">
                    <label class="navbar-brand">Tweeter</label>
                </div>
                <ul class="nav navbar-nav">
                    <li><asp:LinkButton runat="server" OnClick="NavHome_Click">Home</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" OnClick="NavProfile_Click">Profile</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" OnClick="NavMessages_Click">Messages</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" OnClick="NavBlocking_Click">Blocking</asp:LinkButton></li>
                </ul>
                    <div class="navbar-form navbar-left">
                        <asp:TextBox id="txtSearch" class="form-control" placeholder="Search" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" class="btn btn-default" runat="server" Text="Search" OnClick="NavSearch_Click" />
                    </div>
            
                <ul class="nav navbar-nav navbar-right">
                <li><asp:LinkButton runat="server" OnClick="Logout_Click">Logout</asp:LinkButton></li>
                </ul>
            </div>
        </nav>

        <div id="convos" runat="server">
            <h3>Conversations</h3>
            <hr />
        </div>

        <div id="section">
            <div id="messageBox" runat="server">

            </div>
            <div id="postMsg">
                <label>Write a message</label>
                <br />
                <asp:TextBox ID="txtMessage" class="form-control" TextMode="MultiLine" width="400" height="100" MaxLength="140" placeholder="Write something..." runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnMessage" class="btn btn-default" runat="server" Text="Send" OnClick="SendMessage"/>
            </div>
        </div>>

    </div>
    </form>
</body>
</html>
