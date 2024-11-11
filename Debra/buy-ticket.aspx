<%@ Page Title="" Language="C#" MasterPageFile="~/buy-ticket.Master" AutoEventWireup="true" CodeBehind="buy-ticket.aspx.cs" Inherits="Debra.buy_ticket1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="buy-ticket-container">
        <div class="header">
            <h1>Buy Ticket</h1>
        </div>

        <div class="ticket-details">
            <h2>Event Details</h2>
            <div class="details-card">
                <img id="eventPoster" class="event-poster" src="" alt="Event Poster" runat="server" />
                <div class="event-info">
                    <p>
                        <strong>Event ID:</strong>
                        <asp:Label ID="lblEventID" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Event Name:</strong>
                        <asp:Label ID="lblEventName" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Date:</strong>
                        <asp:Label ID="lblEventDate" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Location:</strong>
                        <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        <strong>Category:</strong>
                        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                    </p>
                    <p><strong>Price:</strong> RS. <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label></p>
                </div>
            </div>

            <div class="purchase-section">
                <h3>Purchase Ticket</h3>
                <label for="ticket-quantity">Quantity:</label>
                <input type="number" id="ticket-quantity" min="1" value="1" class="quantity-input">
                <button class="purchase-button" id="open-modal">Buy Ticket</button>
            </div>
        </div>
    </div>

    <!-- Modal for Purchase Confirmation -->
    <div id="purchase-modal" class="modal">
        <div class="modal-content">
            <span class="close-button">&times;</span>
            <h2>Confirm Your Purchase</h2>
            <p>
                <strong>Event Name:</strong>
                <asp:Label ID="modalEventName" runat="server" Text=""></asp:Label>
            </p>
            <p><strong>Price per Ticket:</strong> RS. <asp:Label ID="modalPrice" runat="server" Text=""></asp:Label></p>
            <p><strong>Quantity:</strong> <span id="selected-quantity">1</span></p>
            <p><strong>Total Price:</strong> RS.<span id="total-price">50.00</span></p>

            <label for="customer-name">Customer Name:</label>
            <input type="text" id="customerNameTextBox" runat="server" placeholder="Enter your name" required />

            <label for="customer-email">Customer Email:</label>
            <input type="email" id="customerEmailTextBox" runat="server" placeholder="Enter your email" required />

            <label for="customer-contact">Customer Contact:</label>
            <input type="tel" id="customerContactTextBox" runat="server" placeholder="Enter your contact number" required 
                   oninput="validateContactNumber(this)" />

            <label for="card-number">Card Number:</label>
            <input type="text" id="cardNumberTextBox" runat="server" placeholder="0000-0000-0000-0000" maxlength="19" required 
                   oninput="formatCardNumber(this)" />

            <label for="ticket-quantity">Quantity:</label>
            <input type="number" id="ticketQuantityTextBox" runat="server" min="1" value="1" required />

            <!-- Hidden labels for event details -->
            <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>

            <asp:Button ID="ConfirmPurchaseButton" runat="server" Text="Confirm Purchase" OnClick="ConfirmPurchase_Click" />
        </div>
    </div>

    <script>
        const pricePerTicket = parseFloat(document.getElementById('<%= lblPrice.ClientID %>').textContent); // Price per ticket
        const openModalButton = document.getElementById('open-modal');
        const modal = document.getElementById('purchase-modal');
        const closeModalButton = document.querySelector('.close-button');
        const selectedQuantity = document.getElementById('ticket-quantity');
        const totalPriceElement = document.getElementById('total-price');

        // Open modal and set values
        openModalButton.addEventListener('click', function () {
            const quantity = selectedQuantity.value;
            const totalPrice = (pricePerTicket * quantity).toFixed(2);
            document.getElementById('selected-quantity').textContent = quantity;
            document.getElementById('total-price').textContent = totalPrice;

            modal.style.display = 'block';
        });

        // Close modal
        closeModalButton.addEventListener('click', function () {
            modal.style.display = 'none';
        });

        // Close modal when clicking outside
        window.addEventListener('click', function (event) {
            if (event.target === modal) {
                modal.style.display = 'none';
            }
        });

        // Function to format card number input
        function formatCardNumber(input) {
            let value = input.value.replace(/\D/g, ''); // Remove all non-digit characters
            if (value.length > 16) {
                value = value.slice(0, 16); // Limit to 16 digits
            }
            const formattedValue = value.replace(/(\d{4})(?=\d)/g, '$1-'); // Add dash every 4 digits
            input.value = formattedValue; // Update input value
        }

        // Function to validate contact number input
        function validateContactNumber(input) {
            // Allow only digits and one hyphen
            const regex = /^(\d{0,10}|(\d{0,9}-\d{1,1}))$/;
            if (!regex.test(input.value)) {
                input.value = input.value.slice(0, -1); // Remove the last character if it doesn't match
            }
        }
    </script>
</asp:Content>
