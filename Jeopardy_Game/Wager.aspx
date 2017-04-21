<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wager.aspx.cs" Inherits="Jeopardy_Game.Wager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
    <title></title>
    
</head>
<body>
    
    
    <form id="form1" runat="server">
        <div hidden="hidden" id="divAudio" runat="server"></div>
    <div class="play_area">
    
        <br />
    
        <asp:Image ID="imgDailyDouble" runat="server" />
    
        <br />

        <div id="wager_area">
            <asp:Label ID="lblScore" runat="server" Text="" CssClass="Label"></asp:Label>
            <br />
            <asp:Label ID="lblWager" runat="server" Text="Enter Wager: " CssClass="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tbxWager" runat="server" Width="73px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            <br />
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
    
    </div>
    </form>
</body>
</html>
