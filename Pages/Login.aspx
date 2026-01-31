<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyWebApp.Login" %>

<asp:Content ID="log" ContentPlaceHolderID="MainContent" runat="server">

   <form id="form1" runat="server">
  
    <h2>Login</h2>

    <div class="form-group">
         <asp:Label Text="username" runat="server"/>
         <asp:TextBox ID="txtUsername" runat="server" Required="required"/>
    </div>

    <div class="form-group">
         <asp:Label Text="Password" runat="server" />
         <asp:TextBox ID="textPassword" textmode="Password" runat="server" Required="required"/>
    </div>

    <div class="form-group">
         <asp:Button ID="bntcancel" Text="cancel" OnClick="btnCancel_Click" OnClientClick="this.form.reset(); return false;" runat="server" CssClass="btn" Style ="background:red;"/>
         <asp:Button ID="btnlogin" Text="Login" OnClick="btnLogin_Click" runat="server" CssClass="btn" />
        <br />
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Visible="false" CssClass="alert alert-danger"></asp:Label>
    </div>
    </form>
</asp:Content>