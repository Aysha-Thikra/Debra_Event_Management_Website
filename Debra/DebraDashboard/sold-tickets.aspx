<%@ Page Title="" Language="C#" MasterPageFile="~/DebraDashboard/DebraDashboard.Master" AutoEventWireup="true" CodeBehind="sold-tickets.aspx.cs" Inherits="Debra.DebraDashboard.current_tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-content">
        <h2>Sold Tickets</h2>
        
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

        <div class="card-grid">
            <asp:Repeater ID="rptCurrentTickets" runat="server">
                <ItemTemplate>
                    <div class="ticket-card">
                        <div class="card-header">
                            <h3>Event ID: <%# Eval("EventID") %></h3>
                            <h4>Price per Ticket: RS.<%# Eval("PerTicketPrice") %></h4>
                        </div>
                        <div class="card-body">
                            <p><strong>Number of Tickets:</strong> <%# Eval("TicketQuantity") %></p>
                            <p><strong>Event Name:</strong> <%# Eval("EventName") %></p>
                            <p><strong>Customer Name:</strong> <%# Eval("CustomerName") %></p>
                            <p><strong>Customer Email:</strong> <%# Eval("CustomerEmail") %></p>
                            <p><strong>Customer Contact:</strong> <%# Eval("CustomerContact") %></p>
                            <p><strong>Card Number:</strong> <%# Eval("CardNumber") %></p> 
                            <p><strong>Total Price:</strong> RS.<%# Eval("TotalPrice") %></p>
                            <p><strong>Purchase Date:</strong> <%# Eval("PurchaseDate", "{0:MM/dd/yyyy}") %></p>
                        </div>
                        <div class="card-footer">
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Repeater>
    </div>

</asp:Content>
