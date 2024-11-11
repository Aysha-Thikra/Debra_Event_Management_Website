<%@ Page Title="" Language="C#" MasterPageFile="~/DebraDashboard/DebraDashboard.Master" AutoEventWireup="true" CodeBehind="DebraDashboard.aspx.cs" Inherits="Debra.DebraDashboard.DebraDashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Main Content -->
    <div class="main-content">
        <div class="header">
            <div class="header-title">Admin Dashboard</div>
            <div class="header-right">
                <div class="search-bar">
                    <input type="text" placeholder="Search...">
                    <i class="fas fa-search"></i>
                </div>
                <div class="user-profile">
                    <span>Admin</span>
                    <img class="user-avatar" src="../images/categories/blues.jpeg" alt="Admin Avatar">
                </div>
            </div>
        </div>

        <!-- Dashboard Section -->
        <div class="dashboard-section">
            <h2>Welcome to the Admin Dashboard</h2>
            <div class="card-container">
                <div class="Rcard">
                    <div class="card-content">
                        <i class="fas fa-calendar-alt"></i>
                        <h3><asp:Label ID="TotalEventsLabel" runat="server" Text="0"></asp:Label></h3> 
                        <p>Total Events</p>
                    </div>
                </div>
                <div class="Rcard">
                    <div class="card-content">
                        <i class="fas fa-ticket-alt"></i>
                        <h3><asp:Label ID="TicketsSoldLabel" runat="server" Text="0"></asp:Label></h3>
                        <p>Tickets Sold</p>
                    </div>
                </div>
                <div class="Rcard">
                    <div class="card-content">
                        <i class="fas fa-dollar-sign"></i>
                        <h3><asp:Label ID="TotalEarningsLabel" runat="server" Text="$0.00"></asp:Label></h3> 
                        <p>Total Earnings</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
