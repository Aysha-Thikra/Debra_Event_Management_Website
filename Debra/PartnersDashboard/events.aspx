<%@ Page Title="" Language="C#" MasterPageFile="~/PartnersDashboard/PartnersDashboard.Master" AutoEventWireup="true" CodeBehind="events.aspx.cs" Inherits="Debra.PartnersDashboard.events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main-content">
        <h2>Events</h2>
        <div class="card-grid" id="EventCardsPlaceholder" runat="server">
            <!-- Dynamic event cards will be added here -->
        </div>
    </div>

</asp:Content>
