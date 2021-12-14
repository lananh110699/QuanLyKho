using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using QuanLyKho.Models;

namespace QuanLyKho.Controllers
{
    public class AccountsController : Controller
    {
        Encrytion ecy = new Encrytion();
        LTQLDBContext DB = new LTQLDBContext();
        StringProcess pro = new StringProcess();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                //Mã Hóa mật khẩu trước khi cho vào database
                acc.Password = ecy.PasswordEncrytion(acc.Password);
                DB.Accounts.Add(acc);
                DB.SaveChanges();
                return RedirectToAction("Login", "Accounts");
            }
            return View(acc);
        }
        public ActionResult Login(string returnUrl)

        {
            if (CheckSession() == 1)

            {

                return RedirectToAction("Index", "HomeAdmin", new { Area = "Admin" });
            }
            else if (CheckSession() == 2)

            {
                return RedirectToAction("Index", "HomeClient", new { Area = "Client" });

            }
            ViewBag.ReturnUrl = returnUrl;
            return View();

        }

        private int CheckSession()
        {
            using (var db = new LTQLDBContext())
            {
                var user = HttpContext.Session["idUser"];
                if (user != null)
                {
                    var role = db.Accounts.Find(user.ToString()).RoleID;
                    if (role != null)
                    {
                        if (role.ToString() == "ADMIN")
                        {
                            return 1;
                        }
                        else if (role.ToString() == "CLIENT")
                        {
                            return 2;
                        }
                    }
                }
            }
            return 0;
        }

        [HttpPost]
        [AllowAnonymous]

        public ActionResult Login(Account acc, string returnUrl)

        {
            try
            {
                if (!string.IsNullOrEmpty(acc.Username) && !string.IsNullOrEmpty(acc.Password))
                {

                    using (var db = new LTQLDBContext())

                    {
                        var passToMD5 = pro.GetMD5(acc.Password);
                        var account = db.Accounts.Where(m => m.Username.Equals(acc.Username) && m.Password.Equals(passToMD5)).Count();
                        if (account == 1)
                        {
                            FormsAuthentication.SetAuthCookie(acc.Username, false);
                            Session["idUser"] = acc.Username;
                            return RedirectTolocal(returnUrl);
                        }

                        ModelState.AddModelError("", "Thông tin đăng nhập chưa chính xác");

                    }
                }
                ModelState.AddModelError("", "Username and password is required.");
            }

            catch
            {
                ModelState.AddModelError("", "Hệ thống đang được bảo trì, vui lòng liên hệ với quản trị viên");
            }
            return View(acc);
        }

        private ActionResult RedirectTolocal(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                if (CheckSession() == 1)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { Area = "Admin" });
                }
                else if (CheckSession() == 2)
                {
                    return RedirectToAction("Index", "HomeClient", new { Area = "Client" });
                }
            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["iduser"] = null;
            return RedirectToAction("Login", "Accounts");
        }
    }
}
