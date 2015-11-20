<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="asemat.aspx.cs" Inherits="Controllers_asemat_asemat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
    <h1 class="page-header">Asemien aikataulut</h1>
    <div class="input-group col-md-12">
        <asp:TextBox ID="tbSearchStations" CssClass="search-query form-control" runat="server" placeholder="Kirjoita aseman nimi tai aseman tunnus"></asp:TextBox>
        <span class="input-group-btn">
            <asp:Button ID="btnSearchStations" CssClass="btn btn-success train-search-button" runat="server" Text="Etsi" OnClick="btnSearchStations_Click"/>
        </span>
        <div class="col-md-3">
            <asp:TextBox ID="tbStationDate" CssClass="search-query form-control short-textbox" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="tbStationTime" CssClass="search-query form-control short-textbox" runat="server"></asp:TextBox>
        </div>

    </div>

</asp:Content>

