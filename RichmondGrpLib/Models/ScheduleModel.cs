using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondGrpLib.Models
{
   public class ScheduleModel
    {
        public int ScheduleId { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public Nullable<int> SchedulePeriod { get; set; }
        public Nullable<int> ScheduleEngineer { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string UpdateUserId { get; set; }

    }
}
