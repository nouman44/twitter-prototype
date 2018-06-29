<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TweeterConnect.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Content/bootstrap.css" />
    <link rel="stylesheet" href="Styles/MainPageStyle.css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <nav class="navbar navbar-default">
        <div class="container-fluid">
            <div class="navbar-header">
                <label class="navbar-brand">Tweeter</label>
            </div>
            <ul class="nav navbar-nav">
                <li><a href="#">Home</a></li>
                <li><a href="#">Profile</a></li>
                <li><a href="#">Messages</a></li>
            </ul>
                <div class="navbar-form navbar-left">
                    <asp:TextBox id="txtSearch" class="form-control" placeholder="Search" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" class="btn btn-default" runat="server" Text="Search" />
                </div>
            
            <ul class="nav navbar-nav navbar-right">
            <li><asp:LinkButton runat="server" OnClick="Logout_Click">Logout</asp:LinkButton></li>
            </ul>
        </div>
    </nav>

        <div id="userStats">
            <asp:Label id="txtFName" runat="server">First</asp:Label>
            <asp:Label id="txtLName" runat="server">Last</asp:Label>
            <br /><br />
            <asp:Label id="txtUsername" runat="server">@Username</asp:Label>
            <br />
            <hr />
            <span class="statBlock">
            <label id="lblTweets" for="txtTweets" class="statLabel">TWEETS | </label>
            <asp:Label id="txtTweets" runat="server">10</asp:Label>
            </span>
            <span class="statBlock">
            <label id="lblFollowing" for="txtFollowing" class="statLabel">FOLLOWING | </label>
            <asp:Label id="txtFollowing" runat="server">20</asp:Label>
            </span>
            <span class="statBlock">
            <label id="lblFollowers" for="txtFollowers" class="statLabel">FOLLOWERS</label>
            <asp:Label id="txtFollowers" runat="server">20</asp:Label>
            </span>
        </div>

        <div id="contentBox" runat="server">
            <div class="verticalBox">
                <label>Post a Tweet</label>
                <br />
                <asp:TextBox ID="txtPost" class="form-control" TextMode="MultiLine" placeholder="What's happening?" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnPost" class="btn btn-default" runat="server" Text="Tweet" OnClick="PostTweet_Click"/>
            </div>
            <div class="verticalBox">
                <asp:Label class="tweetName" runat="server">First</asp:Label>
                <asp:Label class="tweetName" runat="server">Last &nbsp</asp:Label>
                <asp:LinkButton class="tweetUser" runat="server">@Username</asp:LinkButton>
                <hr />
                <p>Fun times following the #EmporioArmani #AutomatRadio music truck around Milan during the Design Week. Stay tuned for more upcoming locations on automatradio.com</p>
                <asp:Label class="tweetDateTime" runat="server">11/12/17 at 4:00</asp:Label>
            </div>
        </div>

         <div id="trending">
            <label>Trending Tweets</label>
             <hr />
         </div>
    </form>
</body>
</html>
