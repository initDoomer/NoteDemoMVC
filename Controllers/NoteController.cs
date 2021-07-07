using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoteDemoMVC.Models;
using NoteDemoMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NoteController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // retrieve all the notes with there corrsponding category and status

            IEnumerable<Note> noteList = _db.Note
                .Include(u => u.Category)
                .Include(u => u.Status);


            return View(noteList);
        }


        // get: upsert

        public IActionResult Upsert(int? id)
        {
            // if it gets an id it is for Edit
            // if no id it is for create


            NoteVM noteVM = new NoteVM()
            {
                Note = new Note(),

                CategorySelectList = _db.Category.Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                ),

                StatusSelectList = _db.Status.Select(

                    u => new SelectListItem
                    {
                        Text = u.StatusType,
                        Value = u.Id.ToString()
                    }

                    )

            };

            if (id == null || id == 0)
            {
                return View(noteVM);
            }

            noteVM.Note = _db.Note.Find(id);

            if (noteVM.Note == null)
            {
                return NotFound();
            }


            return View(noteVM);
        }


        // post: upsert
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(NoteVM noteVM)
        {

            if (ModelState.IsValid)
            {
                // for create ID will be zero
                if(noteVM.Note.Id == 0 )
                {
                    _db.Note.Add(noteVM.Note);

                }
                else
                {

                    // for edit
                    //updating existing product
                   // var objFromDb = _db.Note.AsNoTracking().FirstOrDefault(u => u.Id == noteVM.Note.Id);


                    _db.Note.Update(noteVM.Note);

                }


                noteVM.CategorySelectList = _db.Category.Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                );

                noteVM.StatusSelectList = _db.Status.Select(

                    u => new SelectListItem
                    {
                        Text = u.StatusType,
                        Value = u.Id.ToString()
                    }

                    );


                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            // if any model state error return the same view with the same data
            return View(noteVM);
        }


        // get: for delete

        public IActionResult Delete(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            Note note = _db.Note
                .Include(u => u.Category)
                .Include(u => u.Status)
                .FirstOrDefault(u=>u.Id == id);

            if (note == null)
            {
                return NotFound();
            }


            return View(note);
        }

        // Post:for Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {



            var obj = _db.Note.Find(id);
            if (obj == null) { return NotFound(); }

           


            _db.Note.Remove(obj);

            // now update the  database
            _db.SaveChanges();

            // redirect category index action
            // since it is in the same controller we do not need to define controller name
            return RedirectToAction("Index");

        }

    }
}
