using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace College.Courses.Controllers
{
    public class HomeController : Controller
    {
        Courses objCourse = null;

        public HomeController()
        {
            objCourse = new Courses();

        }
        public ActionResult Index()
        {
            objCourse.CourseList = new List<string>();
            objCourse.DicCourseList = new Dictionary<string, string>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string course)
        {
            var splitCourse = course.Split(':');
            string key = splitCourse[0].ToLower().Trim();
            string value = splitCourse[1].ToLower().Trim();
            JsonResult jResult = new JsonResult();
            jResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            objCourse.DicCourseList.Add(key, value);
            if (!string.IsNullOrEmpty(splitCourse[1]))
            {
                if (objCourse.DicCourseList.Count > 1)
                {
                    if (Circular(key, value))
                    {
                        objCourse.DicCourseList.Remove(key);
                        jResult.Data = "Can not add the course becuase of circular reference";
                    }
                    else
                    {
                        jResult.Data = "success";

                    }
                }
                else
                {
                    jResult.Data = "success";

                }

            }
            else
            {
                jResult.Data = "success";

            }



            return jResult;
        }
        public ActionResult About()
        {

            foreach (KeyValuePair<string, string> course in objCourse.DicCourseList)
            {
                string key = course.Key;
                string value = course.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    if (objCourse.CourseList.IndexOf(value) < 0)
                    {
                        objCourse.CourseList.Add(value);
                        concate(key, value, objCourse.DicCourseList);
                        //objCourse.CourseList.Add(key);
                    }
                }

            }
            //check if any key does not exist
            foreach (KeyValuePair<string, string> course in objCourse.DicCourseList)
            {
                string key = course.Key;             
                if (!string.IsNullOrEmpty(key))
                {
                    if (objCourse.CourseList.IndexOf(key) < 0)
                    {
                        objCourse.CourseList.Add(key);                       
                        
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in objCourse.CourseList)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(item);
            }
            ViewBag.Result = sb.ToString();

            return View();
        }
        private void concate(string key, string value, Dictionary<string, string> CourseList)
        {
            foreach (KeyValuePair<string, string> course in CourseList)
            {
                if (value == course.Value)
                {

                    objCourse.CourseList.Add(course.Key);

                }
            }
        }
        private bool Circular(string key, string value)
        {
            string storeKey = key;
            string storeValue = string.Empty;
            for (int i = objCourse.DicCourseList.Count; i > 0; i--)
            {
                if (storeValue == string.Empty)
                {
                    if (objCourse.DicCourseList.ContainsKey(value))
                    {
                        storeValue = objCourse.DicCourseList[value];
                    }
                }
                else
                {
                    if (objCourse.DicCourseList.ContainsKey(storeValue))
                    {
                        storeValue = objCourse.DicCourseList[storeValue];
                    }
                }
                if (storeKey == storeValue)
                {
                    return true;
                }

            }
            return false;
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}