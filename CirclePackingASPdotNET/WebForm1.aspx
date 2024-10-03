<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CirclePackingASPdotNET.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="Diameter" runat="server" Text="直徑"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Dia_text" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Slice" runat="server" Text="間隙"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="slice_text" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Inward" runat="server" Text="內縮"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="inward_text" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <asp:Button ID="btnDraw" runat="server" Text="繪製" OnClick="btnDraw_Click"/>
            <asp:Table ID="imgTable" runat="server"></asp:Table>
        </div>
    </form>
</body>
</html>
