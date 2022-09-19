using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMicroservice.Models
{
    public class Employee
    {

        public int EmployeeId { get; set; }


        public string EmployeeName { get; set; }


        public long ContactNo { get; set; }

        public string EmailId { get; set; }
        public string Password{get;set;}


    }
}
