<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPages/Usersmp.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="MyWebApp.UserProfile" %>


<asp:Content ID="up" ContentPlaceHolderID="usercontent" runat="server">


    <div class="upleft">
        <asp:Button Text="EDIT Profile" runat="server" CssClass="btn1" OnClick="btnClick_Edit"/>
        <asp:Button  Text="Update Image" runat="server" CssClass="btn1" OnClick="btnClick_Image"/>
        <asp:Button Text ="Change Password" runat="server" CssClass="btn1" OnClick="btnClick_password"/>
    </div>


    <div>
        
      <h1 class="title">USER DATA</h1>
   
      <div class="upform">
        <div class="form-group">
        <label>First Name</label>
        <asp:TextBox ID="txtFn" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>
        
        <div class="form-group"> 
        <label>Last Name</label>
        <asp:TextBox ID="txtLn" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

        <div class="form-group">
        <label>DisplayName</label>
        <asp:TextBox ID="txtDn" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

      </div>
        
      <div class="upform">

        <div class="form-group">
        <label>Date Of Birth</label>
        <asp:TextBox ID="txtDob" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

        <div class="form-group">
        <label>Gender</label>
        <asp:TextBox ID="txtGender" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

        <div class="form-group">
        <label >Age</label>
        <asp:TextBox ID="txtAge" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>
            
     </div>

      <div class="upform">

        <div class="form-group">
        <label >Address</label>
        <asp:TextBox ID="txtAddress" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

        <div class="form-group">
        <label >City</label>
        <asp:TextBox ID="txtCity" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>
        
        <div class="form-group">
        <label>State</label>
        <asp:TextBox ID="txtState" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

      </div>

      <div class="upform">

        <div class="form-group">
        <label>ZipCode</label>
        <asp:TextBox ID="txtZip" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

        <div class="form-group">
        <label>Phone</label>
        <asp:TextBox ID="txtPhone" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

        <div class="form-group">
        <label>Mobile</label>
        <asp:TextBox ID="txtMobile" runat="server" ReadOnly="true" CssClass="hide" ClientIDMode="Static"/>
        </div>

       </div>  
      
        <div class="line">
            <asp:Button ID="btnCancel" Text="CANCEL" runat="server" OnClick="btn_Cancel" CssClass="btn1" Visible ="false" ClientIDMode="Static"/>
            <asp:Button ID="btnUpdate" Text="UPDATE" runat="server" OnClick="btn_Update" CssClass="btn1" Visible="false" ClientIDMode="Static" OnClientClick="if (! $('#formup').valid()) return false;" />
        </div>

    </div>
</asp:Content>
