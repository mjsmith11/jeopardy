<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionUI.aspx.cs" Inherits="Jeopardy_Game.QuestionUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
    <title>Software Engineering Jeopardy</title>
</head>
<body>
    <form id="form1" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <div hidden="hidden" id="divAudio" runat="server"></div>
    <div class="play_area">
        <div id="divHeadings" class="headings" runat="server">
            <asp:Label ID="lblCategory" runat="server" Text="" CssClass="question_heading"></asp:Label>
            <asp:Label ID="lblValue" runat="server" Text="" CssClass="question_heading"></asp:Label>
            <br />
            <br />
        </div>
        <div id="divQuestion" class="question_material" runat="server">
            <asp:Label ID="lblQuestionText" runat="server" Text="" CssClass="question_text"></asp:Label>
            <div id="divPicture" runat="server">
                <asp:Image ID="imgQuestion" runat="server" />
                <br />
                <br />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div id="divAnswers" class="answer_choices" runat="server">
            <asp:Button ID="btnAnswer1" runat="server" Text="" OnClick="btnAnswer1_Click" CssClass="answer_choice"/>
            <br />
            <asp:Button ID="btnAnswer2" runat="server" Text="" OnClick="btnAnswer2_Click" CssClass="answer_choice"/>
            <br />
            <asp:Button ID="btnAnswer3" runat="server" Text="" OnClick="btnAnswer3_Click" CssClass="answer_choice"/>
            <br />
            <asp:Button ID="btnAnswer4" runat="server" Text="" OnClick="btnAnswer4_Click" CssClass="answer_choice"/>
        </div>
        <br />
        <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="bigBtn" OnClick="btnContinue_Click" /><br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer2" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblTime" runat="server" Text="" CssClass="prompt"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick"></asp:Timer>
                <asp:Timer ID="Timer2" runat="server" Interval="5" OnTick="Timer2_Tick"></asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <br />
    </div>
    </form>
</body>
</html>
