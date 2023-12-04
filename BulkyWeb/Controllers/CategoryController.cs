using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db)
        {
                _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList(); 
            return View(objCategoryList);
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (int.TryParse(obj.Name, out int result) )
            {
                ModelState.AddModelError("name", "The value of Name cannot be all of numbers");
            } 

            if (obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("name" ,"The DisplayOrder cannot exaclty match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            else return View();
        }
    }
}
