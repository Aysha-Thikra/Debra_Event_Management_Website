<%@ Page Title="" Language="C#" MasterPageFile="~/DebraDashboard/DebraDashboard.Master" AutoEventWireup="true" CodeBehind="manage-commissions.aspx.cs" Inherits="Debra.DebraDashboard.manage_commissions" %>

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
        <h2>Manage Commissions</h2>
        <table>
            <thead>
                <tr>
                    <th>CommissionID</th>
                    <th>Partner Name</th>
                    <th>Event Name</th>
                    <th>Commission Rate (%)</th>
                    <th>Commission Amount (RS.)</th>
                </tr>
            </thead>
            <tbody>
                <asp:Literal ID="commissionTableBody" runat="server"></asp:Literal>
            </tbody>
        </table>
    </div>
</asp:Content>
