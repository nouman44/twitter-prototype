<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="TweeterConnect.SignUp" %>

<!DOCTYPE html>

<!DOCTYPE HTML>
<html>
	<head>
	        <title>Sign Up for Tweeter</title>
		    <link rel="stylesheet" href="Styles/Style.css" />
	        <link rel="shortcut icon" type="image/x-icon" href="Images/Tweeter.ico" />
	    </head>
	<body style = "background-image:url(Images/Background.gif); background-size: cover; background-position:left">
            <form id="form1" runat="server" method="post" style="display:block; top: 4px; left: 0px;">z
		    	    <h1 class="auto-style1">Sign Up for &nbsp<img src="Images/Label-White.png" alt="Tweeter" width="250"/></h1>
                    <br/>
                    <asp:TextBox ID="firstname" runat="server" placeholder="First Name" required="true" MaxLength="15"></asp:TextBox><br />
		            <asp:TextBox ID="lastname" runat="server" placeholder="Last Name" required="true" MaxLength="15"></asp:TextBox><br />
                    <asp:TextBox ID="username" runat="server" placeholder="User Name" required="true" MaxLength="15"></asp:TextBox>
                    <asp:Label ID="userMsg" runat="server" ></asp:Label><br /><br />
		            <asp:TextBox ID="email" runat="server" placeholder="Email ID" required="true" type="email" MaxLength="50"></asp:TextBox><br />
		            <asp:TextBox ID="password" runat="server" type="password" placeholder="Password" required="true" MaxLength="10"></asp:TextBox><br />
		            <asp:Calendar ID="bDate" required="true" runat="server"></asp:Calendar><br />
                    <label>Gender: </label>
                    <asp:RadioButton ID="rdMale" GroupName="Gender" runat="server" Text="M"/>
                    <asp:RadioButton ID="rdFemale" GroupName="Gender" runat="server" Text="F"/><br/><br/>
                    <asp:TextBox ID="city" runat="server" placeholder="City" MaxLength="40" required="true"></asp:TextBox><br />
		            <asp:TextBox ID="country" runat="server" placeholder="Country" MaxLength="40" required="true"></asp:TextBox><br />
                    <asp:TextBox ID="phone" runat="server" placeholder="Phone Number" MaxLength="13" pattern="[\+]\d{12}" title="e.g. +923404718228" required="true"></asp:TextBox><br />
                    <asp:button type="submit" runat="server" value="Sign Up" Text="Sign Up" Width="289px" OnClick="SignUp_Click"/>
			    </form>
            
		    <footer id="footer">
		    	    <ul class="copyright">
		    		        <li>&copy; Hamza, Nouman & Nouman <script>document.write(new Date().getFullYear())</script></li>
		    	    </ul>
		    </footer>
	    </body>
</html>
