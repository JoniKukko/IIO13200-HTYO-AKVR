<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="asemat.aspx.cs" Inherits="Controllers_asemat_asemat" %>

<asp:content id="Content1" contentplaceholderid="head" runat="Server">
</asp:content>



<asp:content id="Content2" contentplaceholderid="mainContentHolder" runat="Server">
    <div class="col-sm-12">
        <h1 class="page-header">Asemien aikataulut</h1>
    </div>
    <div class="col-md-12">
        <div class="row">
            <div class="input-group col-sm-6">
                <asp:TextBox ID="tbSearchStations" CssClass="search-query form-control col-sm-6" runat="server" placeholder="Kirjoita aseman nimi tai aseman tunnus"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:Button ID="btnSearchStations" CssClass="btn btn-success train-search-button" runat="server" Text="Etsi" OnClick="btnSearchStations_Click"/>
                </span>
            </div>
            <div>
                <asp:Label ID="labelStation" runat="server"></asp:Label>
            </div>
            <div>
                <h2>Saapuvat</h2>
                <asp:Table ID="tableArrivingTrains" runat="server" CssClass="col-md-3 table table-striped">
                    <asp:TableHeaderRow>
                        <asp:TableCell>PVM</asp:TableCell>
                        <asp:TableCell>Määränpää</asp:TableCell>
                        <asp:TableCell>Raide</asp:TableCell>
                        <asp:TableCell>Aikataulu</asp:TableCell>
                        <asp:TableCell>Ennuste</asp:TableCell>
                        <asp:TableCell>Junan numero</asp:TableCell>
                    </asp:TableHeaderRow>
                </asp:Table>
                <h2>Lähtevät</h2>
                <asp:Table ID="tableDepartingTrains" runat="server" CssClass="col-md-3 table table-striped">
                    <asp:TableHeaderRow>
                        <asp:TableCell>PVM</asp:TableCell>
                        <asp:TableCell>Määränpää</asp:TableCell>
                        <asp:TableCell>Raide</asp:TableCell>
                        <asp:TableCell>Aikataulu</asp:TableCell>
                        <asp:TableCell>Ennuste</asp:TableCell>
                        <asp:TableCell>Junan numero</asp:TableCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
        </div>
    </div>

</asp:content>

