<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="junat.aspx.cs" Inherits="Controllers_junat_junat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="Server">
    <div class="col-md-12">
        <h1 class="page-header">Junien aikataulut ja tiedot</h1>
    </div>
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="tbSearchTrains" CssClass="search-query form-control" runat="server" placeholder="Kirjoita junan tunnus tai asema"></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:Button ID="btnSearchTrains" CssClass="btn btn-success train-search-button" runat="server" Text="Etsi" OnClick="btnSearchTrains_Click" />
                    </span>
                </div>
            </div>
        </div>

        <div class="row spacer-both-xs">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:DropDownList ID="dlTrains" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="dlTrains_SelectedIndexChanged"></asp:DropDownList>
                    <span class="input-group-addon">
                        <asp:Label ID="labelTrain" runat="server"></asp:Label></span>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <asp:Table ID="tableTrainResults" runat="server" CssClass="table table-striped">
            <asp:TableHeaderRow>
                <asp:TableCell>Asema</asp:TableCell>
                <asp:TableCell>Raide</asp:TableCell>
                <asp:TableCell>Saapuu</asp:TableCell>
                <asp:TableCell>Lähtee</asp:TableCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>

