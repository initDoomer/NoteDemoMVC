using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteDemoMVC.Models;
using NoteDemoMVC.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Controllers
{
    public class DetailController : Controller
    {

        private readonly ApplicationDbContext _db;

        public DetailController(ApplicationDbContext db)
        {
            _db = db;

        }


        public IActionResult Index()
        {

            /*  get note ids from the session
             * 
             * 
             */
            var context = HttpContext;
            var sessionList = SessionSetterGetter.GetSession(context, WC.NoteSession);

            // get only the ids
            var urgentIds =  sessionList.Select(obj => obj.NoteId).ToList();


            // filter the notes marked as urgent
            IEnumerable <Note> urgentNoteList = _db.Note
                        .Include(u => u.Category)
                         .Include(u => u.Status)
                         .Where(u => urgentIds.Contains(u.Id));
            

            return View(urgentNoteList);
        }
    }
}
