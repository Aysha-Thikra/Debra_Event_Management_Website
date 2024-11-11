<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Debra.login1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="login-container">
        <h2>Login to your account</h2>

        <asp:Panel runat="server" CssClass="login-form">
            <div class="form-group">
                <label for="username">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter your username"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="password">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
            </div>

            <asp:Button ID="LoginButton" runat="server" CssClass="login-btn" Text="Login" OnClick="LoginBtn" />

            <p class="signup-text">Don’t have an account? <a href="signup.aspx">Signup here</a></p>
        </asp:Panel>
    </div>

</asp:Content>
