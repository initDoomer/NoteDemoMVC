using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteDemoMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC.Controllers
{
    public class FileDataController : Controller
    {
        ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public FileDataController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            // retrieve list of files from data base

            IEnumerable<FileData> fileList = _db.FileData;

            return View(fileList);
        }


        // get: for create
        public IActionResult Create()
        {
            return View(new FileData());
        }



        // post: for create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FileData obj)
        {
            // steps:   1) get the file from the form , 2) get file name along with extension, 3) get the storage path
            // 4) savefile to server, 5) save the filename into the database

            

            if(!ModelState.IsValid)
            {
                return View(obj);
            }
                string webRootPath = _webHostEnvironment.WebRootPath;

                //
                // creating new product
                string upload = webRootPath + WC.ImagePath; // where to save the images
                string fileName = Guid.NewGuid().ToString(); // generate  a globally unique random name

                //get the extension of the file
                string extension = Path.GetExtension(obj.ImageFileName.FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                        obj.ImageFileName.CopyTo(fileStream); // copy file to the location with the info described above
                }


            obj.ImageName = fileName + extension;
                 _db.FileData.Add(obj); // add product to db

                  _db.SaveChanges();

                //
                return RedirectToAction(nameof(Index));           


        }


        

    }
}
