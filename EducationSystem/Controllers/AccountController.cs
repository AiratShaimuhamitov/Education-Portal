using EducationSystem.Models;
using ESDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EducationSystem.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Profile");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                UserProfile profile = null;
                using (EduPortalContext db = new EduPortalContext())
                {
                    profile = db.Profiles.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                }
                if (profile != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile profile = null;
                using (EduPortalContext db = new EduPortalContext())
                {
                    profile = db.Profiles.FirstOrDefault(u => u.Email == model.Email);
                }
                if (profile == null)
                {
                    // создаем нового пользователя
                    using (EduPortalContext db = new EduPortalContext())
                    {
                        if (model.IsTeacher)
                            db.Profiles.Add(new Teacher(model.Email, model.Password, model.FirstName, model.SecondName, model.DateOfBirth, model.City));
                        else
                            db.Profiles.Add(new Student(model.Email, model.Password, model.FirstName, model.SecondName, model.DateOfBirth, model.City));
                        db.SaveChanges();

                        profile = db.Profiles.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (profile != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("Index", "Profile");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public ActionResult ImageUpload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                var photo = new byte[upload.ContentLength];
                upload.InputStream.Read(photo, 0, upload.ContentLength);

                using (EduPortalContext context = new EduPortalContext())
                {
                    var email = User.Identity.Name;
                    var profile = context.Profiles.Where(p => p.Email == email).FirstOrDefault();

                    profile.Photo = photo;
                    context.SaveChanges();
                }

            }
            return Index();
        }

        [ChildActionOnly]
        public ActionResult GetDefaultNavBar()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.Name;
                using (EduPortalContext context = new EduPortalContext())
                {
                    var userProfile = context.Profiles.Where(p => p.Email == email).FirstOrDefault();
                    return PartialView(userProfile);
                }
            }
            return View();
        }

        [ChildActionOnly]
        public ActionResult GetBlackNavBar()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.Name;
                using (EduPortalContext context = new EduPortalContext())
                {
                    var userProfile = context.Profiles.Where(p => p.Email == email).FirstOrDefault();
                    return PartialView(userProfile);
                }
            }
            return View();
        }

    }
}