using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondGrpLib.Models
{
   public class CalendarModel
    {
        public string title { get; set; }

        public int? engineerId { get; set; }
       
        public DateTime? scheduleDate { get; set; }
        public DateTime createDate { get; set; }
        public string start => GetDateStartString(period, scheduleDate);
        public string end => GetDateEndString(period, scheduleDate);
        public int? period { get; set; }

        public string GetDateStartString(int? periodvalue, DateTime? scheduleDatevalue)
        {
            if(periodvalue != null && scheduleDatevalue != null)
            {
                if(period == 1)
                {
                    return scheduleDatevalue.Value.ToString("yyyy-MM-dd")+" 09:00:00";
                }else
                {
                    return scheduleDatevalue.Value.ToString("yyyy-MM-dd") + " 13:00:00";
                }
            }
            return "";
        }

        public string GetDateEndString(int? periodvalue, DateTime? scheduleDatevalue)
        {
            if (periodvalue != null && scheduleDatevalue != null)
            {
                if (period == 1)
                {
                    return scheduleDatevalue.Value.ToString("yyyy-MM-dd") + " 13:00:00";
                }
                else
                {
                    return scheduleDatevalue.Value.ToString("yyyy-MM-dd") + " 17:00:00";
                }
            }
            return "";
        }
    }
}
