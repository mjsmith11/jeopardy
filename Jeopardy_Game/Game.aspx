<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Game.aspx.cs" Inherits="JeoparyGame.Game" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

          <asp:ScriptManager ID="ScriptManager1" runat="server">
      
                </asp:ScriptManager>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
       <ContentTemplate>
      
               
      
              <asp:Label ID="timelbl" runat="server" style="z-index: 1; left: 511px; top: 450px; position: absolute; width: 129px" Font-Bold="True" Font-Size="Medium" ForeColor="Yellow"></asp:Label>
     
            </ContentTemplate>
        
          
        </asp:UpdatePanel>
              <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick" >
        </asp:Timer>
       
         
        
        
       <img src="image/bluebk.jpg" height="1080" width="1920" />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="Yellow" style="z-index: 1; left: 217px; top: 26px; position: absolute; width: 419px; height: 37px" Text="Jeopardy Game "></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" Font-Strikeout="False" ForeColor="Yellow" style="z-index: 1; left: 225px; top: 120px; position: absolute; width: 92px" Text="Round :"></asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Yellow" style="z-index: 1; left: 459px; top: 115px; position: absolute; width: 91px" Text="Amount:"></asp:Label>
        <asp:Button ID="Button1" runat="server" BackColor="#9933FF" Font-Bold="True" ForeColor="Yellow" style="z-index: 1; left: 237px; top: 445px; position: absolute; width: 148px; height: 33px" Text="Submit" />
  
    <div>
    
    </div>
        <asp:Label ID="questionLbl" runat="server" style="z-index: 1; left: 224px; top: 181px; position: absolute; width: 819px" Text="Q. WHAT IS OUR NATIONAL GAME?" Font-Bold="True" Font-Size="Large" ForeColor="#003300"></asp:Label>
        <asp:RadioButton ID="RadioButton1" runat="server" style="z-index: 1; left: 224px; top: 229px; position: absolute" Font-Bold="True" Font-Size="Large" ForeColor="#333300" Text="A. Hockey" />
        <p>
            <asp:RadioButton ID="RadioButton2" runat="server" style="z-index: 1; left: 224px; top: 279px; position: absolute; bottom: 385px" Font-Bold="True" Font-Size="Large" ForeColor="#333300" Text="B.Cricket" />
                  </p>
        <p>
            <asp:RadioButton ID="RadioButton3" runat="server" style="z-index: 1; left: 224px; top: 335px; position: absolute" Font-Bold="True" Font-Size="Large" ForeColor="#333300" Text="C.Football" />
        </p>
        <p>
            <asp:RadioButton ID="RadioButton4" runat="server" style="z-index: 1; left: 224px; top: 392px; position: absolute" Font-Bold="True" Font-Size="Large" ForeColor="#333300" Text="D.Baseball" />
        
        </p>
    </form>
</body>
</html>
