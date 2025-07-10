using CRUD_2312B1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_2312B1.Controllers
{
    public class UserController : Controller
    {
        private readonly LabsevenContext a;

        public UserController(LabsevenContext _dbase)
        {
            a = _dbase;
        }

        public IActionResult Index()
        {
            //var fetch = dbase.Products.ToList();
            return View(a.Products.ToList());
        }
    }
}
