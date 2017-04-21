<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameBoardUI.aspx.cs" Inherits="Jeopardy_Game.GameBoardUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class=>
    
        <asp:Table ID="tblGameboard" runat="server" BackColor="#0719B8" BorderColor="Black" BorderStyle="Solid" BorderWidth="5px" CellPadding="10" CellSpacing="10" Font-Bold="True" Font-Names="Arial Black" Font-Size="XX-Large" ForeColor="#F3B94E" GridLines="Both">
        </asp:Table>
   
        <div class="info_area">
        <asp:Label ID="lblGameInfo" runat="server" Text="" CssClass="Label"></asp:Label>
            </div>
    
    </div>
    </form>
</body>
</html>
