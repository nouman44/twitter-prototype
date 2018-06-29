<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="TweeterConnect.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" href="Styles/HomePageStyle.css" />

    <script src="Scripts/jquery-3.1.1.js"></script>

    <script type="text/javascript">
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="tweetBox" runat="server">
        <div class="verticalBox">
            <label>Post a Tweet</label>
            <br />
            <asp:TextBox ID="txtPost" class="form-control" TextMode="MultiLine" width="400" height="100" MaxLength="140" placeholder="What's happening?" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnPost" class="btn btn-default" runat="server" Text="Tweet" OnClick="PostTweet_Click"/>
        </div>
    </div>
</asp:Content>
