using Microsoft.AspNetCore.Mvc;
using NoteDemoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Controllers
{
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StatusController(ApplicationDbContext db)
        {
            _db = db;
        }

        // required view all category, create category, edit caterogy, delete category
        public IActionResult Index()
        {

            IEnumerable<Status> categoryList = _db.Status;
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
        public IActionResult Create(Status obj)
        {

            if (ModelState.IsValid)
            {
                _db.Status.Add(obj);
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
            if (id == null || id == 0)
            {
                return NotFound();

            }

            Status status = _db.Status.Find(id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // post :for edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Status obj)
        {

            if (ModelState.IsValid)
            {
                _db.Status.Update(obj);
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
            var obj = _db.Status.Find(id);
            if (obj == null) { return NotFound(); }
            return View(obj);
        }

        // Post:for Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStatus(int? id)
        {



            if (id != null && id != 0)
            {
                var obj = _db.Status.Find(id);
                if (obj == null) { return NotFound(); }

                _db.Status.Remove(obj);

                // now update the  database
                _db.SaveChanges();

                // redirect category index action
                // since it is in the same controller we do not need to define controller name
                return RedirectToAction(nameof(Index));
            }
            return NotFound(); // return to the same create page with the data if invalid state
        }
    }
}
