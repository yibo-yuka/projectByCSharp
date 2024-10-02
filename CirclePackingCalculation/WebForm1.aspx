<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CirclePackingCalculation.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>圖片上傳系統</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>圖片上傳系統
            </h2>
            <hr/>
            上傳圖片：<asp:FileUpload ID="FileUpload1" runat="server" /><asp:Label ID="lblChoose" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label><br/>
            <asp:Button ID="btnUpload" runat="server" Text="上傳" OnClick="btnUpload_Click"/>
            <asp:Label ID="lblSuccess" runat="server" Text="上傳成功!" Visible="false" ForeColor="Blue"></asp:Label><asp:Label ID="lblFailed" runat="server" Text="上傳失敗!" Visible="false" ForeColor="Red"></asp:Label>
            <hr />
            <p>已上傳圖片清單</p>
            <asp:Table ID="imgTable" runat="server"></asp:Table>
        </div>
    </form>
</body>
</html>
