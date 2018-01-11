using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College.Courses
{
    public class Courses
    {
        public List<string> CourseList
        {
            get
            {
                return HttpContext.Current.Session["courses"] as List<string>;
            }
            set
            {
                HttpContext.Current.Session["courses"] = value;
            }
        }

        public Dictionary<string, string> DicCourseList
        {
            get
            {
                return HttpContext.Current.Session["DicCourse"] as Dictionary<string, string>;
            }
            set
            {
                HttpContext.Current.Session["DicCourse"] = value;
            }
        }
    }
}