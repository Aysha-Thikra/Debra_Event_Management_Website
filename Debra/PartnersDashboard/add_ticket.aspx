<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_tickets.aspx.cs" Inherits="YourNamespace.add_tickets" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <link rel="icon" href="../images/logo.png" type="image/x-icon">
    <title>Add Ticket</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color:  #574752; 
            margin: 0;
            padding: 20px;
        }
        
        h2 {
            color: #49243E; 
            text-align: center;
        }

        .form-container {
            max-width: 600px;
            margin: auto;
            padding: 20px;
            background-color: #ffffff; 
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            margin-top: 70px;
        }

        label {
            font-weight: bold;
            color: #704264; 
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-group input, .form-group select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 16px;
            transition: border 0.3s;
        }

        .form-group input:focus, .form-group select:focus {
            border-color: #BB8493; 
            outline: none;
        }

        .btn-submit {
            background-color: #49243E; 
            color: white;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s;
            width: 100%;
        }

        .btn-submit:hover {
            background-color: #704264; 
        }

        .message {
            color: red;
            text-align: center;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Add Ticket</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
            <div class="form-group">
                <label for="ddlEvent">Select Event:</label>
                <asp:DropDownList ID="ddlEvent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="txtEventName">Event Name:</label>
                <asp:TextBox ID="txtEventName" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPrice">Price:</label>
                <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtQuantity">Quantity:</label>
                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="btnAddTicket" runat="server" Text="Add Ticket" OnClick="btnAddTicket_Click" CssClass="btn-submit" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnBack" runat="server" Text="Back to Partner Dashboard" OnClick="btnBack_Click" CssClass="btn-submit" />
            </div>
        </div>
    </form>
</body>
</html>
