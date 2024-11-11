<%@ Page Title="" Language="C#" MasterPageFile="~/DebraDashboard/DebraDashboard.Master" AutoEventWireup="true" CodeBehind="manage-partners.aspx.cs" Inherits="Debra.DebraDashboard.manage_partners" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
            gap: 20px;
        }
        .partner-card {
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 15px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }
        .edit-btn, .delete-btn {
            color: white;
            padding: 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-content">
        <h2>Manage Partners</h2>
        <div class="card-grid">
            <asp:Literal ID="partnersTableBody" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>
