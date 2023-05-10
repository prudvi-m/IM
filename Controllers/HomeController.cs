using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InventorySystem.Controllers
{
    [CheckSession]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InventoryContext _context;

        public HomeController(ILogger<HomeController> logger, InventoryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string? categoryID, string? warehouseID)
        {
            InventoryView view = new InventoryView();
            if((categoryID == "All" && warehouseID == "All") || (categoryID == null && warehouseID == null))
            {
                view.Products = _context.Products.Include(x => x.Category).Include(x => x.Warehouse).ToList();
            }
            else if(categoryID == "All" && warehouseID != "All")
            {
                view.Products = _context.Products.Include(x => x.Category).Include(x => x.Warehouse).Where(x=>x.WarehouseID == Convert.ToInt32(warehouseID)).ToList();
            }
            else if (categoryID != "All" && warehouseID == "All")
            {
                view.Products = _context.Products.Include(x => x.Category).Include(x => x.Warehouse).Where(x=>x.CategoryID == Convert.ToInt32(categoryID)).ToList();
            }
            else
            {
                view.Products = _context.Products.Include(x => x.Category).Include(x => x.Warehouse).Where(x => x.WarehouseID == Convert.ToInt32(warehouseID)).Where(x => x.CategoryID == Convert.ToInt32(categoryID)).ToList();
            }

            view.Warehouses = _context.Warehouses.ToList();
            view.Categories = _context.Categories.ToList();
            ViewBag.catID = categoryID;
            ViewBag.warID = warehouseID;
            return View(view);
        }

        public IActionResult AddEditProduct(int ? id)
        {
            InventoryView data = new InventoryView();

            if (id != null)
            {
                data.Product = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
                data.Categories = _context.Categories.ToList();
                data.Warehouses = _context.Warehouses.ToList();
                ViewBag.btnval = "Update";
                return View(data);
            }
            else
            {
                data.Product = new Product();
                data.Categories = _context.Categories.ToList();
                data.Warehouses = _context.Warehouses.ToList();
                ViewBag.btnval = "Add";
                return View(data);
            }
        }

        [HttpPost]
        public IActionResult AddEditProduct(Product product)
        {
            if(product.ProductId == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var data = _context.Products.Where(x=>x.ProductId == product.ProductId).FirstOrDefault();
                data.ProductName = product.ProductName;
                data.ProductCode = product.ProductCode;
                data.Vendor = product.Vendor;
                data.Price = product.Price;
                data.CategoryID = product.CategoryID;
                data.WarehouseID = product.WarehouseID;
                data.Description = product.Description;
                data.Quantity = product.Quantity;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int ? id)
        {
            var data = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
            _context.Products.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }

        public IActionResult EditQuantity(int ID, int Quantity)
        {
            var data = _context.Products.Where(x => x.ProductId == ID).FirstOrDefault();
            data.Quantity = Quantity;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Category()
        {
            var data = _context.Categories.ToList();
            return View(data);
        }

        public IActionResult Warehouse()
        {
            var data = _context.Warehouses.ToList();
            return View(data);
        }

        public IActionResult AddEditcategory(int? id)
        {
            Category category = null;
            if (id != null)
            {
                ViewBag.btnval = "Update";
                category = _context.Categories.Where(x=>x.Id == id).FirstOrDefault();
            }
            else
            {
                ViewBag.btnval = "Add";
                category = new Category();
            }

            return View(category);
        }
        public IActionResult AddEditWarehouse(int? id)
        {
            Warehouse warehouse = null;
            if (id != null)
            {
                ViewBag.btnval = "Update";
                warehouse = _context.Warehouses.Where(x => x.ID == id).FirstOrDefault();
            }
            else
            {
                ViewBag.btnval = "Add";
                warehouse = new Warehouse();
            }

            return View(warehouse);
        }

        public IActionResult SaveCategory(Category category)
        {
            if(category.Id == 0)
            {
                    _context.Categories.Add(category);
            }
            else
            {
                var data = _context.Categories.Where(x => x.Id == category.Id).FirstOrDefault();
                data.CategoryName = category.CategoryName;
            }
            _context.SaveChanges();
            return RedirectToAction("Category");
        }


        public IActionResult SaveWarehouse(Warehouse warehouse)
        {
            if(warehouse.ID == 0)
            {
                _context.Warehouses.Add(warehouse);
            }
            else
            {
                var data = _context.Warehouses.Where(x=>x.ID == warehouse.ID).FirstOrDefault();
                data.Email = warehouse.Email;
                data.ContactPerson = warehouse.ContactPerson;
                data.Phone = warehouse.Phone;
                data.Location = warehouse.Location;
            }
            _context.SaveChanges();
            return RedirectToAction("Warehouse");
        }

        public IActionResult DeleteCategory(int? id)
        {
            var checkdata = _context.Categories.Where(x => x.Id == id).ToList();
            if(checkdata.Count == 0)
            {

                var data = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
                _context.Categories.Remove(data);
                _context.SaveChanges();
            }
            else
            {
                ViewBag.err = "Cannot Delete Category";
            }
            var category = _context.Categories.ToList();
            return View("Category", category);
        }

        public IActionResult Deletewarehouse(int? id)
        {
            var Checkdata = _context.Warehouses.Where(x => x.ID == id).ToList();
            if(Checkdata.Count == 0)
            {

                var data = _context.Warehouses.Where(x => x.ID == id).FirstOrDefault();
                _context.Warehouses.Remove(data);
                _context.SaveChanges();
            }
            else
            {
                ViewBag.err = "Cannot Delete Warehouse";
            }
            var warehouse = _context.Warehouses.ToList();
                return View("Warehouse", warehouse);
            
        }

        public IActionResult Account(int? id)
        {
            ViewDetails view = new ViewDetails();
            var userID = HttpContext.Session.GetString("UserID");
            var RoleID = HttpContext.Session.GetString("RoleID");
            if(RoleID == "2")
            {
                if(id != 0)
                {
                    ViewBag.btn = "Update";
                    view.Account = _context.Accounts.Include(x => x.Role).Where(x => x.ID == id).FirstOrDefault();
                }
                else
                {
                    ViewBag.btn = "Add";
                    view.Account = new Account();
                }
                view.Roles = _context.Roles.ToList();
            }
            else
            {
                ViewBag.btn = "Update";
                view.Account = _context.Accounts.Include(x => x.Role).Where(x => x.ID == Int32.Parse(userID)).FirstOrDefault();

            }

            return View(view);
        }
        
        public IActionResult AccountList()
        {
            var data = _context.Accounts.Include(x => x.Role).ToList();
            return View(data);
        }

        public IActionResult AddEditAccount(Account account)
        {
            if(account.ID == 0)
            {
                _context.Accounts.Add(account);
            }
            else
            {
                var roleID = HttpContext.Session.GetString("RoleID");
                var data = _context.Accounts.Where(x => x.ID == account.ID).FirstOrDefault();
                data.FirstName = account.FirstName;
                data.LastName = account.LastName;
                data.Email = account.Email;
                data.Password = account.Password;
            }
            _context.SaveChanges();

            var RoleID = HttpContext.Session.GetString("RoleID");
            if (RoleID == "2")
            {
                return RedirectToAction("AccountList");

            }
            else
            {
                return RedirectToAction("Account");

            }
        }

        public IActionResult AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return RedirectToAction("AccountList");
        }

        public IActionResult DeleteAccount(int? id)
        {
            var data = _context.Accounts.Where(x => x.ID == id).FirstOrDefault();
            _context.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("AccountList");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckName(string CategoryName)
        {
            bool exis = _context.Categories.Any(x => x.CategoryName == CategoryName);
            if (exis)
                return Json(data: false);
            else
                return Json(data: true);

        }

        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> CheckWarehouse(string Location)
        {
            bool exists = await _context.Warehouses.AnyAsync(x => x.Location == Location);
            if (exists)
                return Json(data: false);
            else
                return Json(data: true);
        }

        public IActionResult CheckProductCode(string ProductCode)
       {
            bool exists =  _context.Products.Any(x => x.ProductCode == ProductCode);
            if (exists)
                return Json(data: false);
            else
                return Json(data: true);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}