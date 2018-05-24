using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackaton2018.Models.DbModels
{
    public class Questionnary
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
         
        public string Answer1 { get; set; }
    }
}