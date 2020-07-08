using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TodoWebApp.Models;

namespace TodoWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var uid = User.Identity.GetUserId();
            var viewModel = _context.Todo.Where(t => t.UserId == uid);
            if (viewModel == null)
                throw new HttpException(404, "Not found");
            return View(viewModel);
        }

        public ActionResult Detail(int id)
        {
            var uid = User.Identity.GetUserId();
            var todo = _context.Todo.SingleOrDefault(t => t.Id == id && t.UserId == uid);
            if (todo == null)
                throw new HttpException(404, "Not found");
            return View(todo);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Todo todo)
        {
            if (!ModelState.IsValid) return View(todo);
            todo.UserId = User.Identity.GetUserId();
            _context.Todo.Add(todo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var uid = User.Identity.GetUserId();
            var todo = _context.Todo.SingleOrDefault(t => t.Id == id && t.UserId == uid);
            if (todo == null)
                throw new HttpException(404, "Not found");
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Todo todo)
        {
            if (!ModelState.IsValid) return View(todo);
            var uid = User.Identity.GetUserId();
            var todoInDb = _context.Todo.SingleOrDefault(t => t.Id == todo.Id && t.UserId == uid);
            todoInDb.Name = todo.Name;
            todoInDb.Content = todo.Content;
            todoInDb.IsDone = todo.IsDone;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var uid = User.Identity.GetUserId();
            var todo = _context.Todo.SingleOrDefault(t => t.Id == id && t.UserId == uid);
            if (todo == null)
                throw new HttpException(404, "Not found");
            _context.Todo.Remove(todo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}