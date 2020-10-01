using CreateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace CreateAPI.Controllers
{
    public class EmployeeController : ApiController
    {

        // GET api/employee
        [Route("api/employee")]
        public HttpResponseMessage Get()
        {
            var employees = EmployeeRepo.GetEmployees();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employees);
            return response;
        }
        // GET api/employees/id
        [Route("api/employees/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            Employee emp = EmployeeRepo.GetEmployeeById(id);
            if (emp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee doesn't exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, emp);
        }

        [Route("api/employees")]
        public HttpResponseMessage Post(Employee e)
        {
            var emp = EmployeeRepo.InsertEmployee(e);
            if (emp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Input parameters are missing or incorrect");
            }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, emp);
            return response;
        }

        public HttpResponseMessage GetForEmptyRecord()
        {
            var employees = EmployeeRepo.GetEmployees_emptyRecord();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, employees);
            return response;
        }

    }
}
