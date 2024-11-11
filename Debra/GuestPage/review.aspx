<%@ Page Title="" Language="C#" MasterPageFile="~/GuestPage/guest.Master" AutoEventWireup="true" CodeBehind="review.aspx.cs" Inherits="Debra.GuestPage.review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="reviews">
        <div class="review-header">
            <h3>Reviews</h3>
            <a class="feedback-btn" href="feedback.aspx" style="text-decoration:none">Give Feedback</a>
        </div>

        <div class="feedback-grid">
            <asp:PlaceHolder ID="FeedbackPlaceHolder" runat="server"></asp:PlaceHolder>
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
</asp:Content>
