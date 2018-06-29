<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TweeterConnect.Login" %>

<!DOCTYPE HTML>
<html>
	<head>
	        <title>Welcome to Tweeter</title>
		    <link rel="stylesheet" href="Styles/Style.css" />
	        <link rel="shortcut icon" type="image/x-icon" href="Images/Tweeter.ico" />
	    </head>
	<body style = "background-image:url(Images/Background.gif); background-size: cover; background-position:left">
            <h1>Welcome</h1>
            
		    <header id="header">
		    	    <h1><img src="Images/Label-White.png" alt="Tweeter" width="330"/></h1>
		    	    <p>&nbsp Tweet your life.</p>
		        </header>
            
		    <form id="form1" runat="server" method="post" style="display:block">
		    	    &nbsp<br />
                    <asp:TextBox ID="username" runat="server" placeholder="Username"></asp:TextBox>
                    <asp:Label ID="userMsg" runat="server" ></asp:Label><br /><br />
                    <asp:TextBox ID="password" runat="server" type="password" placeholder="Password"></asp:TextBox>
                    <asp:Label ID="passMsg" runat="server" ></asp:Label><br /><br />
		            <asp:button runat="server" value="Log In" Text="Log In" OnClick="LoginButton_Click"/>
				    <asp:button runat="server" value="Sign Up" Text="Sign Up" OnClick="SignUp_Click" />
            </form>
            
		    <footer id="footer">
		    	    <ul class="copyright">
		    		        <li>&copy; Hamza, Nauman & Nouman <script>document.write(new Date().getFullYear())</script></li>
		    	        </ul>
		        </footer>
	    </body>
</html>