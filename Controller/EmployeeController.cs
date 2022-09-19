using EmployeeMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using OfferMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeMicroservice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        OfferHelper _api = new OfferHelper();
        public static List<Employee> Employees;

        public EmployeeController()
        {
            Employees = new List<Employee>()
            {
                new Employee{EmployeeId=101, EmployeeName="Pranav", ContactNo=9874561235,EmailId="pranav@gmail.com",Password="12345"},
                new Employee{EmployeeId=102, EmployeeName="Jayakumaran", ContactNo=9638527415,EmailId="jk@gmail.com",Password="12345"},
                new Employee{EmployeeId=103, EmployeeName="Nikhil", ContactNo=9517534628,EmailId="nik@gmail.com",Password="12345"},
                new Employee{EmployeeId=104, EmployeeName="Bhavesh", ContactNo=9871236548,EmailId="bhavesh@gmail.com",Password="12345"},
                new Employee{EmployeeId=105, EmployeeName="Harini", ContactNo=9658471237,EmailId="harini@gmail.com",Password="11111"},
                new Employee{EmployeeId=106, EmployeeName="Vishwa", ContactNo=8564231795,EmailId="vishwa@gmail.com",Password="11111"},
                new Employee{EmployeeId=107, EmployeeName="Rohan", ContactNo=8994562177,EmailId="rohan@gmail.com",Password="11111"},
                new Employee{EmployeeId=108, EmployeeName="Rohini", ContactNo=9633554872,EmailId="rohini@gmail.com",Password="11111"},
                new Employee{EmployeeId=109, EmployeeName="Ragavan", ContactNo=7754269983,EmailId="ragavan@gmail.com",Password="11111"},
                new Employee{EmployeeId=110, EmployeeName="Hari", ContactNo=9884756823,EmailId="hari@gmail.com",Password="11111"},
                new Employee{EmployeeId=111, EmployeeName="Vignesh", ContactNo=7895546128,EmailId="vigneshgmail.com",Password="11111"},
                new Employee{EmployeeId=112, EmployeeName="Suresh", ContactNo=8899456218,EmailId="suresh@gmail.com",Password="11111"}

            };
        }

        [HttpGet]
        [Route("GetEmployeeList")]
        public ActionResult<List<Employee>> GetEmployeeList()
        {
            return Employees;
        }

        //View All Offers
        [HttpGet()]
        [Route("ViewEmployeeOffers/{employeeId}")]
        public async Task<ActionResult<IList<OfferData>>> ViewEmployeeOffers(int employeeId)
        {
            List<OfferData> offers = new List<OfferData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("https://localhost:44389/api/Offer/GetOffersList");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                offers = JsonConvert.DeserializeObject<List<OfferData>>(results);
            }
            var employeeOffers = offers.Where(c => c.EmployeeId == employeeId).ToList();

            if (employeeOffers.Count == 0)
            {
                return NotFound("No offers found");
            }
            return employeeOffers;

        }

        

        [HttpGet]
        [Route("ViewMostLikedOffers/{employeeId}")]
        public async Task<ActionResult<IList<OfferData>>> ViewMostLikedOffers(int employeeId)
        {

            List<OfferData> offers = new List<OfferData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("https://localhost:44389/api/Offer/GetOffersList");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                offers = JsonConvert.DeserializeObject<List<OfferData>>(results);
            }
            var offer = (from c in offers where c.EmployeeId==employeeId orderby c.Likes descending select c).Take(3).ToList();
            //List<OfferProvider> lists = offers.ToList();
            if(offer.Count==0)
            {
                return NotFound("No Offers Found");
            }
            return offer;

        }

        [HttpGet]
        [Route("GetPointsList")]
        public async Task<List<PointsData>> GetPointsList()
        {
            List<PointsData> offers = new List<PointsData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/points");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                offers = JsonConvert.DeserializeObject<List<PointsData>>(results);
            }

            //List<OfferDto> lists = offers.ToList();
            return offers;
        }
    }
}
