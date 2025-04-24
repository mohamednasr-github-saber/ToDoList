using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class To_DoController : Controller
    {
        private readonly ApplicationDbContext _Context=new ();
        public IActionResult Index()
        { 
            return View();
        }
        public IActionResult Create(string ? name)
        {
            var items = _Context.Items;
            ViewBag.Name = name;
            return View(items.ToList());
        }
        public IActionResult CreateNew()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNew(Item item)
        {
            if (ModelState.IsValid)
            {
                _Context.Items.Add(item);
                _Context.SaveChanges();

                return RedirectToAction(nameof(Create));
            }

            return View(item);
        }
        public IActionResult Edit(int id)
        {
            var item = _Context.Items.Find(id);

            if (item is not null)
            {
                return View(item);
            }

            return RedirectToAction(nameof(Create));
        }
        [HttpPost]
        public IActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                _Context.Items.Update(item);
                _Context.SaveChanges();
                return RedirectToAction(nameof(Create));
            }

            return View(item);
        }
        public IActionResult Delete(int id)
        {
            var item = _Context.Items.Find(id);

            if (item is not null)
            {
                _Context.Remove(item);
                _Context.SaveChanges();

                return RedirectToAction(nameof(Create));
            }

            return Content ("no page ");
        }
    }


    
}
