<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameBoardUI.aspx.cs" Inherits="Jeopardy_Game.GameBoardUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Software Engineering Jeopardy</title>
    <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Table ID="tblGameboard" runat="server" BackColor="#0719B8" BorderColor="Black" BorderStyle="Solid" BorderWidth="5px" CellPadding="10" CellSpacing="10" Font-Bold="True" Font-Names="Arial Black" Font-Size="XX-Large" ForeColor="#F3B94E" GridLines="Both">
        </asp:Table>
   <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="info_area">
        <asp:Label ID="lblGameInfo" runat="server" Text="" CssClass="Label"></asp:Label><br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblRoundInfo" runat="server" Text="" CssClass="Label"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
    
    </div>
        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
