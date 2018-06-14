using CarWash.DataLayer.DataManager;
using CarWash.DataLayer.SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWash.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBookings()
        {
            ServiceDatamanager ser = new ServiceDatamanager();
            return this.Json(ser.GetMasterDetail(),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProjects()
        {
            ServiceDatamanager ser = new ServiceDatamanager();
            return this.Json(ser.GetProjects(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProject(DataLayer.DBContext.Project entity)
        {
            ServiceDatamanager ser = new ServiceDatamanager();
            return this.Json(ser.SaveProject(entity), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteProject(DataLayer.DBContext.Project entity)
        {
            ServiceDatamanager ser = new ServiceDatamanager();
            return this.Json(ser.DeleteProject(entity), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateBooking(CarwashMaster entity)
        {
            ServiceDatamanager ser = new ServiceDatamanager();
            ser.UpdateServiceOrder(entity.id);
            return this.Json(ser.GetMasterDetail(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            var file1 = Request.Files;
            if(file1.Count>0)
            {
                var name = file1[0].FileName;

            }
            return this.Json(true, JsonRequestBehavior.AllowGet);

        }
    }
}