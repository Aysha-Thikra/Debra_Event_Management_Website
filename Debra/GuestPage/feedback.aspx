<%@ Page Title="" Language="C#" MasterPageFile="~/GuestPage/guest.Master" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="Debra.GuestPage.feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="feedback-container">
        <h2>We Value Your Feedback</h2>
        <form class="feedback-form" method="post">
            <div class="form-group">
                <label for="name">Name</label>
                <input type="text" id="name" name="name" placeholder="Your Name" required>
            </div>
            <div class="form-group">
                <label for="email">Email</label>
                <input type="email" id="email" name="email" placeholder="Your Email" required>
            </div>
            <div class="form-group">
                <label for="feedback">Feedback</label>
                <textarea id="feedback" name="feedback" rows="4" placeholder="Your Feedback" required></textarea>
            </div>
            <div class="form-buttons">
                <button type="submit" class="submit-btn">Submit</button>
                <button type="reset" class="reset-btn">Reset</button>
            </div>
        </form>
    </div>

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
