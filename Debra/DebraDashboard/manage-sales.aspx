<%@ Page Title="" Language="C#" MasterPageFile="~/DebraDashboard/DebraDashboard.Master" AutoEventWireup="true" CodeBehind="manage-sales.aspx.cs" Inherits="Debra.DebraDashboard.manage_sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 18px;
            text-align: left;
        }

        th, td {
            padding: 12px;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #f4f4f4;
        }

        tr:hover {
            background-color: #f1f1f1;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-content">
        <h2>Manage Sales</h2>
        <table>
            <thead>
                <tr>
                    <th>Sale ID</th>
                    <th>Ticket ID</th>
                    <th>Event ID</th>
                    <th>Customer ID</th>
                    <th>Customer Name</th>
                    <th>Quantity</th>
                    <th>Total Price (RS)</th>
                    <th>Sale Date</th>
                </tr>
            </thead>
            <tbody id="salesTableBody" runat="server">
                <!-- Sales data will be populated here dynamically -->
            </tbody>
        </table>
    </div>
</asp:Content>
