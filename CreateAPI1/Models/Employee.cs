using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public string Message { get; set; }

    }
}