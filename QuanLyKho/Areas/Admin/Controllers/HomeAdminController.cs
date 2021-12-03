using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKho.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        //khóa
        [Authorize(Roles = "Admin")]
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}