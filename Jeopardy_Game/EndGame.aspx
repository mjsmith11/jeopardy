<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndGame.aspx.cs" Inherits="Jeopardy_Game.EndGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
    <title>Software Engineering Jeopardy</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="display">
        <asp:Label ID="lblHeading" runat="server" Text="Game Over" CssClass="pageHeading"></asp:Label>
        <asp:Label ID="lblScore" runat="server" Text="" CssClass="prompt"></asp:Label><br /><br /><br />
        <asp:Label ID="lblHighScores" runat="server" Text="High Scores" CssClass="prompt"></asp:Label><br />
        <div id="divHighScoreArea" runat="server"></div>
        <asp:Button ID="btnPlayAgain" runat="server" Text="Play Again?" OnClick="btnPlayAgain_Click" /><br /><br />
    </div>
    </form>
</body>
</html>
