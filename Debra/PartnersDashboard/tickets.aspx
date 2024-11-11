<%@ Page Title="" Language="C#" MasterPageFile="~/PartnersDashboard/PartnersDashboard.Master" AutoEventWireup="true" CodeBehind="tickets.aspx.cs" Inherits="Debra.PartnersDashboard.tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-content">
        <h2>Tickets</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Size="XX-Large"></asp:Label> 
        <div class="card-grid">
            <asp:Repeater ID="rptTickets" runat="server">
                <ItemTemplate>
                    <div class="ticket-card">
                        <h3>Ticket ID: <%# Eval("TicketID") %></h3>
                        <p><strong>Price of one Ticket:</strong> RS. <%# Eval("Price") %></p>
                        <p><strong>Total number of Tickets:</strong> <%# Eval("Quantity") %></p>
                        <p><strong>Event ID:</strong> <%# Eval("EventID") %></p>
                        <p><strong>Event Name:</strong> <%# Eval("EventName") %></p>
                        <p><strong>Status:</strong> <%# Eval("Status") %></p>
                        <button class="edit-btn" onclick='location.href="edit_ticket.aspx?TicketID=<%# Eval("TicketID") %>"'>Edit</button>
                        <button class="delete-btn" onclick='DeleteTicket("<%# Eval("TicketID") %>")'>Delete</button>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Repeater>
    </div>
    
    <script type="text/javascript">
        function DeleteTicket(ticketId) {
            if (confirm('Are you sure you want to delete this ticket?')) {
                window.location = 'delete_ticket.aspx?TicketID=' + ticketId;
            }
        }
    </script>
</asp:Content>
