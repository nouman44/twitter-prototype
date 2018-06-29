<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Blocking.aspx.cs" Inherits="TweeterConnect.Blocking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Blocking</title>
    <link rel="stylesheet" href="Styles/BlockingStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="blockBox" runat="server">
        <div class="verticalBox">
            <h3>Blocked Users</h3>
            <hr />
        </div>
    </div>
</asp:Content>
