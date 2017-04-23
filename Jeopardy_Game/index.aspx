<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="JeoparyGame.Name" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
    <title>Software Engineering Jeopardy</title>
</head>
<body>
 
    <form id="form1" runat="server">
        <div class="nameArea">
        <p>
            <asp:Label ID="Label2" runat="server" Text="Software Engineering Jeopardy" CssClass="pageHeading"></asp:Label>     
        </p>
        <p>
        <asp:Label ID="Label1" runat="server" Text="Enter Your Name To Begin" CssClass="prompt"></asp:Label><br /><br />
        <asp:TextBox ID="nametxtbox" MaxLength="50"  runat="server"></asp:TextBox><br />
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label> 
        </p>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/><br /><br />
        </div>     
        
    </form>
</body>
</html>
