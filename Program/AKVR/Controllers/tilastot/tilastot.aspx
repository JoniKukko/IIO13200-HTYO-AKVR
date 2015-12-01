<%@ Page Title="" Language="C#" MasterPageFile="~/Controllers/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="tilastot.aspx.cs" Inherits="Controllers_tilastot_tilastot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="Server">
    <h1 class="page-header">Tilastot</h1>
    <p>Myöhästymistilastot kertovat, kuinka hyvin junat ovat olleet ajallaan tiettynä aikavälinä. Jos annat molempiin päivämääräkenttiin saman päivän, saat yhden päivän tiedot.</p>
    <p>Keskimäärin myöhässä -sarake kertoo, kuinka paljon juna on keskimäärin ollut matkan aikana myöhässä. Tähän vaikuttaa muun muassa se, jos juna on lähtenyt aluksi ajallaan, tai jos se on saanut aikataulua kiinni.</p>
    <p>Enimmillään myöhässä -sarake kertoo, kuinka paljon juna oli myöhässä sillä liikennepaikkavälillä, jolta se myöhästyi eniten.</p>
    <div class="col-sm-12">
        <div class="row">
            <div class="form-inline col-sm-12">
                <div class="form-group">
                    <label for="labelFromDate">Alkupäivä: </label>
                    <asp:TextBox ID="tbDelayFrom" runat="server" CssClass="form-control" placeholder="5.10.2015"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="labelToDate">Loppupäivä: </label>
                    <asp:TextBox ID="tbDelayTo" runat="server" CssClass="form-control" placeholder="6.10.2015"></asp:TextBox>
                </div>
                <asp:Button ID="btnSearchDelayedTrains" runat="server" Text="Hae" CssClass="btn btn-success train-search-button" OnClick="btnSearchDelayedTrains_Click" />
            </div>
            <asp:Label ID="labelDelay" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:Repeater ID="repeaterDelayedTrains" runat="server">
                    <HeaderTemplate>
                        <table class="spacer-top-md table table-striped">
                            <tr>
                                <td>Juna</td>
                                <td>Lähtöpäivä</td>
                                <td>Tyyppi</td>
                                <td>Keskimäärin myöhässä (min)</td>
                                <td>Enimmillään myöhässä (min)</td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("FullTrainName") %></td>
                            <td><%# (string)Eval("departureDate", "{0:dd.MM.yyyy}") %></td>
                            <td><%# Eval("trainCategory") %></td>
                            <td><%# Eval("AverageDelay") %></td>
                            <td><%# Eval("MaxDelay") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>

