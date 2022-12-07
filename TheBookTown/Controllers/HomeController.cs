using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheBookTown.Models;
using TheBookTown.ViewModels;

namespace TheBookTown.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public HomeController(IBookRepository bookRepository,
                             IWebHostEnvironment hostingEnvironment)
        {
            _bookRepository = bookRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        public ViewResult Index()
        {
            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("test project", "rohityadavtesting@gmail.com"));
            //message.To.Add(new MailboxAddress("test project", "rohityadavtesting@gmail.com"));
            //message.Subject = "test mail from .net core";
            //message.Body = new TextPart("plain")
            //{
            //    Text = "hello world mail"
            //};

            //using(var client =new SmtpClient())
            //{
            //    client.Connect("smtp.gmail.com", 587, false);
            //    client.Authenticate("rohityadavtesting@gmail.com", "");
            //    client.Send(message);
            //    client.Disconnect(true);
            //}


            var model = _bookRepository.GetAllBook();
            return View(model);
            //return View();
        }

        public ViewResult ViewAllBooks()
        {
            var model = _bookRepository.GetAllBook();
            return View(model);

        }
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Book = _bookRepository.GetBook(id ?? 1),
                PageTitle = " Book details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Book book = _bookRepository.GetBook(id);
            BookEditViewModel bookEditViewModel = new BookEditViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                ExistingPhotoPath = book.PhotoPath,
                ExistingPdfUrl = book.BookPdfUrlPath 

            };


            return View(bookEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(BookEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = _bookRepository.GetBook(model.Id);
                book.Title = model.Title;
                book.Author = model.Author;
                book.Genre = model.Genre;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    book.PhotoPath = ProcessUploadedFile(model);

                }
                if (model.BookPdf != null)
                {
                    if (model.ExistingPdfUrl != null)
                    {
                        string pdffilePath = Path.Combine(hostingEnvironment.WebRootPath, "Pdfs", model.ExistingPdfUrl);
                        System.IO.File.Delete(pdffilePath);
                    }
                    book.BookPdfUrlPath = ProcessUploadedPdf(model);

                }


                _bookRepository.Update(book);
                return RedirectToAction("ViewAllBooks");
            }
            return View();


        }

        private string ProcessUploadedFile(BookCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);

                }


            }

            return uniqueFileName;
        }

         
        private string ProcessUploadedPdf(BookCreateViewModel model)
        {
            string pdfuniqueFileName = null;
            if (model.BookPdf != null)
            {
                string uploadspdfFolder = Path.Combine(hostingEnvironment.WebRootPath, "Pdfs");
                pdfuniqueFileName = Guid.NewGuid().ToString() + "_" + model.BookPdf.FileName;
                string pdffilePath = Path.Combine(uploadspdfFolder, pdfuniqueFileName);
                using (var pdffileStream = new FileStream(pdffilePath, FileMode.Create))
                {
                    model.BookPdf.CopyTo(pdffileStream);

                }


            }

            return pdfuniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(BookCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                string pdfuniqueFileName = ProcessUploadedPdf(model);

                Book newBook = new Book
                { 
                    Title = model.Title,
                    Author = model.Author,
                    Genre = model.Genre,
                    PhotoPath = uniqueFileName,
                    BookPdfUrlPath = pdfuniqueFileName

                };
                _bookRepository.Add(newBook);
                return RedirectToAction("Details", new { id = newBook.Id });
            }
            return View();


        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            

            _bookRepository.Delete(id);
            return RedirectToAction("ViewAllBooks");
        }





        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
