using ESDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            UserProfile userProfile = null;
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.Name;
                using (EduPortalContext context = new EduPortalContext())
                {
                    userProfile = context.Profiles.Where(p => p.Email == email).FirstOrDefault();
                }
            }
            ViewBag.Profile = userProfile;
            return View();
        }

        [ChildActionOnly]
        public ActionResult CourseList()
        {
            EduPortalContext context = new EduPortalContext();
            return PartialView(context.Cources);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Course(int id)
        {
            UserProfile userProfile = null;
            string email = User.Identity.Name;
            using (EduPortalContext context = new EduPortalContext())
            {
                var course = context.Cources.FirstOrDefault(x => x.ID == id);

                userProfile = context.Profiles.Where(p => p.Email == email).FirstOrDefault();
                ViewBag.Profile = userProfile;

                return View(course);
            }
        }

        public ActionResult Author(int? authorId)
        {
            using (EduPortalContext context = new EduPortalContext())
            {
                var userProfile = context.Teachers.FirstOrDefault(t => t.UserId == authorId);
                return PartialView(userProfile);
            }
        }
    }
}