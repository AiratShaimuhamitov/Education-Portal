using ESDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
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
            return RedirectToAction("Index", "Account");
        }

        public FileContentResult GetProfileImage()
        {
            string email = User.Identity.Name;
            using (EduPortalContext context = new EduPortalContext())
            {
                var userProfile = context.Profiles.Where(p => p.Email == email).FirstOrDefault();
                if (userProfile.Photo != null)
                {
                    return File(userProfile.Photo, "jpg");
                }
            }
            return null;
        }

        public UserProfile GetCurrentUser()
        {
            string email = User.Identity.Name;
            using (EduPortalContext context = new EduPortalContext())
            {
                return context.Profiles.Where(p => p.Email == email).FirstOrDefault();               
            }
        }
    }
}