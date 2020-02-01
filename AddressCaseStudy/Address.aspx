<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Address.aspx.cs" Inherits="AddressCaseStudy.Address" %>

<!DOCTYPE html>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAN62dHhzKgfH4Tf3GalKzh69OXCSIM0qo&sensor=false&libraries=places"></script>
<script src="Assets/main.js"></script>
<link href="Assets/style.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="destination">Enter Address</label><br />
            <input type="text" runat="server" id="destination"><br /><br />
            <div id="topFive" runat="server" class="location"></div><br />
            <asp:button runat="server" id="submitaddress" OnClick="submitaddress_Click" Text="Submit"></asp:button>
        </div>
    </form>
</body>


</html>