<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TweeterConnect.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Search Results</title>
    <link rel="stylesheet" href="Styles/SearchStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <div id="searchResults" runat="server">

        </div>
</asp:Content>
