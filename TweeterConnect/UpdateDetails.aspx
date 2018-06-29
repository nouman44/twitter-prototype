<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="UpdateDetails.aspx.cs" Inherits="TweeterConnect.UpdateDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Update Details</title>
    <link rel="stylesheet" href="Styles/ViewDetailsStyle.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="verticalBox">
        <h2>Update Details</h2>
        <hr />
        <asp:LinkButton runat="server" class="btn btn-default" OnClick="ShowUserDetails">Load Default Values</asp:LinkButton>
        <br /><br />
        <asp:LinkButton ID="lnkUsername" runat="server" OnClick="UpdateProfileSession" CssClass="username"></asp:LinkButton>
        <br /><br />
        <asp:Label ID="tagFName" runat="server" Text="Label" CssClass="details">First Name: </asp:Label>
        <asp:TextBox ID="txtFName" runat="server" required="true" MaxLength="15"></asp:TextBox>
        <br /><br />
        <asp:Label ID="tagLName" runat="server" Text="Label" CssClass="details">Last Name: </asp:Label>
        <asp:TextBox ID="txtLName" runat="server" required="true" MaxLength="15"></asp:TextBox>
        <br /><br />
        <asp:Label ID="tagGender" runat="server" Text="Label" CssClass="details">Gender:</asp:Label>
        <asp:RadioButton ID="rdMale" GroupName="Gender" runat="server" Text="M"/>
        <asp:RadioButton ID="rdFemale" GroupName="Gender" runat="server" Text="F"/>
        <br /><br />
        <asp:Label ID="tagBDate" runat="server" Text="Label" CssClass="details">Date of Birth: </asp:Label>
        <br />
        <asp:Calendar ID="bDate" required="true" runat="server"></asp:Calendar><br />
        <br /><br />
        <asp:Label ID="tagPhone" runat="server" Text="Label" CssClass="details">Phone No: </asp:Label>
        <asp:TextBox ID="txtPhone" runat="server" MaxLength="13" pattern="[\+]\d{12}" title="e.g. +923404718228" required="true"></asp:TextBox>
        <br /><br />
        <asp:Label ID="tagCity" runat="server" Text="Label" CssClass="details">City: </asp:Label>
        <asp:TextBox ID="txtCity" runat="server" MaxLength="40" required="true"></asp:TextBox>
        <br /><br />
        <asp:Label ID="tagCountry" runat="server" Text="Label" CssClass="details">Country: </asp:Label>
        <asp:TextBox ID="txtCountry" runat="server" MaxLength="40" required="true"></asp:TextBox>
        <br /><br />
        <label>Privacy: </label>
        <asp:RadioButton ID="rdPrivate" GroupName="Privacy" runat="server" Text="Private"/>
        <asp:RadioButton ID="rdPublic" GroupName="Privacy" runat="server" Text="Public"/>
        <br /><br />
        <asp:button runat="server" Text="Update" class="btn btn-default" OnClick="UpdateUserDetails"/>
        <hr />
    </div>
</asp:Content>
