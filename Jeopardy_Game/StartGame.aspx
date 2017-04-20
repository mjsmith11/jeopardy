<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartGame.aspx.cs" Inherits="JeoparyGame.StartGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body       >
 
  <img src="image/bluebk.jpg" height="1080" width="1920" />
 
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Font-Underline="False" ForeColor="Yellow" style="z-index: 1; left: 638px; top: 166px; position: absolute; width: 323px; height: 37px" Text="Jeopary Game!"></asp:Label>
        
  
    <div>
        <%--
    <asp:Image ID="Image1" runat="server" Height="715px" Width="1632px" ImageUrl="C:\Users\asad\Documents\Visual Studio 2015\Projects\JeoparyGame\JeoparyGame\image" />
        <img src="\image\960.jpg" alt="Sample Photo" />
      style="background-image:url(image\bk.jpg)
  --%>


    </div>
      
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
   
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
            <asp:Button ID="Button1" runat="server" Text="Play" style="z-index: 1; left: 597px; top: 336px; position: absolute; width: 302px; height: 73px; margin-bottom: 0px;" OnClick="Button1_Click" BackColor="#9933FF" Font-Bold="True" Font-Size="XX-Large" ForeColor="Yellow"/>
        </form>
</body>
</html>
