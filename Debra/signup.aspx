<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/signup.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="Debra.signup1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .popup {
            display: none; 
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0, 0, 0, 0.4); 
        }

        .popup-content {
            background-color: red;
            color: white; 
            margin: 15% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%; 
            text-align: center;
        }

        .close {
            color: #fff; 
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

        .close:hover,
        .close:focus {
            color: #ccc; 
            text-decoration: none;
            cursor: pointer;
        }
    </style>

    <div class="signup-container">
        <h2>Sign Up for Your Account</h2>

        <!-- Popup for error messages -->
        <div id="errorPopup" class="popup">
            <div class="popup-content">
                <span class="close" onclick="closePopup()">&times;</span>
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="false"></asp:Literal>
            </div>
        </div>

        <form class="signup-form" onsubmit="return validateForm();">
            <div class="form-group">
                <label for="first-name">First Name</label>
                <asp:TextBox ID="FirstName" runat="server" placeholder="Enter your first name" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="last-name">Last Name</label>
                <asp:TextBox ID="LastName" runat="server" placeholder="Enter your last name" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="company-name">Company Name</label>
                <asp:TextBox ID="CompanyName" runat="server" placeholder="Enter your company name" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="contact-number">Contact Number</label>
                <asp:TextBox ID="ContactNumber" runat="server" placeholder="000-0000000" required="required" maxlength="12" 
                             onkeypress="return onlyNumbers(event)" oninput="formatPhoneNumber(this)" 
                             title="Please enter a valid contact number in the format 000-0000000"></asp:TextBox>
                <span id="contactError" style="color:red; display:none;">Please enter a valid contact number.</span>
            </div>
            <div class="form-group">
                <label for="email">Email</label>
                <asp:TextBox ID="Email" runat="server" placeholder="Enter your email" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="address">Address</label>
                <asp:TextBox ID="Address" runat="server" placeholder="Enter your address" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="username">Username</label>
                <asp:TextBox ID="Username" runat="server" placeholder="Enter a username" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <asp:TextBox ID="Password" runat="server" placeholder="Create a password" TextMode="Password" required="required"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="SubmitButton" runat="server" Text="Create Account" CssClass="signup-btn" OnClick="SubmitForm" />
            </div>
        </form>
        <p class="login-text">Already have an account? <a href="login.aspx">Login here</a></p>
    </div>

    <script>
        // Function to show the popup
        function showPopup() {
            var popup = document.getElementById("errorPopup");
            popup.style.display = "block";
        }

        // Function to close the popup
        function closePopup() {
            var popup = document.getElementById("errorPopup");
            popup.style.display = "none";
        }

        // Restrict input to numbers only
        function onlyNumbers(event) {
            var char = String.fromCharCode(event.which);
            if (!/[0-9]/.test(char) && event.which !== 45) { 
                event.preventDefault();
            }
        }

        // Format input to 000-0000000
        function formatPhoneNumber(input) {
            var value = input.value.replace(/\D/g, ''); 
            if (value.length > 3 && value.length <= 10) {
                value = value.slice(0, 3) + '-' + value.slice(3); 
            }
            input.value = value.slice(0, 12); 
        }

        // Validate the contact number
        function validateForm() {
            var contactNumber = document.getElementById("ContactNumber").value;
            var regex = /^\d{3}-\d{7}$/; 
            var contactError = document.getElementById("contactError");

            if (!regex.test(contactNumber)) {
                contactError.style.display = "block"; 
                return false;
            } else {
                contactError.style.display = "none"; 
                return true; 
            }
        }

        // Show popup if there's an error message
        <% if (!string.IsNullOrEmpty(ErrorMessage.Text)) { %>
            showPopup();
        <% } %>
    </script>

</asp:Content>
