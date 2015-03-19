<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GrapeFruitStore.Models.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - ������� �������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        ������� �������</h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <% Html.RenderPartial("ProductForm"); %>
    <p>
        <%=Html.ActionLink("��������� � ������", "Index", null, new { @class = "button" })%>
    </p>
</asp:Content>
