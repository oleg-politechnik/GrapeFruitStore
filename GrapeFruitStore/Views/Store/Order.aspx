<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GrapeFruitStore.Models.Order>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(String.Format("GrapeFruit Store - ����� ������������ {0} �� {1}, {2}",
        Model.UserName, Model.AddedDate.ToShortDateString(), Model.AddedDate.ToShortTimeString())) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%= Html.Encode(String.Format("GrapeFruit Store - ����� �� {0}, {1}",
            Model.AddedDate.ToShortDateString(), Model.AddedDate.ToShortTimeString())) %></h2>
    <% Html.RenderPartial("StatusMessage"); %>
    <p>
    </p>
    <table>
        <tr>
            <th>
                �
            </th>
            <th>
                �����
            </th>
            <th>
                ������������
            </th>
            <th>
                ����
            </th>
            <th>
                ����������
            </th>
            <th>
                ���������
            </th>
        </tr>
        <% int i = 1; decimal total = 0; foreach (var item in Model.OrderItem)
           { %>
        <tr>
            <td>
                <%= Html.Encode(i) %>
            </td>
            <td>
                <%= Html.ActionLink(item.Product.Department.Title, "Department", new { id = item.Product.DepartmentID })%>
            </td>
            <td>
                <%= Html.ActionLink(item.Product.Title, "Product", new { id = item.ProductID })%>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F}", item.UnitPrice)) %>
            </td>
            <td>
                <%= Html.Encode(item.Quantity) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:F} ���.", item.UnitPrice * item.Quantity)) %>
            </td>
        </tr>
        <% i++;
           total += item.UnitPrice * item.Quantity;
           } %>
    </table>
    <p>
        <%= Html.Encode(String.Format("�����: {0:F} ���.", total))%>
    </p>
    <%--<p>
        <%= Html.Encode(String.Format("������ ������: {0}.", Model.Status))%>
    </p>--%>
    <p>
        <%= Html.ActionLink("� �������", "Order", new { id = "" }, new { @class = "button" })%>
    </p>
</asp:Content>
