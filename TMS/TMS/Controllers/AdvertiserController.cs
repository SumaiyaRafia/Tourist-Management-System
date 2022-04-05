using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Auth;
using TMS.Models.Database;
using TMS.Models.Database.Entity;

namespace TMS.Controllers
{
    public class AdvertiserController : Controller
    {

        [HttpGet]
        public ActionResult Register()
        {
            return View(new AdvertiserinfoModel());
        }
        [HttpPost]
        public ActionResult Register(AdvertiserinfoModel AD)
        {
            if (ModelState.IsValid)
            {
                TouristDBEntities1 db = new TouristDBEntities1();
                var a = new AdvertiserinfoModel();

                var n = new AdvertiserInfo();

                n.advertiserid = AD.advertiserid;
                n.emailid = AD.emailid;
                n.username = AD.username;
                n.password = AD.password;
                Session["id"] = n.advertiserid.ToString();

                db.AdvertiserInfoes.Add(n);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(AD);
        }
        [HttpGet]
        public ActionResult RegisterAD()
        {
            return View(new ADinfoModel());
        }
        [HttpPost]
        public ActionResult RegisterAD(ADinfoModel AD)
        {
            if (ModelState.IsValid)
            {
                TouristDBEntities1 db = new TouristDBEntities1();
                var a = new ADinfoModel();

                var n = new ADinfo();

                n.adid = AD.adid;
                n.advertisename = AD.advertisename;
                n.advertisetype = AD.advertisetype;
                n.advertisebudget = AD.advertisebudget;
                n.contactno = AD.contactno;
                Session["id"] = n.adid;

                db.ADinfoes.Add(n);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(AD);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdvertiserinfoModel ulogin)
        {

            if (ModelState.IsValid)
            {
                TouristDBEntities1 db = new TouristDBEntities1();
                var u = (from e in db.AdvertiserInfoes
                         where e.username.Equals(ulogin.username) &&
                         e.password.Equals(ulogin.password)
                         select e).FirstOrDefault();

                if (u != null)
                {
                    Session["advertiserid"] = u.advertiserid;
                    Session["username"] = u.username;
                    Session["password"] = u.password;

                    return RedirectToAction("Dashboard");
                }
                ViewBag.ErrorMessage = "Invalid User";
                TempData["msg"] = "Invalid Credentials";
                return RedirectToAction("Login");

            }
            return View();
        }




        public ActionResult Home()
        {
            return View();
        }

        //[AdvertiserAccess]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            int id = Convert.ToInt32(Session["advertiserid"]);
            TouristDBEntities1 db = new TouristDBEntities1();
            AdvertiserInfo adetails = db.AdvertiserInfoes.Find(id);
            if(adetails == null)
            {
                return HttpNotFound();
            }
            return View(adetails);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            int id = Convert.ToInt32(Session["advertiserid"]);
            TouristDBEntities1 db = new TouristDBEntities1();
            var adv = (from ad in db.AdvertiserInfoes
                       where ad.advertiserid == id
                       select ad).FirstOrDefault();
            return View(adv);
        }
        [HttpPost]
        public ActionResult Edit(AdvertiserInfo adv)
        {
            TouristDBEntities1 db = new TouristDBEntities1();
            var adv1 = (from ad in db.AdvertiserInfoes
                       where ad.advertiserid == adv.advertiserid
                       select ad).FirstOrDefault();
            db.Entry(adv1).CurrentValues.SetValues(adv);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        public ActionResult Delete()
        {
            int id = Convert.ToInt32(Session["advertiserid"]);

            try
            {
                using (TouristDBEntities1 model = new TouristDBEntities1())
                {
                    AdvertiserInfo adv = model.AdvertiserInfoes.Where((x)=> x.advertiserid == id).FirstOrDefault();
                    model.AdvertiserInfoes.Remove(adv);
                    model.SaveChanges();
                }
                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}