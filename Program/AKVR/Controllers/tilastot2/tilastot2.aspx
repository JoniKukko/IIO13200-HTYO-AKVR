<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="tilastot2.aspx.cs" Inherits="Controllers_tilastot2_tilastot2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" Runat="Server">
<div class="col-sm-12">

    <!-- OTSIKKO -->
    <div class="row">
        <div class="col-sm-12">
            <div class="page-header">
                <h1>Tilastot <small>Viimeisimmät syyt</small></h1>
            </div>
        </div>
    </div>


    <!-- PVM KENTTÄ JA HAKUPAINIKE -->
    <div class="row">
        <div class="col-sm-12">
            <div class="form-inline">
                <div class="form-group">
                    <label>Päivämäärä</label>
                    <asp:TextBox ID="datebox_date" runat="server" type="date" CssClass="form-control" value="2015-12-01" />
                </div>
                <asp:button ID="buttonSelectCauses" runat="server" CssClass="btn btn-success train-search-button" Text="Hae" OnClick="buttonSelectCauses_Click" />
            </div>
        </div>
    </div>


    <!-- TULOS TAULUKKO -->
    <div class="row spacer-top-md">
        <div class="col-sm-12">
            <asp:table runat="server" ID="table_trainData" CssClass="table table-bordered">
                <asp:TableRow>
                    <asp:TableCell>Juna</asp:TableCell>
                    <asp:TableCell>Tyyppi</asp:TableCell>
                    <asp:TableCell>Asema</asp:TableCell>
                    <asp:TableCell>Syy</asp:TableCell>
                </asp:TableRow>
            </asp:table>
        </div>
    </div>

    
</div>
</asp:Content>

