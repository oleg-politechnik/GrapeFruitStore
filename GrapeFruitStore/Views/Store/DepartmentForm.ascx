<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GrapeFruitStore.Models.Department>" %>
<% using (Html.BeginForm())
   {%>
<fieldset>
    <legend>���������</legend>
    <p>
        <label for="Title">
            ��������:</label>
        <%= Html.TextBox("Title", Model.Title) %>
        <%= Html.ValidationMessage("Title", "*") %>
    </p>
    <p>
        <label for="Description">
            ��������:</label>
        <%= Html.TextArea("Description", Model.Description)%>
        <%= Html.ValidationMessage("Description", "*") %>
    </p>
    <p>
        <input type="submit" value="���������" />
    </p>
</fieldset>
<% } %>
