<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Jeopardy_Game.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
    <title>Software Engineering Jeopardy</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="display">
        <asp:Label ID="lblShrug" runat="server" Text="¯\_(ツ)_/¯" CssClass="pageHeading"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" Text="An Unexpected Error Occured" CssClass="pageHeading"></asp:Label>
        <asp:Button ID="btnHome" runat="server" Text="Start a new Game" OnClick="btnHome_Click"/><br /><br />
    </div>
    </form>
</body>
</html>
