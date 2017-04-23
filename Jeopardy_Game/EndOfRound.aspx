<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndOfRound.aspx.cs" Inherits="Jeopardy_Game.EndOfRound" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="roundTransition">
    
        <asp:Label ID="lblHead" runat="server" Text="Round Complete" CssClass="pageHeading"></asp:Label>
        <asp:Label ID="lblScore" runat="server" Text="" CssClass="prompt"></asp:Label><br /><br />
        <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" /><br /><br />
    </div>
    </form>
</body>
</html>
