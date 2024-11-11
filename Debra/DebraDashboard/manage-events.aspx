<%@ Page Title="Manage Events" Language="C#" MasterPageFile="~/DebraDashboard/DebraDashboard.Master" AutoEventWireup="true" CodeBehind="manage-events.aspx.cs" Inherits="Debra.DebraDashboard.manage_events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main-content">
        <h2>Manage Events</h2>
        <div class="card-grid" id="eventCardGrid" runat="server">
            <!-- Dynamic Event Cards will be added here -->
        </div>
    </div>
</asp:Content>
