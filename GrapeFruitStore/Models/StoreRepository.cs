using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrapeFruitStore.Models
{
    /*
     * One approach that can make applications easier to maintain and test is to use a “repository” pattern.  A 
     * repository class helps encapsulate data querying and persistence logic, and abstracts away the 
     * implementation details of the data persistence from the application.  In addition to making application 
     * code cleaner, using a repository pattern can make it easier to change data storage implementations in 
     * the future, and it can help facilitate unit testing an application without requiring a real database. 
     * 
     * For our application we’ll define a ProductRepository class:
     * 
     * То есть выходит, что этот класс является внешним классом модели по отношению к контроллеру, и все
     * взаимодействие с ним ведется только через public-методы этого класса
     * 
     * We’ve discussed two different ways to use the built-in model-binding features of ASP.NET MVC.  The
     * first using the UpdateModel() method to update properties on an existing model object, and the
     * second using ASP.NET MVC’s support for passing model objects in as action method parameters.  Both
     * of these techniques are very powerful and extremely useful. 
     * 
     * This power also brings with it responsibility.  It is important to always be paranoid about
     * security when accepting any user input, and this is also true when binding objects to form input.
     * You should be careful to always HTML encode any user-entered values to avoid HTML and JavaScript
     * injection attacks, and be careful of SQL injection attacks (note: we are using LINQ to SQL for our
     * application, which automatically encodes parameters to prevent these types of attacks).  You should
     * never rely on client-side validation alone, and always employ server-side validation to guard
     * against hackers attempting to send you bogus values.
     * 
     * One additional security item to make sure you think about when using the binding features of
     * ASP.NET MVC is the scope of the objects you are binding.  Specifically, you want to make sure you
     * understand the security implications of the properties you are allowing to be bound, and make sure
     * you only allow those properties that really should be updatable by an end-user to be updated.   
     */

    public class StoreRepository
    {
        
        StoreDataContext db = new StoreDataContext();

        //
        // разные методы чтения (выборки)

        public IQueryable<Department> FindAllDepartments()
        {
            return db.Department;
        }

        public IQueryable<Product> FindAllProducts()
        {
            return db.Product;
        }

        public IQueryable<Product> FindProductsByDepartment(int departmentID)
        {
            return db.Product.Where(p => p.DepartmentID == departmentID);
        }

        public Department GetDepartment(int id)
        {
            return db.Department.SingleOrDefault(d => d.DepartmentID == id);
        }

        public Product GetProduct(int id)
        {
            return db.Product.SingleOrDefault(p => p.ProductID == id);
        }

        public IQueryable<Order> FindAllOrders(string username)
        {
            return db.Order.Where(o => o.UserName == username & o.IsShoppingCart == false);
        }

        public Order GetOrder(string username, int id)
        {
            return db.Order.SingleOrDefault(o => o.UserName == username & o.IsShoppingCart == false & o.OrderID == id);
        }

        public Order UNSAFEGetOrder(int id)
        {
            return db.Order.SingleOrDefault(o => o.IsShoppingCart == false & o.OrderID == id);
        }

        public IQueryable<Order> UNSAFEFindOrders()
        {
            return db.Order.Where(o => o.IsShoppingCart == false);
        }

        public Order GetShoppingCart(string username)
        {
            return db.Order.Where(sc => sc.UserName == username & sc.IsShoppingCart == true).SingleOrDefault();
        }

        public OrderItem GetShoppingCartProduct(string username, int productId)
        {
            return db.OrderItem.Where(op => op.Order.UserName == username & op.ProductID == productId & op.Order.IsShoppingCart == true).SingleOrDefault();
        }

        public IQueryable<OrderItem> FindAllOrderItems(string username)
        {
            var orderitems = from orderitem in db.OrderItem
                             where orderitem.Order.UserName == username
                             select orderitem;
            return orderitems;
        }

        //
        // Insert/Delete Methods

        public void Add(Department department)
        {
            db.Department.InsertOnSubmit(department);
        }

        public void Add(Product product)
        {
            db.Product.InsertOnSubmit(product);
        }

        public void Add(Order order)
        {
            db.Order.InsertOnSubmit(order);
        }

        public void Add(OrderItem orderitem)
        {
            db.OrderItem.InsertOnSubmit(orderitem);
        }

        public void Delete(Department department)
        {
            db.Department.DeleteOnSubmit(department);
        }

        public void Delete(Product product)
        {
            db.Product.DeleteOnSubmit(product);
        }

        public void Delete(OrderItem orderitem)
        {
            db.OrderItem.DeleteOnSubmit(orderitem);
        }

        //
        // Пока не вызовем, транзакция не завершится

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}

