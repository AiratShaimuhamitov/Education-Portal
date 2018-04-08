using ESDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Modules
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.Name;
                using (EduPortalContext context = new EduPortalContext())
                {
                    var userProfile = context.Profiles.Where(p => p.Email == email).FirstOrDefault();
                    return View(userProfile);
                }
            }
            return View();
        }
    }
}