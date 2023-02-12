using Microsoft.AspNetCore.Mvc;
using BulkyBook2.Data;
using Microsoft.EntityFrameworkCore;
using BulkyBook2.Models;

namespace BulkyBook2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationContextDb _db;
        public CategoryController(ApplicationContextDb db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category a)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Add(a);
                _db.SaveChanges();
                TempData["success"] = "Create Category is successfully!!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            var categoryID = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryID == null)
                return NotFound();
            return View(categoryID); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category a)
        {
            if(a.Name==a.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(a);
                _db.SaveChanges();
                TempData["success"] = "Update Category is successfully!!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            var categoryID = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryID == null)
                return NotFound();
            return View(categoryID);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var a = _db.Categories.Find(id);
            if(a==null)
            {
                return NotFound();
            }
                _db.Categories.Remove(a);
            TempData["success"] = "Delete Category is successfully!!";
                _db.SaveChanges();
                return RedirectToAction("Index");
            
            
        }
    }
}
