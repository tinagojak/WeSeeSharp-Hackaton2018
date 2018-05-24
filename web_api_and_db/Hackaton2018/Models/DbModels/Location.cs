using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackaton2018.Models.DbModels
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Address { get; set; }
    }
}