<%@ Page Title="" Language="C#" MasterPageFile="~/DebraDashboard/DebraDashboard.Master" AutoEventWireup="true" CodeBehind="earnings.aspx.cs" Inherits="Debra.DebraDashboard.earnings" %>

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
        <h2>Earnings</h2>
        <table>
            <thead>
                <tr>
                    <th>Earning ID</th>
                    <th>Event ID</th>
                    <th>Ticket ID</th>
                    <th>Total Earnings (RS)</th>
                    <th>Commission (RS)</th>
                    <th>Earning Date</th>
                </tr>
            </thead>
            <tbody>
                <asp:Literal ID="earningsTableBody" runat="server"></asp:Literal>
            </tbody>
        </table>
    </div>
</asp:Content>
