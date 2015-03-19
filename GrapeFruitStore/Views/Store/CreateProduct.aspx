<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GrapeFruitStore.Models.Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - Создать продукт
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Создать продукт</h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <% Html.RenderPartial("ProductForm"); %>
    <p>
        <%=Html.ActionLink("Вернуться к списку", "Index", null, new { @class = "button" })%>
    </p>
</asp:Content>
