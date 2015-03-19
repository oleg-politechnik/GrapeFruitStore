<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - About Us
</asp:Content>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        About</h2>
    <p style="min-height: 340px;">
        <img src="../../Content/Images/store.jpg" />
        Этот интернет-магазин — интерактивный веб-сайт, рекламирующий товар или услугу,
        принимающий заказы на покупку.
        <br />
        Выбрав необходимые товары или услуги, пользователь имеет возможность оформить заказ.
        После отправки заказа с покупателем связывается продавец и уточняет место и время,
        в которое следует доставить заказ. Доставка осуществляется либо собственной курьерской
        службой, либо компанией, предоставляющей услуги доставки, либо по почте — посылкой
        или бандеролью. Товар оплачивается курьеру наличными деньгами при получении покупателем
        товара.
    </p>
</asp:Content>
