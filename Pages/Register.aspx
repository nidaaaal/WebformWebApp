<%@ Page Language="C#" Async="true" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.Master" CodeBehind="Register.aspx.cs" Inherits="MyWebApp.Register" %>

<asp:Content ID="reg" ContentPlaceHolderID="MainContent" runat="server">

        <form id="form2" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <h3>Create Your Account</h3>
                
                <div class="form-group">
                    <label>Email / Phone</label>
                    <asp:TextBox ID ="txtusername" runat="server" ClientIDMode="Static"/>
                </div>

                <div class="form-group">
                    <label>Password</label>
                    <asp:TextBox ID ="password" TextMode="Password" runat="server" ClientIDMode="Static"/>
                </div>

                <div class="form-group">
                    <label>Confirm Password</label>
                    <asp:TextBox ID ="cnfrmpass" TextMode="Password" runat="server" ClientIDMode="Static"/>
                </div>

                <h3>Personal Information</h3>
                
                <div class="form-group">
                    <label>First Name</label>
                    <asp:TextBox ID ="fn" runat="server" ClientIDMode="Static"/>
                </div>

                <div class="form-group">
                    <label>Last Name</label>
                    <asp:TextBox ID ="ln" runat="server" ClientIDMode="Static"/>
                </div>

                <div class="form-group">
                    <label>Display Name</label>
                    <asp:TextBox ID ="dn" runat="server" ClientIDMode="Static"/>
                  </div>

                <div class="form-group">
                  <label>Date Of Birth</label>
                   <div class="line">
                    <asp:dropdownlist ID="ddlDay" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="Day" Value="0" />                   
                    </asp:dropdownlist>
                    <asp:dropdownlist ID="ddlMonth" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="Month" Value="0" Selected="True"/>
                    </asp:dropdownlist>
                    <asp:dropdownlist ID="ddlYear" runat="server" ClientIDMode="Static">
                        <asp:ListItem Text="year" Value="0" Selected="True"/>
                    </asp:dropdownlist>
                   </div>
               </div>

               <div class="form-group">
                   <label>Age</label>
                   <asp:TextBox ID="txtAge" runat="server" ClientIDMode="Static" onkeydown="return false;"/>
              </div>

              <div class="form-group">
                   <label>Gender</label>
                   <asp:RadioButtonList ID="rblGender" runat="server" ClientIDMode="Static" RepeatLayout="Flow" RepeatDirection="Horizontal">
                     <asp:ListItem Text="Male" Value="0" Selected="True"></asp:ListItem>
                     <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                   </asp:RadioButtonList>
              </div>

              <div class="form-group">
                   <label>Address</label>
                   <asp:TextBox ID ="address" TextMode="MultiLine" runat="server" ClientIDMode="Static"/>
              </div>

            <asp:UpdatePanel runat="server">
            <ContentTemplate>

              <div class="form-group">
                   <label>State</label>
                   <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" 
                       ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlsate_Selection">
                       <asp:ListItem  Text="-- Select State --" Value="" Selected="True"></asp:ListItem>
                       <asp:ListItem  Text="Kerala" Value="Kerala"></asp:ListItem>
                       <asp:ListItem  Text="Tamilnadu" Value="Tamilnadu"></asp:ListItem>
                       <asp:ListItem  Text="Karnataka" Value="Karnataka"></asp:ListItem>
                  </asp:DropDownList>
              </div>

              <div class="form-group">
                   <label>City</label>
                   <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" ClientIDMode="Static">
                      <asp:ListItem Text="-- Select State First --" Value=""></asp:ListItem>
                   </asp:DropDownList>
              </div>
             </ContentTemplate>
            </asp:UpdatePanel>

             <div class="form-group">
                <label>Zipcode</label>
                <asp:TextBox ID="zip" runat="server" ClientIDMode="Static"/>
             </div>

            <div class="form-group">
                <label>Phone</label>
                <asp:TextBox ID="phone" runat="server" ClientIDMode="Static"/>
            </div>

            <div class="form-group">
                <label>Mobile</label>
                <asp:TextBox ID="mobile" TextMode="Number" MaxLength="10" runat="server" ClientIDMode="Static"/>
            </div>
            <br/>
            <br/>
            <div class="line">
                <asp:Button ID="bntcancel" Text="Cancel" OnClientClick="this.form.reset(); return false;" runat="server" CssClass="btn"/>
                <asp:Button ID="btnSubmit" Text="Register" OnClick="btnSubmit_Click"  runat="server" CssClass="btn" />


                <br />
                <br />
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Visible="false" CssClass="error"></asp:Label>
            </div>
            <br/>
       </form>
    </asp:Content>
