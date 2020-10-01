using CreateAPI.Controllers;
using CreateAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Xunit;
using Xunit.Abstractions;


namespace TestEmployeeApi
{
    public class EmployeeApiTests
    {
        List<string> nameList = new List<String>();

        [Fact]
        public void Test_GetAllEmployeeByCount_Return_OK()
        {
            List<Employee> result = EmployeeRepo.GetEmployees();
            EmployeeController employeeController = new EmployeeController();
            employeeController.Request = new HttpRequestMessage();
            employeeController.Request.SetConfiguration(new HttpConfiguration());
            //Act
            HttpResponseMessage responseMsg = employeeController.Get();
            Assert.Equal(HttpStatusCode.OK, responseMsg.StatusCode);
            
            String actualEmployeeContent = responseMsg.Content.ReadAsStringAsync().Result;
            List<Employee> actuallEmployees = JsonConvert.DeserializeObject<List<Employee>>(actualEmployeeContent);

            foreach (Employee obj in actuallEmployees)
            {
                nameList.Add(obj.Name);
            }
            Assert.Equal(6, nameList.Count);
        }

        [Fact]
        public void Test_GetAllEmployee_ListEmpty_Return_OK()
        {
            List<Employee> result = EmployeeRepo.GetEmployees_emptyRecord();
            EmployeeController employeeController = new EmployeeController();
            employeeController.Request = new HttpRequestMessage();
            employeeController.Request.SetConfiguration(new HttpConfiguration());
            //Act
            HttpResponseMessage responseMsg = employeeController.GetForEmptyRecord();
            Assert.Equal(HttpStatusCode.OK, responseMsg.StatusCode);
            

            String actualEmployeeContent = responseMsg.Content.ReadAsStringAsync().Result;
            List<Employee> actuallEmployees = JsonConvert.DeserializeObject<List<Employee>>(actualEmployeeContent);

            //Assert.Equal(0, actuallEmployees.Count);
             Assert.Empty(actuallEmployees);
        }
        [Fact]
        public void Test_GetEmployeeById_Return_OK()
        {
            //arrange
            EmployeeController employeeController = new EmployeeController();
            employeeController.Request = new HttpRequestMessage();
            employeeController.Request.SetConfiguration(new HttpConfiguration());
            //Act
            HttpResponseMessage responseMsg = employeeController.Get(3);
            //Assert
            Assert.Equal(HttpStatusCode.OK, responseMsg.StatusCode);

            String actualEmployeeContent = responseMsg.Content.ReadAsStringAsync().Result;
            Employee actualEmployee = JsonConvert.DeserializeObject<Employee>(actualEmployeeContent);
            Assert.Equal("Smith", actualEmployee.Name);
        }
        [Fact]
        public void Test_GetEmployeeById_DoesNotExist_Retrun_NotFound()
        {
            //arrange
            EmployeeController employeeController = new EmployeeController();
            employeeController.Request = new HttpRequestMessage();
            employeeController.Request.SetConfiguration(new HttpConfiguration());
            //Act
            HttpResponseMessage responseMsg = employeeController.Get(11);
            //Assert
            Assert.Equal(HttpStatusCode.NotFound, responseMsg.StatusCode);

            string str = responseMsg.Content.ReadAsStringAsync().Result;
            Employee actualEmployee = JsonConvert.DeserializeObject<Employee>(str);
            
            string Expected = "Employee doesn't exist";
            Assert.Equal(Expected, actualEmployee.Message);
        }

        [Fact]
        public void Test_PostNewEmployee_ValidAge_Return_Created()
        {
            EmployeeController employeeController = new EmployeeController();
            Employee emp = new Employee() { EmployeeId = 12, Name = "Finch", Age = 28, Department = "Placement", Salary = 15000 };

            employeeController.Request = new HttpRequestMessage();
            employeeController.Request.SetConfiguration(new HttpConfiguration());
            HttpResponseMessage responseMsg = employeeController.Post(emp);

            Assert.Equal(HttpStatusCode.Created, responseMsg.StatusCode);

            //Act
            HttpResponseMessage responseMsg1 = employeeController.Get(12);
            //Assert
            Assert.Equal(HttpStatusCode.OK, responseMsg1.StatusCode);
            String actualEmployeeContent = responseMsg1.Content.ReadAsStringAsync().Result;
            Employee actualEmployee = JsonConvert.DeserializeObject<Employee>(actualEmployeeContent);
            Assert.Equal(28, actualEmployee.Age);
        }
        [Fact]
        public void Test_PostNewEmployee_InvalidAge_Return_BadRequest()
        {
            EmployeeController employeeController = new EmployeeController();
            Employee emp = new Employee() { EmployeeId = 13, Name = "Ramu", Age = 18, Department = "IT", Salary = 15000.50 };
            employeeController.Request = new HttpRequestMessage();
            employeeController.Request.SetConfiguration(new HttpConfiguration());
            HttpResponseMessage responseMsg = employeeController.Post(emp);
            Assert.Equal(HttpStatusCode.BadRequest, responseMsg.StatusCode);

            //Act
            HttpResponseMessage responseMsg1 = employeeController.Get(13);
            //Assert
            Assert.Equal(HttpStatusCode.NotFound, responseMsg1.StatusCode);

            string str = responseMsg.Content.ReadAsStringAsync().Result;
            Employee actualEmployee = JsonConvert.DeserializeObject<Employee>(str);
            string Expected = "Input parameters are missing or incorrect";
            Assert.Equal(Expected, actualEmployee.Message);
        }
    }
}
