<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ViewDetails.aspx.cs" Inherits="TweeterConnect.ViewDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View Details</title>
    <link rel="stylesheet" href="Styles/ViewDetailsStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="verticalBox">
        <h2>User Details</h2>
        <hr />
        <asp:LinkButton ID="lnkUsername" runat="server" OnClick="UpdateProfileSession" CssClass="username"></asp:LinkButton>
        <br /><br />
        <asp:Label ID="tagFName" runat="server" Text="Label" CssClass="details">First Name: </asp:Label>
        <asp:Label ID="lblFName" runat="server" Text="Label"></asp:Label>
        <br /><br />
        <asp:Label ID="tagLName" runat="server" Text="Label" CssClass="details">Last Name: </asp:Label>
        <asp:Label ID="lblLName" runat="server" Text="Label"></asp:Label>
        <br /><br />
        <asp:Label ID="tagGender" runat="server" Text="Label" CssClass="details">Gender:</asp:Label>
        <asp:Label ID="lblGender" runat="server" Text="Label"></asp:Label>
        <br /><br />
        <asp:Label ID="tagBDate" runat="server" Text="Label" CssClass="details">Date of Birth: </asp:Label>
        <asp:Label ID="lblBDate" runat="server" Text="Label"></asp:Label>
        <br /><br />
        <asp:Label ID="tagEmail" runat="server" Text="Label" CssClass="details">Email: </asp:Label>
        <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
        <br /><br />
        <asp:Label ID="tagPhone" runat="server" Text="Label" CssClass="details">Phone No: </asp:Label>
        <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label>
        <br /><br />
        <asp:Label ID="tagCity" runat="server" Text="Label" CssClass="details">City: </asp:Label>
        <asp:Label ID="lblCity" runat="server" Text="Label"></asp:Label>
        <br /><br />
        <asp:Label ID="tagCountry" runat="server" Text="Label" CssClass="details">Country: </asp:Label>
        <asp:Label ID="lblCountry" runat="server" Text="Label"></asp:Label>
        <hr />
    </div>
</asp:Content>
