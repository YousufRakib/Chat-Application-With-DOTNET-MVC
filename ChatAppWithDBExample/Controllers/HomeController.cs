using ChatAppWithDBExample.ChatApp.BLL.UserManager;
using ChatAppWithDBExample.ChatApp.DAL.UserRepository;
using ChatAppWithDBExample.ChatApp.Model;
using ChatAppWithDBExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChatAppWithDBExample.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            
            return View(new User());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(User userData)
        {

            if (new UserRepository().ExistingUser(userData))
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.LoginError = "This Username or Email exist in the system";
            }

            return View();
            
        }


        [HttpPost]
        public ActionResult DeleteMessage(int messageId)
        {
            
            return Json(new UserManager().DeleteMessage(messageId));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View(new LoginData());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginData loginData)
        {
            if (ModelState.IsValid)
            {
                int userId;
                if (new UserRepository().Login(loginData, out userId))
                {
                    FormsAuthentication.RedirectFromLoginPage(userId.ToString(), loginData.RememberMe);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.LoginError = "Username or password was incorrect";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            int userId = int.Parse(User.Identity.Name);
            new UserRepository().RemoveAllUserConnections(userId);
            ChatHub.OfflineUser(userId);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        [HttpPost]
        public ActionResult GetChatbox(int toUserId)
        {
            ChatBoxModel chatBoxModel = new UserManager().GetChatbox(toUserId);
            return PartialView("~/Views/Partials/_ChatBox.cshtml", chatBoxModel);
        }


        [HttpPost]
        public ActionResult SendMessage(int toUserId, string message)
        {
            return Json( new UserManager().SendMessage(toUserId, message));
        }

        [HttpPost]
        public ActionResult LazyLoadMssages(int toUserId, int skip)
        {
            return Json(new UserManager().LazyLoadMssages(toUserId, skip));
        }
    }
}