<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GrapeFruitStore.Models.Product>" %>
<% using (Html.BeginForm())
   {%>
<fieldset>
    <legend>Параметры</legend>
    <label for="error">
    </label>
    <p>
        <label for="Department">
            Отдел:</label>
        <b>
            <%= Html.Encode(Model.Department.Title)%></b>
    </p>
    <p>
        <label for="Title">
            Название:</label>
        <%= Html.TextBox("Title", Model.Title, new { @maxlength = 255 })%>
        <%= Html.ValidationMessage("Title", "*") %>
    </p>
    <p>
        <label for="Description">
            Описание:</label>
        <%= Html.TextArea("Description", Model.Description)%>
        <%= Html.ValidationMessage("Description", "*") %>
    </p>
    <p>
        <label for="ImageUrl">
            Картинка:</label>
        <%= Html.TextBox("ImageUrl", Model.ImageUrl, new { @maxlength = 255 })%>
        <%= Html.ValidationMessage("ImageUrl", "*") %>
    </p>
    <p>
        <label for="Price">
            Цена:</label>
        <%= Html.TextBox("Price", Model.Price)%>
        <%= Html.ValidationMessage("Price", "*")%>
    </p>
    <p>
        <label for="UnitsInStock">
            Количество:</label>
        <%= Html.TextBox("UnitsInStock", Model.UnitsInStock)%>
        <%= Html.ValidationMessage("UnitsInStock", "*") %>
    </p>
    <%= Html.Hidden("DepartmentID", Model.DepartmentID)%>
    <p>
        <input type="submit" value="Сохранить" />
    </p>
</fieldset>
<% } %>
<div>
    <%=Html.ActionLink("К отделу", "Department", new { id = Model.DepartmentID }, new { @class = "button" })%>
</div>
