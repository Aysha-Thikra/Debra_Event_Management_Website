<%@ Page Title="" Language="C#" MasterPageFile="~/GuestPage/guest.Master" AutoEventWireup="true" CodeBehind="about-us.aspx.cs" Inherits="Debra.GuestPage.about_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="about">
        <div class="about-container">
            <div class="about-content">
                <h2>About Debra</h2>
                <p class="about-description">
                    Welcome to Debra, where we redefine the art of event management. Our mission is to create unforgettable
                    experiences that bring people together, whether it’s a concert, a corporate event, or a community festival. 
                    With a team of dedicated professionals, we ensure every detail is meticulously planned and executed.
                </p>
            </div>

            <div class="about-images">
                <img src="../images/debra.png" alt="Event Management" class="about-image">
            </div>

            <hr class="custom-hr-1">
            <hr class="custom-hr">
            <h3 class="vision-mission">Our Values</h3>
            <div class="about-values-container">
                <div class="value-card">
                    <i class="fas fa-check-circle"></i>
                    <h4>Innovation</h4>
                    <p>We embrace creativity in all our projects.</p>
                </div>
                <div class="value-card">
                    <i class="fas fa-check-circle"></i>
                    <h4>Customer Satisfaction</h4>
                    <p>Our clients are at the heart of everything we do.</p>
                </div>
                <div class="value-card">
                    <i class="fas fa-check-circle"></i>
                    <h4>Sustainability</h4>
                    <p>We are committed to eco-friendly practices.</p>
                </div>
                <div class="value-card">
                    <i class="fas fa-check-circle"></i>
                    <h4>Community Engagement</h4>
                    <p>We believe in giving back to our community.</p>
                </div>
            </div>

            <hr class="custom-hr">
            <h3 class="vision-mission">Our Vision and Mission</h3>
            <div class="vision-mission-wrapper">
                <div class="vision-card">
                    <i class="fas fa-eye"></i>
                    <h4>Vision</h4>
                    <p>
                        To be a leading event management company that transforms experiences through creativity and
                        excellence.
                    </p>
                </div>
                <div class="mission-card">
                    <i class="fas fa-bullseye"></i>
                    <h4>Mission</h4>
                    <p>
                        To provide exceptional event management services that exceed our clients' expectations and create
                        lasting memories.
                    </p>
                </div>
            </div>

            <hr class="custom-hr">
            <h3 class="vision-mission">Our Team</h3>
            <p class="team-description">
                Our diverse team of experts brings a wealth of experience from various industries, ensuring that every event is unique and tailored to our clients' needs. We are passionate about what we do, and it shows in our results.
            </p>
            <div class="team-profiles">
                <div class="profile">
                    <img src="../images/manager.png" alt="Team Member 1">
                    <h4>John Doe</h4>
                    <p>Event Manager</p>
                </div>
                <div class="profile">
                    <img src="../images/marketing manager.png" alt="Team Member 2">
                    <h4>Jane Smith</h4>
                    <p>Marketing Director</p>
                </div>
                <div class="profile">
                    <img src="../images/Logistics Coordinator.png" alt="Team Member 3">
                    <h4>Michael Brown</h4>
                    <p>Logistics Coordinator</p>
                </div>
            </div>
        </div>
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
    </body>

</html>

</asp:Content>
