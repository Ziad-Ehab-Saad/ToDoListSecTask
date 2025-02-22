using Microsoft.AspNetCore.Mvc;
using ToDoListSecTask.DataAccess;
using ToDoListSecTask.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoListSecTask.Controllers
{
    public class ToDoController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var data = context.toDoLists.ToList();

            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateItem(ToDoList data, IFormFile file)
        {

            if (data.DeadLine < DateTime.Now)
            {
                ModelState.AddModelError("DeadLine", "The deadline should be in the future.");
            }

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    data.File = fileName;

                }

                context.toDoLists.Add(data);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }


            return View(nameof(Create), data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = context.toDoLists.FirstOrDefault(e => e.id == id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }


        [HttpPost]
        public IActionResult Edit(ToDoList dataReq, IFormFile file)
        {
            var DataInDb = context.toDoLists.FirstOrDefault(e => e.id == dataReq.id);
            if (DataInDb == null)
            {
                return NotFound();

            }
            if (file == null)
            {
                ModelState.Remove("File");
            }
            if (dataReq.DeadLine < DateTime.Now)
            {
                ModelState.AddModelError("DeadLine", "The deadline should be in the future.");
            }

            if (ModelState.IsValid)
            {
                DataInDb.Name = dataReq.Name;
                DataInDb.Description = dataReq.Description;
                DataInDb.DeadLine = dataReq.DeadLine;

                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    DataInDb.File = fileName;
                }

                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            dataReq.File = DataInDb.File;
            return View(dataReq);
        }


        public IActionResult Delete(int id)
        {
            var data = context.toDoLists.FirstOrDefault(e => e.id == id);
            if (data != null)
            {
                context.toDoLists.Remove(data);
                context.SaveChanges();
            }
            return RedirectToAction("Index");



        }
    }
}
