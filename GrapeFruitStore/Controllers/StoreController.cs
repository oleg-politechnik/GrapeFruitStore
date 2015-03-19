using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Profile;
using GrapeFruitStore.Models;
using GrapeFruitStore.Helpers;

namespace GrapeFruitStore.Controllers
{
    /*
     * In the scenario above, our DepartmentFormViewModel class directly exposes the Dinner model
     * object as a property, along with a supporting SelectList model property. This approach
     * works fine for scenarios where the HTML UI we want to create within our view template
     * corresponds relatively closely to our domain model objects.
     * 
     * For scenarios where this isn’t the case, one option that you can use is to create a
     * custom-shaped ViewModel class whose object model is more optimized for consumption by
     * the view – and which might look completely different from the underlying domain model
     * object. For example, it could potentially expose different property names and/or
     * aggregate properties collected from multiple model objects.
     * 
     * Custom-shaped ViewModel classes can be used both to pass data from controllers to views
     * to render, as well as to help handle form data posted back to a controller’s action
     * method. For this later scenario, you might have the action method update a ViewModel
     * object with the form-posted data, and then use the ViewModel instance to map or retrieve
     * an actual domain model object.
     * 
     * Custom-shaped ViewModel classes can provide a great deal of flexibility, and are
     * something to investigate any time you find the rendering code within your view templates
     * or the form-posting code inside your action methods starting to get too complicated.
     * This is often a sign that your domain models don’t cleanly correspond to the UI you are
     * generating, and that an intermediate custom-shaped ViewModel class can help.
     * 
     */

    public class StoreViewModel
    {
        public Department Department { get; private set; }
        public IQueryable<Product> ProductsList { get; private set; }

        public StoreViewModel(Department department, IQueryable<Product> productsList)
        {
            Department = department;
            ProductsList = productsList;
        }
    }

    /*
     * Response.Write() -- реальное зло
     * 
     * Необходимо имплементировать всю логику работы с данными внутри методов класса DepartmentController,
     * а потом передавать аргументы, необходимые для отрисовки ХТМЛ, отдельному шаблону "вид", который и
     * генерит всю эту визуальную байду.  Этот шаблон содержит разметку+код_для_разметки, но это пофик,
     * т.к. вся бизнес-логика все равно в контроллере.
     * 
     * В это классе DepartmentController каждый раз возвращаем в УИ тот шаблон визуального ХТМЛ представления,
     * который надо (его тип ActionResult).  We can then call the View() helper 
     * method on the Controller base class to return back a “ViewResult” object: <- вот это было впечатляюще, ага
     * 
     */

    [HandleError]
    public class StoreController : Controller
    {
        StoreRepository storeRepository = new StoreRepository();

        //
        // GET: /Store

        public ActionResult Index()
        {
            ViewData["AtStore"] = "selected";
            var departments = storeRepository.FindAllDepartments();
            return View(departments);
        }

        //
        // GET: /Store/Department/{!important id}

        public ActionResult Department(int? id)
        {
            ViewData["AtStore"] = "selected";
            Department department = storeRepository.GetDepartment(id.Value);
            if (department == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            StoreViewModel storeView = new StoreViewModel(department, storeRepository.FindProductsByDepartment(id.Value));
            return View(storeView);
        }

        //
        // GET: /Store/Product/{!important id}

        public ActionResult Product(int? id)
        {
            ViewData["AtStore"] = "selected";
            Product product = storeRepository.GetProduct(id.Value);
            if (product == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            return View(product);
        }

        //
        // GET: /Store/Order/{id}

        [Authorize]
        public ActionResult Order(int? id)
        {
            ViewData["AtOrder"] = "selected";
            if (!id.HasValue)
            {
                IEnumerable<Order> order;
                if (User.IsInRole(Constants.AdministratorsRoleName))
                {
                    order = storeRepository.UNSAFEFindOrders();    //ROOT ONLY!!!!!!! очень опасная штука
                }
                else
                {
                    order = storeRepository.FindAllOrders(User.Identity.Name);
                }
                return View("Orders", order);
            }
            else
            {
                Order order;
                if (User.IsInRole(Constants.AdministratorsRoleName))
                {
                    order = storeRepository.UNSAFEGetOrder(id.Value);    //ROOT ONLY!!!!!!! очень опасная штука
                }
                else
                {
                    order = storeRepository.GetOrder(User.Identity.Name, id.Value);
                }
                if (order == null)
                {
                    throw new HttpException(404, "Страница не найдена");
                }
                return View(order);
            }
        }

        #region Root

        #region Department

        //
        // GET: /Store/CreateDepartment

        [Authorize]
        public ActionResult CreateDepartment()
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Department department = new Department();
            return View(department);
        }

        //
        // POST: /Store/CreateDepartment

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult CreateDepartment(Department department)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            try
            {
                storeRepository.Add(department);
                storeRepository.Save();
                TempData["SuccessMessage"] = String.Format("Отдел \"{0}\" успешно создан.", department.Title);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ModelState.AddModelErrors(department.GetRuleViolations());
            }
            return View(department);
        }

        //
        // GET: /Store/EditDepartment/{!important id}

        [Authorize]
        public ActionResult EditDepartment(int id)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Department department = storeRepository.GetDepartment(id);
            if (department == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            return View(department);
        }

        //
        // POST: /Store/EditDepartment/{!important id}

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult EditDepartment(int id, FormCollection formValues)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Department department = storeRepository.GetDepartment(id);
            if (department == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            try
            {
                UpdateModel(department);
                /*
                 * The UpdateModel() helper method automatically populates the ModelState collection
                 * when it encounters errors while trying to assign form values to properties on the
                 * model object.  For example, our Dinner object’s EventDate property is of type
                 * DateTime.  When the UpdateModel() method was unable to assign the string value
                 * “BOGUS” to it in the scenario above, the UpdateModel() method added an entry to
                 * the ModelState collection indicating an assignment error had occurred with that property.
                 * 
                 * что за траву они курили?? совсем все не так происходит...
                 */
                storeRepository.Save();
                TempData["SuccessMessage"] = "Изменения сохранены.";
                return RedirectToAction("Department", new { id = department.DepartmentID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ModelState.AddModelErrors(department.GetRuleViolations());
            }
            return View(department);
        }

        //
        // GET: /Store/DeleteDepartment/{!important id}

        [Authorize]
        public ActionResult DeleteDepartment(int id)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Department department = storeRepository.GetDepartment(id);
            if (department == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            return View(department);
        }

        //  
        // POST: /Store/DeleteDepartment/{!important id}

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult DeleteDepartment(int id, string confirmButton)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Department department = storeRepository.GetDepartment(id);
            if (department == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            if (department.Product.Count != 0)
            {
                TempData["ErrorMessage"] = "Нельзя удалить непустой отдел.";
                return RedirectToAction("Department", new { id = department.DepartmentID });
            }
            try
            {
                storeRepository.Delete(department);
                storeRepository.Save();
                TempData["SuccessMessage"] = "Отдел удален.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(department);
        }

        #endregion

        #region Product

        //
        // GET: /Store/CreateProduct/{!important departmentId}

        [Authorize]
        public ActionResult CreateProduct(int Department)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Department department = storeRepository.GetDepartment(Department);
            if (department == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Product product = new Product();
            product.Department = department;
            return View(product);
        }

        //
        // POST: /Store/CreateProduct/{!important departmentId}

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult CreateProduct(Product product)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Department department = storeRepository.GetDepartment(product.DepartmentID);
            if (department == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            product.Department = department;
            try
            {

                storeRepository.Add(product);
                storeRepository.Save();
                TempData["SuccessMessage"] = String.Format("Товар \"{0}\" успешно создан.", product.Title);
                return RedirectToAction("Product", new { id = product.ProductID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ModelState.AddModelErrors(product.GetRuleViolations());
            }
            return View(product);
        }

        //
        // GET: /Store/EditProduct/{!important id}

        [Authorize]
        public ActionResult EditProduct(int id)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Product product = storeRepository.GetProduct(id);
            if (product == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            return View(product);
        }

        //
        // POST: /Store/EditProduct/{!important id}

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult EditProduct(int id, FormCollection formValues)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Product product = storeRepository.GetProduct(id);
            if (product == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            try
            {
                UpdateModel(product);
                storeRepository.Save();
                TempData["SuccessMessage"] = "Изменения сохранены.";
                return RedirectToAction("Product", new { id = product.ProductID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ModelState.AddModelErrors(product.GetRuleViolations());
            }
            return View(product);
        }

        //
        // GET: /Store/DeleteProduct/{!important id}

        [Authorize]
        public ActionResult DeleteProduct(int id)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Product product = storeRepository.GetProduct(id);
            if (product == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            return View(product);
        }

        //  
        // POST: /Store/DeleteProduct/{!important id}

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult DeleteProduct(int id, string confirmButton)
        {
            ViewData["AtStore"] = "selected";
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                throw new HttpException(404, "Страница не найдена");
            }
            Product product = storeRepository.GetProduct(id);
            if (product == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            try
            {
                storeRepository.Delete(product);
                storeRepository.Save();
                TempData["SuccessMessage"] = "Товар удален.";
                return RedirectToAction("Department", new { id = product.DepartmentID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(product);
        }

        #endregion

        /*#region Order

        //
        // GET: /Store/CreateProduct/{!important departmentId}

        [Authorize]
        public ActionResult CreateProduct(int Department)
        {
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                ViewData["Message"] = Constants.UserAccessForbiddenMessage;
                return View("Error");
            }
            Department department = storeRepository.GetDepartment(Department);
            if (department == null)
            {
                ViewData["Message"] = "Отдел не найден или закрыт на переучет.";
                return View("NotFound");
            }
            Product product = new Product();
            product.Department = department;
            return View(product);
        }

        //
        // POST: /Store/CreateProduct/{!important departmentId}

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult CreateProduct(Product product)
        {
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                ViewData["Message"] = Constants.UserAccessForbiddenMessage;
                return View();
            }
            Department department = storeRepository.GetDepartment(product.DepartmentID);
            if (department == null)
            {
                ViewData["Message"] = "Отдел не найден или закрыт на переучет.";
                return View("NotFound");
            }
            product.Department = department;
            try
            {

                storeRepository.Add(product);
                storeRepository.Save();
                TempData["SuccessMessage"] = String.Format("Товар \"{0}\" успешно создан.", product.Title);
                return RedirectToAction("Product", new { id = product.ProductID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ModelState.AddModelErrors(product.GetRuleViolations());
            }
            return View(product);
        }

        //
        // GET: /Store/EditProduct/{!important id}

        [Authorize]
        public ActionResult EditProduct(int id)
        {
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                ViewData["Message"] = Constants.UserAccessForbiddenMessage;
                return View("Error");
            }
            Product product = storeRepository.GetProduct(id);
            if (product == null)
            {
                ViewData["Message"] = Constants.ProductNotFound;
                return View("NotFound");
            }
            return View(product);
        }

        //
        // POST: /Store/EditProduct/{!important id}

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult EditProduct(int id, FormCollection formValues)
        {
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                ViewData["Message"] = Constants.UserAccessForbiddenMessage;
                return View();
            }
            Product product = storeRepository.GetProduct(id);
            if (product == null)
            {
                ViewData["Message"] = Constants.ProductNotFound;
                return View("NotFound");
            }
            try
            {
                UpdateModel(product);
                storeRepository.Save();
                TempData["SuccessMessage"] = "Изменения сохранены.";
                return RedirectToAction("Product", new { id = product.ProductID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ModelState.AddModelErrors(product.GetRuleViolations());
            }
            return View(product);
        }

        //
        // GET: /Store/DeleteProduct/{!important id}

        [Authorize]
        public ActionResult DeleteProduct(int id)
        {
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                ViewData["Message"] = Constants.UserAccessForbiddenMessage;
                return View("Error");
            }
            Product product = storeRepository.GetProduct(id);
            if (product == null)
            {
                ViewData["Message"] = Constants.ProductNotFound;
                return View("NotFound");
            }
            return View(product);
        }

        //  
        // POST: /Store/DeleteProduct/{!important id}

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult DeleteProduct(int id, string confirmButton)
        {
            if (!User.IsInRole(Constants.AdministratorsRoleName))
            {
                ViewData["Message"] = Constants.UserAccessForbiddenMessage;
                return View("Error");
            }
            Product product = storeRepository.GetProduct(id);
            if (product == null)
            {
                ViewData["Message"] = Constants.ProductNotFound;
                return View("NotFound");
            }
            try
            {
                storeRepository.Delete(product);
                storeRepository.Save();
                TempData["SuccessMessage"] = "Товар удален.";
                return RedirectToAction("Department", new { id = product.DepartmentID });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View(product);
        }

        #endregion*/

        #endregion

        #region Shopping Cart

        [Authorize]
        public ActionResult ShoppingCart()
        {
            ViewData["AtShoppingCart"] = "selected";
            Order shoppingcart = storeRepository.GetShoppingCart(User.Identity.Name);
            if (shoppingcart == null)                         //этот ахтунг зарегался, но еще не отоваривался
            {
                try
                {
                    shoppingcart = new Order()
                    {
                        IsShoppingCart = true,
                        UserName = User.Identity.Name,
                        AddedDate = DateTime.Now,
                        Status = "В процессе"
                    };
                    storeRepository.Add(shoppingcart);
                    storeRepository.Save();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToRoute("Default");
                }
            }
            return View(shoppingcart);
        }

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult AddToShoppingCart(int productId, int? quantity)
        {
            ViewData["AtShoppingCart"] = "selected";
            Order shoppingcart = storeRepository.GetShoppingCart(User.Identity.Name);
            if (shoppingcart == null)                         //этот ахтунг зарегался, но еще не отоваривался
            {
                try
                {
                    shoppingcart = new Order()
                    {
                        IsShoppingCart = true,
                        UserName = User.Identity.Name,
                        AddedDate = DateTime.Now,
                        Status = "В процессе"
                    };
                    storeRepository.Add(shoppingcart);
                    storeRepository.Save();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToRoute("Default");
                }
            }
            Product product = storeRepository.GetProduct(productId);
            if (product == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            OrderItem shoppingcartitem = storeRepository.GetShoppingCartProduct(shoppingcart.UserName, productId);
            try
            {
                if (shoppingcartitem != null)
                {
                    shoppingcartitem.Quantity += quantity ?? 1;
                    UpdateModel(shoppingcartitem);
                }
                else
                {
                    shoppingcartitem = new OrderItem()
                    {
                        OrderID = shoppingcart.OrderID,
                        ProductID = productId,
                        Quantity = quantity ?? 1,
                        UnitPrice = storeRepository.GetProduct(productId).Price
                    };
                    storeRepository.Add(shoppingcartitem);
                }
                storeRepository.Save();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("ShoppingCart");
        }

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult CreateOrder(string createorder)
        {
            Order order = storeRepository.GetShoppingCart(User.Identity.Name);
            if (order == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            if (order.OrderItem.ToList().Count == 0)
            {
                throw new HttpException(404, "Корзина пуста");
            }
            Product product;
            try
            {
                foreach (OrderItem orderitem in order.OrderItem)
                {
                    product = storeRepository.GetProduct(orderitem.ProductID);
                    if (orderitem.Quantity > product.UnitsInStock)
                    {
                        throw new Exception(String.Format("На складе только {0} шт. товара \"{1}\".", product.UnitsInStock, orderitem.Product.Title));
                    }
                    else
                    {
                        product.UnitsInStock -= orderitem.Quantity;
                        UpdateModel(product);
                        storeRepository.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ShoppingCart");
            }
            order.AddedDate = DateTime.Now;
            order.IsShoppingCart = false;
            Order shoppingcart = new Order()
            {
                IsShoppingCart = true,
                UserName = User.Identity.Name,
                AddedDate = DateTime.Now,
                Status = "В процессе"
            };
            try
            {
                UpdateModel(order);
                storeRepository.Add(shoppingcart);
                storeRepository.Save();
                TempData["SuccessMessage"] = "Заказ принят.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("Order");
        }

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult ChangeShoppingCart(int ProductID, string Quantity, string change)
        {
            int _orderquantity;
            if (!int.TryParse(Quantity, out _orderquantity))
            {
                TempData["ErrorMessage"] = String.Format("Неверное значение \"{0}\".", Quantity);
                return RedirectToAction("ShoppingCart");
            }
            OrderItem shoppingcartitem = storeRepository.GetShoppingCartProduct(User.Identity.Name, ProductID);
            if (shoppingcartitem == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            int unitsinstock = storeRepository.GetProduct(ProductID).UnitsInStock;
            try
            {
                shoppingcartitem.Quantity = _orderquantity;
                UpdateModel(shoppingcartitem);
                storeRepository.Save();
                TempData["SuccessMessage"] = "Количество изменено.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("ShoppingCart");
        }

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult DeleteShoppingCartItem(int productid, string delete)
        {
            OrderItem shoppingcartitem = storeRepository.GetShoppingCartProduct(User.Identity.Name, productid);
            if (shoppingcartitem == null)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            try
            {
                storeRepository.Delete(shoppingcartitem);
                storeRepository.Save();
                TempData["SuccessMessage"] = "Товар удален.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction("ShoppingCart");
        }

        #endregion
    }
}
