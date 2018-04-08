using ESDataBase;
using ESDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    public class TheoryController : Controller
    {
        public ActionResult Index(int courseId, int themeId)
        {
            using (EduPortalContext context = new EduPortalContext())
            {
                var course = context.Cources.FirstOrDefault(x => x.ID == courseId);
                var themes = course.Themes;
                if (themes != null)
                    return View(themes.FirstOrDefault(x => x.ID == themeId));
            }
            Response.StatusCode = 404;
            return View();
        }

        [ChildActionOnly]
        public ActionResult Themes(int themeId)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userEmail = User.Identity.Name;
                Course course = null;
                using (EduPortalContext context = new EduPortalContext())
                {
                    course = context.Cources.FirstOrDefault();
                    ViewData.Add("activeId", themeId);
                    ViewData.Add("firstThemeName", context.Cources
                                                            .FirstOrDefault(x => x.ID == 1)
                                                            .Themes.FirstOrDefault().Name);
                    var profile = context.Profiles.FirstOrDefault(x => x.Email == userEmail);
                    ViewBag.Profile = profile;
                }

                return PartialView(course);
            }
            return RedirectToAction("Index", "Account", null);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddTheory(int id)
        {
            return View();
        }
    }
}