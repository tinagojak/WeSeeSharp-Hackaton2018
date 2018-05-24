using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackaton2018.Models.DbModels
{
    public class BloodDonorRecord
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public DateTime? Date { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}