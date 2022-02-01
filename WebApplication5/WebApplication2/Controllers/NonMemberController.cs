using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class NonMemberController : Controller
    {
        SaveAnimalsEntities db = new SaveAnimalsEntities();

        public ActionResult NonMemberBlogList()
        {
            var data = from p in (new SaveAnimalsEntities()).tBlog
                       select p;
            return View(data);
        }


        //public ActionResult NonMemberRescueList()
        //{
        //    var data = from p in (new SaveAnimalsEntities()).tRescue
        //               select p;
        //    return View(data);
        //}
        //public ActionResult NonMemberRescueList()
        //{
        //    IEnumerable<tRescue> datas = null;
        //    string keyword = Request.Form["txtKeyword"];//關鍵字搜尋
        //    if (string.IsNullOrEmpty(keyword))
        //    {
        //        datas = from p in (new SaveAnimalsEntities()).tRescue
        //                select p;
        //    }
        //    else
        //    {
        //        datas = from p in (new SaveAnimalsEntities()).tRescue
        //                where p.RescueTitle.Contains(keyword)
        //                select p;
        //    }
        //    return View(datas);

        //}
        public ActionResult NonMemberRescueList(string ResCueDonesearch)
        {
            var ResCueDonesearchLst = new List<string>();
            var ResCueDonesearchQry = from d in db.tRescue orderby d.ResCueDone select d.ResCueDone;
            ResCueDonesearchLst.AddRange(ResCueDonesearchQry.Distinct());
            ViewBag.ResCueDonesearch = new SelectList(ResCueDonesearchLst);
            var data = from m in db.tRescue select m;
            if (!string.IsNullOrEmpty(ResCueDonesearch))
            {
                data = data.Where(x => x.ResCueDone == ResCueDonesearch);
            }
            return View(data);
        }

        public ActionResult NonMemberRescueDetail(int id)
        {
            tRescue rescue = db.tRescue.FirstOrDefault(p => p.RescueID == id);

            if (rescue == null)
            {
                return RedirectToAction("MemberRescueList");
            }
//            IEnumerable<SelectListItem> objPosition = (
//from x in db.tPosition where x.positionPosition != null select x).ToList().Select(x => new SelectListItem { Value = x.positionID.ToString(), Text = x.positionPosition });
//            ViewBag.SelectListItemPosition = new SelectList(objPosition, "Value", "Text", "請選擇區域");

//            IEnumerable<SelectListItem> objspecies = (
//from x in db.tSpecies where x.SpeciesName != null select x).ToList().Select(x => new SelectListItem { Value = x.SpeciesID.ToString(), Text = x.SpeciesName });
//            ViewBag.SelectListItemSpecies = new SelectList(objspecies, "Value", "Text", "請選擇區域");
            return View(rescue);
        }


    }
}