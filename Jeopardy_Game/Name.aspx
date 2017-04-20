<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Name.aspx.cs" Inherits="JeoparyGame.Name" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
 
    <form id="form1" runat="server">
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="Yellow" style="z-index: 1; left: 277px; top: 49px; position: absolute; width: 239px; bottom: 580px" Text="Jeopardy Game"></asp:Label>
        
        
        
        
        <br />
     
        <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 116px; top: 184px; position: absolute; width: 162px" Text="Enter Your Name:" Font-Bold="True" Font-Size="Large" ForeColor="Yellow"></asp:Label>
        <asp:TextBox ID="nametxtbox" MaxLength="50"  runat="server" style="z-index: 1; left: 267px; top: 179px; position: absolute; height: 28px; width: 216px;"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" style="z-index: 1; left: 267px; top: 225px; position: absolute; width: 223px; height: 46px;" Text="Submit" OnClick="Button1_Click" BackColor="#9933FF" Font-Bold="True" ForeColor="Yellow" />
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
     
        <asp:Label ID="Label3error" runat="server" Text="" style="z-index: 1; left: 116px;"></asp:Label> 
    </form>
</body>
</html>
