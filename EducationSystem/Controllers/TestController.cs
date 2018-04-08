using EducationSystem.Services;
using ESDataBase;
using ESDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index(int courseId)
        {
            Course course;
            using (EduPortalContext context = new EduPortalContext())
            {
                course = context.Cources.FirstOrDefault(x => x.ID == courseId);
                ViewBag.CourseName = course.Name;
                var _test = course.Tests.FirstOrDefault();
                ViewBag.QuestionsCount = _test.Questions.Count();
                return View(course.Tests.FirstOrDefault());
            }
        }

        public ActionResult Start(int testID)
        {
            TestService testService;
            if (TempData["test_service"] == null)
            {
                using (EduPortalContext context = new EduPortalContext())
                {
                    Test _test = context.Tests.FirstOrDefault(x => x.ID == testID);
                    testService = new TestService(_test);
                }
            }
            else
            {
                testService = (TestService)TempData["test_service"];
            }

            var question = testService.GetNextQuestion();
            return View(question);
        }
    }
}