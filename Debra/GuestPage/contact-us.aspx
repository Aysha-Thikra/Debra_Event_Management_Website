<%@ Page Title="" Language="C#" MasterPageFile="~/GuestPage/guest.Master" AutoEventWireup="true" CodeBehind="contact-us.aspx.cs" Inherits="Debra.GuestPage.contact_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="contact">
        <div class="contact-container">
            <h2>Contact Us</h2>
            <p>We'd love to hear from you! Fill out the form below or reach out to us through our contact information.</p>
            <form class="contact-form" method="post" onsubmit="return false;">
                <div class="form-group">
                    <label for="name">Name:</label>
                    <input type="text" id="name" name="name" placeholder="Enter your name..." required>
                </div>
                <div class="form-group">
                    <label for="email">Email:</label>
                    <input type="email" id="email" name="email" placeholder="Enter your email..." required>
                </div>
                <div class="form-group">
                    <label for="message">Message:</label>
                    <textarea id="message" name="message" rows="4" placeholder="Your Message" required></textarea>
                </div>
                <button type="button" class="submit-button" onclick="submitForm()">Send Message</button>
            </form>
            <div class="contact-info">
                <h3>Contact Information</h3>
                <p>If you have any questions or inquiries, feel free to reach out to us!</p>
                <div class="contact-card">
                    <div class="contact-item">
                        <i class="fas fa-map-marker-alt"></i>
                        <p>98/A Event Lane, Singapore</p>
                    </div>
                    <div class="contact-item">
                        <i class="fas fa-phone-alt"></i>
                        <p>+65 1234 5678</p>
                    </div>
                    <div class="contact-item">
                        <i class="fas fa-envelope"></i>
                        <p>info@debra.com</p>
                    </div>
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

    <script>
        function submitForm() {
            var name = document.getElementById("name").value;
            var email = document.getElementById("email").value;
            var message = document.getElementById("message").value;

            // Validate input
            if (name && email && message) {
                // Use AJAX to submit the form
                var xhr = new XMLHttpRequest();
                xhr.open("POST", "contact-us.aspx", true);
                xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                xhr.onreadystatechange = function () {
                    if (xhr.readyState === 4 && xhr.status === 200) {
                        alert('Your message has been sent successfully!');
                        document.getElementById("name").value = "";
                        document.getElementById("email").value = "";
                        document.getElementById("message").value = "";
                    } else if (xhr.readyState === 4) {
                        alert('Error sending message: ' + xhr.responseText);
                    }
                };
                xhr.send("name=" + encodeURIComponent(name) + "&email=" + encodeURIComponent(email) + "&message=" + encodeURIComponent(message));
            } else {
                alert('Please fill in all fields.');
            }
        }
    </script>
</asp:Content>
