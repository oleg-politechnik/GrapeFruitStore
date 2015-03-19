<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - Главная
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Добро пожаловать в GrapeFruit Store!</h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <p style="min-height: 130px;">
        <img src="../../Content/Images/freebsd.png" />
        GrapeFruitStore - это суперсовременный мегапопулярный гипермаркет для людей,
        которым интересно покупать совершенно бесполезные вещи или просто разнообразить
        свою жизнь новинками в области вареза, промтоваров, продовольствия и вино-водочного
        производства.
    </p>
</asp:Content>
