using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models;

namespace QuanLyKho.Areas.Admin.Controllers
{
    public class NhapKhoesController : Controller
    {
         LTQLDBContext db = new LTQLDBContext();
        AutoGenerateKey aukey = new AutoGenerateKey();
        // GET: Admin/NhapKhoes
        public ActionResult Index()
        {
            var nhapKhoes = db.NhapKhoes.Include(n => n.HangHoa).Include(n => n.NCC);
            return View(nhapKhoes.ToList());
        }

        // GET: Admin/NhapKhoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            return View(nhapKho);
        }

        // GET: Admin/NhapKhoes/Create
        public ActionResult Create()
        {

            if (db.NhapKhoes.OrderByDescending(m => m.MaPhieuNhap).Count() == 0)
            {
                var newID = "MPN001";
                ViewBag.NewMPNID = newID;
            }
            else
            {
                var MPNID = db.NhapKhoes.OrderByDescending(m => m.MaPhieuNhap).FirstOrDefault().MaPhieuNhap;
                var newID = aukey.GenerateKey(MPNID);
                ViewBag.NewMPNID = newID;
            }
            ViewBag.MaHang = new SelectList(db.HangHoas, "MaHang", "TenHang");
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC");
           
            return View();
        }

        // POST: Admin/NhapKhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieuNhap,NgayNhap,MaNCC,MaHang,SoLuong,DonGia,ThanhTien")] NhapKho nhapKho)
        {
            if (ModelState.IsValid)
            {
                db.NhapKhoes.Add(nhapKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHang = new SelectList(db.HangHoas, "MaHang", "TenHang", nhapKho.MaHang);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", nhapKho.MaNCC);
            return View(nhapKho);
        }

        // GET: Admin/NhapKhoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHang = new SelectList(db.HangHoas, "MaHang", "TenHang", nhapKho.MaHang);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", nhapKho.MaNCC);
            return View(nhapKho);
        }

        // POST: Admin/NhapKhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieuNhap,NgayNhap,MaNCC,MaHang,SoLuong,DonGia,ThanhTien")] NhapKho nhapKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhapKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHang = new SelectList(db.HangHoas, "MaHang", "TenHang", nhapKho.MaHang);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", nhapKho.MaNCC);
            return View(nhapKho);
        }

        // GET: Admin/NhapKhoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            return View(nhapKho);
        }

        // POST: Admin/NhapKhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            db.NhapKhoes.Remove(nhapKho);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
