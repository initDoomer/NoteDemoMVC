using Microsoft.AspNetCore.Mvc;
using NoteDemoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // required view all category, create category, edit caterogy, delete category
        public IActionResult Index()
        {

            IEnumerable<Category> categoryList = _db.Category;
            return View(categoryList);
        }


        // get: for create
        public IActionResult Create()
        {

            return View();
        }


        // post: for create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Category.Add(obj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                return View(obj);
            }

        }


        // get: for edit
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();

            }

            Category category = _db.Category.Find(id);
            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // post :for edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        // Get:for Delete
        public IActionResult Delete(int? id)
        {

            // retrieve a particular category from the db and 
            // display the edit view with the category to edit
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // find method only works on the primary key
            var obj = _db.Category.Find(id);
            if (obj == null) { return NotFound(); }
            return View(obj);
        }

        // Post:for Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {



            if (id != null && id != 0)
            {
                var obj = _db.Category.Find(id);
                if (obj == null) { return NotFound(); }

                _db.Category.Remove(obj);

                // now update the  database
                _db.SaveChanges();

                // redirect category index action
                // since it is in the same controller we do not need to define controller name
                return RedirectToAction("Index");
            }
            return NotFound(); // return to the same create page with the data if invalid state
        }

    }
}
