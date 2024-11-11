<%@ Page Title="" Language="C#" MasterPageFile="~/DebraDashboard/DebraDashboard.Master" AutoEventWireup="true" CodeBehind="manage-tickets.aspx.cs" Inherits="Debra.DebraDashboard.manage_tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../styles.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-content">
        <h2>Manage Tickets</h2>
        <div class="card-grid" runat="server" id="ticketCardGrid"></div>
    </div>
</asp:Content>
