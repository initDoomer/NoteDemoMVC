using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoteDemoMVC.Models;
using NoteDemoMVC.Models.ViewModels;
using NoteDemoMVC.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoteDemoMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;



        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)

        {
            _db = db;
            _logger = logger;
        }


        // get: show all the notes
        public IActionResult Index()
        {
            var context = HttpContext;
            // get all the notes from the db and pass to the view the detailVM

            IEnumerable<Note> noteList = _db.Note
                                        .Include(u => u.Category)
                                         .Include(u => u.Status);


            
            List<DetailVM> detailList = new List<DetailVM>();
            var sessionList = SessionSetterGetter.GetSession(context, WC.NoteSession);


           if (sessionList == null)
            {
                sessionList = new List<UrgentNote>();
            }
            else{
                sessionList = sessionList.ToList();
                
            }
            foreach (var note in noteList)
            {
                detailList.Add(new DetailVM()
                {
                    Note = note,
                    IsImportant = sessionList.Contains(sessionList.FirstOrDefault(u => u.NoteId == note.Id))
                }
                    );
            }


            // get logged in user id 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var ApplicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            int x = 10;

             return View(detailList);
           




            
        }


        // add to urgent


        public IActionResult AddToUrgent(int id)
        {
            // add to the session
            List<UrgentNote> urgentNoteList = new List<UrgentNote>();
            var context = HttpContext;

            if (SessionSetterGetter.GetSession(context,WC.NoteSession)!=null
                && SessionSetterGetter.GetSession(context,WC.NoteSession).Count() >0
                )
            {
                urgentNoteList = SessionSetterGetter.GetSession(context, WC.NoteSession).ToList();
            }

            var tempNote = urgentNoteList.FirstOrDefault(u => u.NoteId == id);

            if (tempNote != null)
            {
                // remove if it exists 

                urgentNoteList.Remove(tempNote);

            }
            else
            {

                    urgentNoteList.Add(new UrgentNote()
                    {
                        NoteId = id
                    });
            }


            SessionSetterGetter.SetSession(context, WC.NoteSession, urgentNoteList);


            return RedirectToAction(nameof(Index));
        }



        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
