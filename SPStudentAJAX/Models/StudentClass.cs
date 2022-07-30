using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPStudentAJAX.Models;

namespace SPStudentAJAX.Models
{
    public class StudentClass
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Roll { get; set; }
        public string Class_ { get; set; }
        public string ClassName { get; set; }
        public string ClassStartDate { get; set; }
    }
}