using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackaton2018.Models.DbModels
{
    public class BloodSupply
    {
        public int Id { get; set; }
        public BloodType BloodType { get; set; }
        public int BloodTypeId { get; set; }
        public int AmountStored { get; set; }
        public DateTime SupplyDate { get; set; }
    }
}