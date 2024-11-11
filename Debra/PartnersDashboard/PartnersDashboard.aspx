<%@ Page Title="" Language="C#" MasterPageFile="~/PartnersDashboard/PartnersDashboard.Master" AutoEventWireup="true" CodeBehind="PartnersDashboard.aspx.cs" Inherits="Debra.PartnersDashboard.PartnersDashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="~/css/PartnersDashboard.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <link rel="icon" href="~/images/logo.png" type="image/x-icon">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="sidebar">
        <div class="logo">Partners Dashboard</div>
        <ul class="sidebar-menu">
            <li><a href="add-events.aspx"><i class="fas fa-plus-circle"></i> Add Events</a></li>
            <li><a href="events.aspx"><i class="fas fa-calendar-alt"></i> Events</a></li>
            <li><a href="tickets.aspx"><i class="fas fa-tags"></i> Tickets</a></li>
        </ul>
    </div>

    <div class="main-content">
        <h2>Welcome to the Partners Dashboard</h2>
        <p>Select an option from the sidebar to manage your events and tickets.</p>
    </div>

</asp:Content>
