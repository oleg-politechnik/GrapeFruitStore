<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    GrapeFruit Store - Регистрация
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Создать аккаунт</h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <p>
        Здравствуйте, а вы знаете, у нас к регистрации допускаются только зарегистрированные пользователи. 
    </p>
    <p>
        Шутка.
    </p>
    <%= Html.ValidationSummary("") %>
    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Аккаунт</legend>
                <p>
                    <label for="username">Имя пользователя:</label>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="email">Email:</label>
                    <%= Html.TextBox("email") %>
                    <%= Html.ValidationMessage("email") %>
                </p>
                <p>
                    <label for="password">Пароль:</label>
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </p>
                <p>
                    <label for="confirmPassword">Подтвердите пароль:</label>
                    <%= Html.Password("confirmPassword") %>
                    <%= Html.ValidationMessage("confirmPassword") %>
                </p>
                <p>
                    <input type="submit" value="Зарегистрироваться" name="Register" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
