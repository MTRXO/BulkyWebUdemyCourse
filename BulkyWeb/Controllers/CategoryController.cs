using Bulky.DataAcces.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork unitofwork)
        {
                _UnitOfWork = unitofwork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _UnitOfWork.Category.GetAll().ToList(); 
            return View(objCategoryList);
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (int.TryParse(obj.Name, out int result))
            {
                ModelState.AddModelError("name", "The value of Name cannot be all of numbers");
            }

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exaclty match the Name");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Save();
                TempData["Succed"] = "Category created succesfully";
                return RedirectToAction("Index");

            }
            else return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id== null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _UnitOfWork.Category.Get(u=>u .Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
      
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Update(obj);
                _UnitOfWork.Save();
                TempData["Succed"] = "Category updated succesfully";
                return RedirectToAction("Index");

            }
            else return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _UnitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _UnitOfWork.Category.Get(u => u.Id == id);

            if (obj == null) 
            {
                return NotFound();

            }
            else
            {
                _UnitOfWork.Category.Remove(obj);
                _UnitOfWork.Save();
                TempData["Succed"] = "Category deleted succesfully";
                return RedirectToAction("Index");
            }
        
     
           
        }

    }


}
