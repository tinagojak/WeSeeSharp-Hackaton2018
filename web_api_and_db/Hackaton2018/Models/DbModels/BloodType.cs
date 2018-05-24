using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackaton2018.Models.DbModels
{
    public class BloodType
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public int MaxAmountStored { get; set; }
        public int MinAmountStored { get; set; }
        public int OptimalAmountStored { get; set; }
        public int SpentAmountPerWeek { get; set; }

        public virtual ICollection<BloodType> BloodTypeTakes { get; set; }
        public virtual ICollection<BloodType> BloodTypeGives { get; set; }
    }
}