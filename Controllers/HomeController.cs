using BlindLowVisionProject.Models;
using BlindLowVisionProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace BlindLowVisionProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(ICustomerRepository customerRepository,
                                   IHostingEnvironment hostingEnvironment)
        {
            _customerRepository = customerRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _customerRepository.GetAllCustomers();
            return View(model);
        }

        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            //throw new Exception("Error in Details View");

            Customer customer = _customerRepository.GetCustomer(id.Value);

            if(customer == null)
            {
                Response.StatusCode = 404;
                return View("CustomerNotFound", id.Value);
            }
                       
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Customer = customer,
                PageTitle = "Customer Details"
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
            Customer customer = _customerRepository.GetCustomer(id);
            CustomerEditViewModel customerEditViewModel = new CustomerEditViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Department = customer.Department,
                ExistingPhotoPath = customer.PhotoPath
            };
            return View(customerEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _customerRepository.GetCustomer(model.Id);
                customer.Name = model.Name;
                customer.Email = model.Email;
                customer.Department = model.Department;
                if(model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                            System.IO.File.Delete(filePath);
                    }
                    customer.PhotoPath = ProcessUploadedFile(model);
                }
               
                _customerRepository.Update(customer);
                return RedirectToAction("index");
            }

            return View();
        }

        private string ProcessUploadedFile(CustomerCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Customer newCustomer = new Customer
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _customerRepository.Add(newCustomer);
                return RedirectToAction("details", new { id = newCustomer.Id });
            }

            return View();
        }
    }
}
