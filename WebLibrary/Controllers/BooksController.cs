using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace WebLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationContext _context;

        public BooksController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Books.ToList());
        }

        [Authorize (Roles = "librian")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "librian")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book { Name = model.Name, Genre = model.Genre, Author = model.Author, Publisher = model.Publisher, isBooked = false, WhoBooked = null, CurrentHolder = null, Users = null };
                _context.Books.Add(book);
                _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            
            return View("~/Views/Account/SomeError.cshtml");
        }

        [Authorize(Roles = "librian")]
        public IActionResult Edit(int id)
        {
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            EditBookViewModel model = new EditBookViewModel { Id = book.Id, Name = book.Name, Genre = book.Genre, Author = book.Author, Publisher = book.Publisher, isBooked = book.isBooked, WhoBooked = book.WhoBooked, CurrentHolder = book.CurrentHolder };
            return View(model);
        }

        [Authorize(Roles = "librian")]
        [HttpPost]
        public IActionResult Edit(EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = _context.Books.Find(model.Id);
                if (book != null)
                {
                    book.Name = model.Name;
                    book.Genre = model.Genre;
                    book.Author = model.Author;
                    book.Publisher = model.Publisher;
                    book.isBooked = model.isBooked;
                    book.WhoBooked = model.WhoBooked;
                    book.CurrentHolder = model.CurrentHolder;
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
