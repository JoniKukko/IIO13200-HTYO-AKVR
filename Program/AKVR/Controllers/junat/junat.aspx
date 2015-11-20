<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="junat.aspx.cs" Inherits="Controllers_junat_junat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="Server">
    <h1 class="page-header">Junien aikataulut ja tiedot</h1>
    <div id="train-search-input">
        <div class="input-group col-md-6">
            <asp:TextBox ID="tbSearchTrains" CssClass="search-query form-control" runat="server" placeholder="Kirjoita junan tunnus tai asema"></asp:TextBox>
            <span class="input-group-btn">
                <asp:Button ID="btnSearchTrains" CssClass="btn btn-success train-search-button" runat="server" Text="Etsi" OnClick="btnSearchTrains_Click" />
            </span>
        </div>
    </div>

    <div id="train-result-container">
        <asp:Label ID="labelTrain" runat="server"></asp:Label>
        <asp:Table ID="tableTrainResults" runat="server">
            <asp:TableHeaderRow>
                <asp:TableCell>Asema</asp:TableCell>
                <asp:TableCell>Raide</asp:TableCell>
                <asp:TableCell>Tyyppi</asp:TableCell>
                <asp:TableCell>Aika</asp:TableCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>

