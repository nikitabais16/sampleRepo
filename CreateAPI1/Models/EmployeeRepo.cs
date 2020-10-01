using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreateAPI.Models
{
    public static class EmployeeRepo
    {
        static List<Employee> employees = new List<Employee>()
        {
            new Employee{EmployeeId=1, Name="Jones", Age =20 ,Department = "Training" ,Salary =10000},
            new Employee{EmployeeId =2, Name="Morgan", Age =21 ,Department = "IT" ,Salary = 20000},
            new Employee{EmployeeId =3, Name="Smith", Age = 22,Department = "Infra" ,Salary =300000},
            new Employee{EmployeeId =4, Name="Gill", Age = 26,Department = "Learning" ,Salary =45000.50},
            new Employee{EmployeeId =5, Name="Thompson", Age = 25,Department = "Support" ,Salary =250000},
            new Employee{EmployeeId =6, Name="Andrews", Age = 29,Department = "HR" ,Salary =300000}
        };
        static List<Employee> emptyEmployees = new List<Employee>()
        {

        };

        public static List<Employee> GetEmployees()
        {
            return employees;
        }
        public static List<Employee> GetEmployees_emptyRecord()
        {
            return emptyEmployees;
        }
        public static Employee GetEmployeeById(int id)
        {
            var Emp = from emp in GetEmployees()
                      where emp.EmployeeId.Equals(id)
                      select emp;
            //first or default
            if (Emp!= null && Emp.Count()>0)
            {
                return Emp.First();
            }
            return null;
        }
        public static List<Employee> InsertEmployee(Employee e)
        {
            if(e.Age >=21 && e.Age <= 60)
            {
                employees.Add(e);
                return employees;
            }
            else
            {
                return null;
            }
           
        }
    }
}