﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using Online_BookShop.DataAccess.Repository.IRepository;
using Online_BookShop.Models.Models;
using Online_BookShop.Models.ViewModels;
using Online_BookShop.Utility;
using System.Collections.Generic;
using System.Drawing;

namespace Online_BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]//Outlines that only admins are allowed to access the product controller
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        //Dependency injection.
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //Converting a category to an IEnumerable of SelectListItem Using Projection
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
           
            return View(objProductList);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category
               .GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.CategoryId.ToString()
               }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.ProductId == id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); /*Image name*/
                    string productPath = Path.Combine(wwwRootPath, @"images\product"); /*Location where we have to save*/

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old  image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //Saving the image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        productVM.Product.ImageUrl = @"\images\product\" + fileName;
                    }
                    if(productVM.Product.ProductId == 0)
                    {
                        _unitOfWork.Product.Add(productVM.Product);
                    }
                    else
                    {
                        _unitOfWork.Product.Update(productVM.Product);
                    }
                    _unitOfWork.Save();
                    TempData["success"] = "Product created successfully"; /*Displays message of a successful action*/
                    return RedirectToAction("Index");
                }
                else
                {

                    productVM.CategoryList = _unitOfWork.Category
                       .GetAll().Select(u => new SelectListItem
                       {
                           Text = u.Name,
                           Value = u.CategoryId.ToString()
                       });
                    return View(productVM);
                }
           

        }
       
        

        #region API CALLS
         //api to retreive data from the database
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objProductList });
        }
        //api to delete data
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (productToBeDeleted == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, 
                productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
