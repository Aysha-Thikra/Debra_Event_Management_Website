<%@ Page Title="" Language="C#" MasterPageFile="~/GuestPage/guest.Master" AutoEventWireup="true" CodeBehind="event.aspx.cs" Inherits="Debra.GuestPage._event" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="featured-image">
        <div class="image-container">
            <img src="../images/crowd.jpg" alt="Concert Events">
            <div class="overlay-bg"></div>
            <div class="overlay-text">Concert Events</div>
        </div>
    </section>

    <section class="events-section" runat="server" id="eventsPlaceholder">
        <!-- Event cards will be loaded here dynamically -->
    </section>

    <footer>
        <p>&copy; 2024 Debra. All rights reserved.</p>
        <div class="footer-links">
            <ul>
                <li><a href="#privacy-policy">Privacy Policy</a></li>
                <li><a href="#terms-of-service">Terms of Service</a></li>
                <li><a href="#sitemap">Sitemap</a></li>
            </ul>
        </div>
    </footer>
</asp:Content>
