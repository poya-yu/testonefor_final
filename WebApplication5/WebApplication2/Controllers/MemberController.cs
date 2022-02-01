using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;
using System.IO;
using WebApplication2.ViewModel;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    
    [Authorize]//以下為授權過後的行為
    public class MemberController : Controller
    {
        SaveAnimalsEntities db = new SaveAnimalsEntities();

        // GET: Member
        public ActionResult Index()
        {
            return View("../Home/Index", "_LayoutMember");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");//回登入頁面不合乎實際
        }


        public ActionResult MemberBlogList()
        {
            var data = from p in (new SaveAnimalsEntities()).tBlog
                       select p;
            return View(data);
        }
        public ActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBlog(tBlog p)
        {
            p.Created_At = DateTime.Now;
            db.tBlog.Add(p);
            db.SaveChanges();
            return RedirectToAction("BlogList");
        }

        public ActionResult MemberRescueList()
        {
            IEnumerable<tRescue> datas = null;
            string keyword = Request.Form["txtKeyword"];//關鍵字搜尋
            if (string.IsNullOrEmpty(keyword))
            {
                datas = from p in (new SaveAnimalsEntities()).tRescue
                        select p;
            }
            else
            {
                datas = from p in (new SaveAnimalsEntities()).tRescue
                        where p.RescueTitle.Contains(keyword)
                        select p;
            }
            return View(datas);
        }


        public ActionResult MemberCreateRescue()
        {
            var rescuePositionCity = db.tPosition.Where(m => m.positionBelong == 0).ToList();
            ViewBag.City = new SelectList(rescuePositionCity, "positionID", "positionPosition");

            var rescuePositionCountry = db.tPosition.Where(m => m.positionBelong != 0).ToList();
            ViewBag.Country = new SelectList(rescuePositionCountry, "positionID", "positionPosition");

            var rescueSpecies = db.tSpecies.ToList();
            ViewBag.Species = new SelectList(rescueSpecies, "SpeciesID", "SpeciesName");

            return View();
        }
        [HttpPost]
        public ActionResult MemberCreateRescue(tRescue p, HttpPostedFileBase RePictures)
        {
            var rescuePositionCity = db.tPosition.Where(m => m.positionBelong == 0).ToList();
            ViewBag.City = new SelectList(rescuePositionCity, "positionID", "positionPosition");

            var rescuePositionCountry = db.tPosition.Where(m => m.positionBelong != 0).ToList();
            ViewBag.Country = new SelectList(rescuePositionCountry, "positionID", "positionPosition");

            var rescueSpecies = db.tSpecies.ToList();
            ViewBag.Species = new SelectList(rescueSpecies, "SpeciesID", "SpeciesName");
            string photoname = "";
            if (RePictures != null)
            {
                if (RePictures.ContentLength > 0)
                {
                    //photoname = Path.GetFileName(RePictures.FileName);
                    photoname = Guid.NewGuid().ToString() + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Images"), photoname);
                    RePictures.SaveAs(path);
                }
            }
            p.RescueTitle = Request.Form["drop1"];//抓title選項
            p.ResCueDone = Request.Form["drop2"];//抓done選項
            p.Created_At = DateTime.Now;//自動給日期
            p.RescueMemberID = Convert.ToInt32(Session["LoginSucess"]);//抓登入者ID
            p.RescuePictures = photoname;
            db.tRescue.Add(p);
            db.SaveChanges();
            return RedirectToAction("MemberRescueList");

        }

        public ActionResult MemberDeleteRescue(int id)
        {
            tRescue rescue = db.tRescue.FirstOrDefault(p => p.RescueID == id);

            if (rescue != null )
            {
                db.tRescue.Remove(rescue);
                db.SaveChanges();
            }
          
            return RedirectToAction("MemberRescueList");
        }

        public ActionResult MemberEditRescue(int id)
        {
            var rescuePositionCity = db.tPosition.Where(m => m.positionBelong == 0).ToList();
            ViewBag.City = new SelectList(rescuePositionCity, "positionID", "positionPosition");

            var rescuePositionCountry = db.tPosition.Where(m => m.positionBelong != 0).ToList();
            ViewBag.Country = new SelectList(rescuePositionCountry, "positionID", "positionPosition");

            var rescueSpecies = db.tSpecies.ToList();
            ViewBag.Species = new SelectList(rescueSpecies, "SpeciesID", "SpeciesName");
            tRescue rescue = db.tRescue.FirstOrDefault(p => p.RescueID == id);

            if (rescue == null)
            {
                return RedirectToAction("MemberRescueList");
            }
            return View(new CRescueViewModel() { Rescue = rescue });
        }



        [HttpPost]
        public ActionResult MemberEditRescue(CRescueViewModel editRescue)
        {
            var rescuePositionCity = db.tPosition.Where(m => m.positionBelong == 0).ToList();
            ViewBag.City = new SelectList(rescuePositionCity, "positionID", "positionPosition");

            var rescuePositionCountry = db.tPosition.Where(m => m.positionBelong != 0).ToList();
            ViewBag.Country = new SelectList(rescuePositionCountry, "positionID", "positionPosition");

            var rescueSpecies = db.tSpecies.ToList();
            ViewBag.Species = new SelectList(rescueSpecies, "SpeciesID", "SpeciesName");
            tRescue rescue = db.tRescue.FirstOrDefault(p => p.RescueID == editRescue.RescueID);

            if (rescue != null)
            {
                if (editRescue.RescuePictures != null)//代表有資料上傳
                {
                    string photoname = Guid.NewGuid().ToString() + ".jpg";
                    rescue.RescuePictures = photoname;
                    editRescue.RescuePictures.SaveAs(Server.MapPath("../../Images/" + photoname));
                }
                rescue.RescueTitle = Request.Form["drop1"];//抓title選項
                rescue.ResCueDone = Request.Form["drop2"];//抓done選項
                rescue.RescuePosition=editRescue.RescuePosition;
                rescue.RescuePosition1 = editRescue.RescuePosition1;
                rescue.RescueSpecies = editRescue.RescueSpecies;
                rescue.RescueEvent = editRescue.RescueEvent;
                rescue.RescueProgress=editRescue.RescueProgress;
                rescue.Created_At=DateTime.Now;
                db.SaveChanges();

            }
            return RedirectToAction("MemberRescueList"); 
        }
        public ActionResult AddtoFollowrescue(int id)
        {
            tRescue rescue = db.tRescue.FirstOrDefault(p => p.RescueID == id);

            if (rescue == null)
            {
                return RedirectToAction("MemberRescueList");
            }
            return View(rescue);
        }
        [HttpPost]

        public ActionResult AddtoFollowrescue(CAddtoRescueViewModel vModel)
        {

            tRescue rescue = db.tRescue.FirstOrDefault(p => p.RescueID == vModel.RescueID);

            if (rescue != null)
            {
                FollowRescue followR = new FollowRescue();
                followR.FollowMemberID= Convert.ToInt32(Session["LoginSucess"]);
                followR.FollowResue = rescue.RescueID;
                db.FollowRescue.Add(followR);
                db.SaveChanges();
            }
            return RedirectToAction("MemberRescueList");
        }
        [HttpPost]
        public ActionResult section(string id)
        {
            var City = Convert.ToInt32(id);
            List<SelectListItem> list = (from p in new SaveAnimalsEntities().tPosition
                                         where p.positionBelong == City
                                         select new SelectListItem
                                         {
                                             Value = p.positionID.ToString(),
                                             Text = p.positionPosition
                                         }).ToList();
            string result = string.Empty;
            if (list == null)
            {
                return Json(result);

            }
            else
            {
                result = JsonConvert.SerializeObject(list);
                return Json(result);
            }
        }





    }
}