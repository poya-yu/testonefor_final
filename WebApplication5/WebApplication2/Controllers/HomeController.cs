using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        SaveAnimalsEntities db = new SaveAnimalsEntities();
        public ActionResult Index()
        {
            return View();
        }
       


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string MemberAccount, string MemberPassword)
        {
            var member = db.tMember
                .Where(m => m.MemberAccount == MemberAccount && m.MemberPassword == MemberPassword)
                .FirstOrDefault();
            if (member == null)
            {
                ViewBag.Message = "帳號密碼錯誤";
                return View();
            }
            Session["Join"] = member.MemberName;
            Session["Welcome"] = member.MemberName + " 謝謝你跟我們一起關心動物的生命";
            Session["LoginSucess"] = member.MemberID;
            FormsAuthentication.RedirectFromLoginPage(MemberAccount, true);
            return RedirectToAction("Index", "Member");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(tMember pMember)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var member = db.tMember
                .Where(m => m.MemberAccount == pMember.MemberAccount)
                .FirstOrDefault();
            if (member == null)
            {
                pMember.Created_At = DateTime.Now;
                db.tMember.Add(pMember);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ViewBag.Message = "此帳號已經有人使用，註冊失敗";
            return View();

        }


    }
}