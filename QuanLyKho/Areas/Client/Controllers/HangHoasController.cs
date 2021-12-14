using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyKho.Models;

namespace QuanLyKho.Areas.Client.Controllers
{
    public class HangHoasController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();
        ExcelProcess ExcelPro = new ExcelProcess();
        AutoGenerateKey aukey = new AutoGenerateKey();

        // GET: Client/HangHoas
        public ActionResult Index()
        {
            return View(db.HangHoas.ToList());
        }

        // GET: Client/HangHoas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoas.Find(id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // GET: Client/HangHoas/Create
        public ActionResult Create()
        {
            if (db.HangHoas.OrderByDescending(m => m.MaHang).Count() == 0)
            {
                var newID = "MHH001";
                ViewBag.NewMHHID = newID;
            }
            else
            {
                var MHHID = db.HangHoas.OrderByDescending(m => m.MaHang).FirstOrDefault().MaHang;
                var newID = aukey.GenerateKey(MHHID);
                ViewBag.NewMHHID = newID;
            }
            return View();

        }

        // POST: Client/HangHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,TenHang,Size,SoLuong,DonGia,ThanhTien")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Add(hangHoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangHoa);
        }

        // GET: Client/HangHoas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoas.Find(id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // POST: Client/HangHoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,TenHang,Size,SoLuong,DonGia,ThanhTien")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangHoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangHoa);
        }

        // GET: Client/HangHoas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoas.Find(id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // POST: Client/HangHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HangHoa hangHoa = db.HangHoas.Find(id);
            db.HangHoas.Remove(hangHoa);
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
        private DataTable CopyDataFromExcelFile(HttpPostedFileBase file)
        {
            string fileExtention = file.FileName.Substring(file.FileName.IndexOf("."));
            string _FileName = "Ten_File_Muon_Luu" + fileExtention;
            string _path = Path.Combine(Server.MapPath("~/Upload/Excels"), _FileName);
            file.SaveAs(_path);
            DataTable dt = ExcelPro.ReadDataFromExcelFile(_path, false);
            return dt;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LTQLDBContext"].ConnectionString);
        private void OverwriteFastData(int? HangHoa)
        {
            //dt là databasecos chứa dữ liệu để import vào database
            DataTable dt = new DataTable();

            //mapping các column trong database vào các column trong table ở CSDL
            SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
            bulkcopy.DestinationTableName = "HangHoa";
            bulkcopy.ColumnMappings.Add("MaHang", "MaHang");
            bulkcopy.ColumnMappings.Add("TenHang", "TenHang");
            bulkcopy.ColumnMappings.Add("Size", "Size");
            bulkcopy.ColumnMappings.Add("SoLuong", "SoLuong");
            bulkcopy.ColumnMappings.Add("DonGia", "DonGia");
            bulkcopy.ColumnMappings.Add("ThanhTien", "ThanhTien");
            con.Open();
            bulkcopy.WriteToServer(dt);
            con.Close();
        }
    }

}
