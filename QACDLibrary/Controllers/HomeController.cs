using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace QACDLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult GetEmployees()
        {
            using (QACDLibrary.Models.QACDLibraryEntities dc = new QACDLibrary.Models.QACDLibraryEntities())
            {
                var artistName = dc.CDs.OrderBy(a => a.ArtistName).ToList();
                return Json(new { data = artistName }, JsonRequestBehavior.AllowGet);
            }
        }

        

        [HttpGet]
        public ActionResult Insert()
        {
            using (QACDLibrary.Models.QACDLibraryEntities dc = new QACDLibrary.Models.QACDLibraryEntities())
            {
               
                return View();
            }
        }


        [HttpPost]
        public ActionResult Insert(QACDLibrary.Models.CD cd)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (QACDLibrary.Models.QACDLibraryEntities dc = new QACDLibrary.Models.QACDLibraryEntities())
                {
                    dc.CDs.Add(cd);
                    dc.SaveChanges();
                    status = true;

                }
            }
            return new JsonResult { Data = new { status = status } };
        }


        [HttpGet]
        public ActionResult Save(int id)
        {
            using (QACDLibrary.Models.QACDLibraryEntities dc = new QACDLibrary.Models.QACDLibraryEntities())
            {
                var v = dc.CDs.Where(a => a.CDId == id).FirstOrDefault();
                return View(v);
            }
        }


        [HttpPost]
        public ActionResult Save(QACDLibrary.Models.CD cd)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (QACDLibrary.Models.QACDLibraryEntities dc = new QACDLibrary.Models.QACDLibraryEntities())
                {
                  
                        //Edit 
                        var v = dc.CDs.Where(a => a.CDId == cd.CDId).FirstOrDefault();
                        if (v != null)
                        {
                            v.ArtistName = cd.ArtistName;
                            v.Genre = cd.Genre;
                            v.AlbumTitle = cd.AlbumTitle;
                        }

                        
                    
                    dc.SaveChanges();
                    status = true;

                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (QACDLibrary.Models.QACDLibraryEntities dc = new QACDLibrary.Models.QACDLibraryEntities())
            {
                var v = dc.CDs.Where(a => a.CDId == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (QACDLibrary.Models.QACDLibraryEntities dc = new QACDLibrary.Models.QACDLibraryEntities())
            {
                var v = dc.CDs.Where(a => a.CDId == id).FirstOrDefault();
                if (v != null)
                {
                    dc.CDs.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
