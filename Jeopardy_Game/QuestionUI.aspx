<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionUI.aspx.cs" Inherits="Jeopardy_Game.QuestionUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="jeopardy.css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
        <div id="divAnswers" class="answer_choices" runat="server">
            <asp:Button ID="btnAnswer1" runat="server" Text="" OnClick="btnAnswer1_Click" CssClass="answer_choice"/>
            <br />
            <asp:Button ID="btnAnswer2" runat="server" Text="" OnClick="btnAnswer2_Click" CssClass="answer_choice"/>
            <br />
            <asp:Button ID="btnAnswer3" runat="server" Text="" OnClick="btnAnswer3_Click" CssClass="answer_choice"/>
            <br />
            <asp:Button ID="btnAnswer4" runat="server" Text="" OnClick="btnAnswer4_Click" CssClass="answer_choice"/>
        </div>
    </div>
    </form>
</body>
</html>
