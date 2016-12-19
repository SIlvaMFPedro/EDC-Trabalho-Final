<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Presidents.aspx.cs" Inherits="edc_trabalho_final.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" CssClass="table table-hover table-striped auto-style5">
        <Columns>
            <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
            <asp:BoundField DataField="Partido" HeaderText="Partido" SortExpression="Partido" />
            <asp:BoundField DataField="Idade" HeaderText="Idade" SortExpression="Idade" />
            <asp:BoundField DataField="PresidenteN" HeaderText="PresidenteN" SortExpression="PresidenteN" />
        </Columns>
    </asp:GridView>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/presidents.xml" TransformFile="~/App_Data/presidentes.xsl"></asp:XmlDataSource>
</asp:Content>
