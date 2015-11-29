<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="tilastot.aspx.cs" Inherits="Controllers_tilastot_tilastot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
    <h1 class="page-header">Tilastot</h1>
    <div class="input-group col-sm-8">
        <asp:Button ID="btnDelays" runat="server" Text="Myöhästymiset" CssClass="btn btn-success train-search-button" />
        <asp:Button ID="btnReasons" runat="server" Text="Button" CssClass="btn btn-success train-search-button" />
        <asp:Button ID="Button3" runat="server" Text="Button" CssClass="btn btn-success train-search-button" />
    </div>
    <div class="row">
        <div class="col-sm-12">

        </div>
    </div>
</asp:Content>

