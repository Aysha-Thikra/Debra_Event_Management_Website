<%@ Page Title="" Language="C#" MasterPageFile="~/PartnersDashboard/PartnersDashboard.Master" AutoEventWireup="true" CodeBehind="add-events.aspx.cs" Inherits="Debra.PartnersDashboard.add_events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="icon" href="../images/logo.png" type="image/x-icon">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="main-content">
        <h2>Add Events</h2>
        <div class="card-container">
            <div class="event-card">
                <div class="form-group">
                    <h5 style="color:white;">Welcome to Debra partners dashboard . select an option . where you can manage the events, manage the tickets, view the events and tickets</h5>
                    <label for="event-name">Event Name:</label>
                    <asp:TextBox ID="eventName" runat="server" required placeholder="Enter event name"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="event-date">Event Date:</label>
                    <asp:TextBox ID="eventDate" runat="server" required placeholder="Enter event date" TextMode="Date"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="event-location">Location:</label>
                    <asp:DropDownList ID="eventLocation" runat="server" required>
                        <asp:ListItem Value="" Text="Select location" Enabled="false" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="colombo-music-hall" Text="The Colombo Music Hall"></asp:ListItem>
                        <asp:ListItem Value="kandy-royal-theatre" Text="Kandy Royal Theatre"></asp:ListItem>
                        <asp:ListItem Value="galle-fort-arena" Text="Galle Fort Arena"></asp:ListItem>
                        <asp:ListItem Value="negombo-beach-pavilion" Text="Negombo Beach Pavilion"></asp:ListItem>
                        <asp:ListItem Value="jaffna-cultural-centre" Text="Jaffna Cultural Centre"></asp:ListItem>
                        <asp:ListItem Value="viharamahadevi-open-air-theatre" Text="Viharamahadevi Open Air Theatre"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="event-description">Description:</label>
                    <asp:TextBox ID="eventDescription" runat="server" required TextMode="MultiLine" placeholder="Enter event description"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="event-category">Category:</label>
                    <asp:DropDownList ID="eventCategory" runat="server" required>
                        <asp:ListItem Value="" Text="Select category" Enabled="false" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="rock" Text="Rock"></asp:ListItem>
                        <asp:ListItem Value="jazz" Text="Jazz"></asp:ListItem>
                        <asp:ListItem Value="blues" Text="Blues"></asp:ListItem>
                        <asp:ListItem Value="classical" Text="Classical"></asp:ListItem>
                        <asp:ListItem Value="hip-hop" Text="Hip-Hop"></asp:ListItem>
                        <asp:ListItem Value="electronic" Text="Electronic"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="event-poster">Event Poster:</label>
                    <asp:FileUpload ID="eventPoster" runat="server" accept="image/*" required></asp:FileUpload>
                </div>
                <asp:Label ID="errorMessageLabel" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                <asp:Button ID="submitEventButton" runat="server" Text="Add Event" OnClick="SubmitEvent_Click" CssClass="submit-btn" />
            </div>
        </div>
    </div>

</asp:Content>
