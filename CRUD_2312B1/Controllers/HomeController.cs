using CRUD_2312B1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CRUD_2312B1.Controllers
{
    public class HomeController : Controller
    {

        private readonly LabsevenContext dbase;

        public HomeController(LabsevenContext _dbase)
        {
            dbase = _dbase;
        }

        //Home Page
        public IActionResult Index()
        {
            return View();
        }

        //  --------  Category Work ----------------------

        // add category
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category c)
        {
            dbase.Categories.Add(c);
            dbase.SaveChanges();
            return RedirectToAction("ViewCategory");
        }


        //View Category
        public IActionResult ViewCategory()
        {
            return View(dbase.Categories.ToList());
        }


        //  --------  Product Work ----------------------

        //View Product
        public IActionResult ViewProduct()
        {
            var ItemsData = dbase.Products.Include(a => a.CIdNavigation);
            return View(ItemsData);

        }


        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Cat = new SelectList(dbase.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product p, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Get image name
                var imageName = Path.GetFileName(file.FileName);

            // Get root path of wwwroot
            string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image");

            // Combine path to save image      
            string imageSavePath = Path.Combine(imagePath, imageName);

            // Save image to wwwroot/Image folder     
            using (var stream = new FileStream(imageSavePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Set image path in the model (for storing in DB)
            p.ProductImage = imageName;

            
                dbase.Products.Add(p);
                dbase.SaveChanges();
                return RedirectToAction("ViewProduct");
            }

            ViewBag.Cat = new SelectList(dbase.Categories, "CategoryId", "CategoryName", p.CId);
            return View();
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var s = dbase.Products.FirstOrDefault(x => x.Id == id);
            return View(s);
        }

        [HttpPost]
        public IActionResult DeleteProduct(Product p)
        {
            dbase.Products.Remove(p);
            dbase.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var s = dbase.Products.FirstOrDefault(x => x.Id == id);
            ViewBag.Categories = new SelectList(dbase.Categories, "CategoryId", "CategoryName");
            return View(s);
        }

        [HttpPost]
        public IActionResult EditProduct(Product p)
        {
            dbase.Products.Update(p);
            dbase.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public IActionResult DetailsProduct(int id)
        {
            var s = dbase.Products.FirstOrDefault(x => x.Id == id);
            return View(s);
        }


    }
}