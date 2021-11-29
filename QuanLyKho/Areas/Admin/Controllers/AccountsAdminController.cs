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

namespace QuanLyKho.Areas.Admin.Controllers
{
    public class AccountsAdminController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();
        Encrytion Encry = new Encrytion();

        // GET: Admin/AccountsAdmin
        public ActionResult Index()
        {
            return View();
        }
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
                acc.Password = Encry.PasswordEncrytion(acc.Password);
                acc.ConfirmPassword = Encry.PasswordEncrytion(acc.ConfirmPassword);
                db.Accounts.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Login", "Accounts");
            }
            return View(acc);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (CheckSession() == 1)
            {
                return RedirectToAction("Index", "NhapKhoesAdmin", new { Area = "Admin" });
            }
            else if (CheckSession() == 2)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Account acc, string returnUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(acc.Username) && !string.IsNullOrEmpty(acc.Password))
                {
                    using (var db = new LTQLDBContext())
                    {
                        var passToMD5 = Encry.PasswordEncrytion(acc.Password);
                        var account = db.Accounts.Where(m => m.Username.Equals(acc.Username) && m.Password.Equals(passToMD5)).Count();
                        if (account == 1)
                        {
                            FormsAuthentication.SetAuthCookie(acc.Username, false);
                            Session["idUser"] = acc.Username;
                            Session["roleUser"] = acc.RoleID;
                            return RedirectToLocal(returnUrl);
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
        private ActionResult RedirectToLocal(string returnUrl)
        {

            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                if (CheckSession() == 1)
                {
                    return RedirectToAction("Index", "NhapKhoesAdmin", new { Area = "Admin" });
                }
                else if (CheckSession() == 2)

                {
                    return RedirectToAction("Index", "Home", new { Area = "" });
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

        //Kiểm tra người dùng đăng nhập quyền gì
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
                        if (role.ToString() == "Admin")

                        {
                            return 1;
                        }
                        else if (role.ToString() == "client")
                        {
                            return 2;
                        }
                    }
                }
            }
            return 0;
        }
    }
    }
