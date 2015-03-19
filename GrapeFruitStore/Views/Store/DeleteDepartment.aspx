<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GrapeFruitStore.Models.Department>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - Удалить отдел
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Удалить отдел</h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <div>
        <p>
            Что, правда что ли удалить <i>
                <%=Html.Encode(Model.Title) %>?</i>
        </p>
    </div>
    <% using (Html.BeginForm())
       { %>
    <input name="confirmButton" type="submit" value="Удалить" />
    <% } %>
</asp:Content>
