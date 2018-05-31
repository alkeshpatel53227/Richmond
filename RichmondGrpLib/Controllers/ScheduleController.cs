using RichmondGrpLib.Models;
using RichmondGrpLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondGrpLib.Controllers
{
    public class ScheduleController
    {
        /// <summary>
        /// Get all schedules for selected dates
        /// 
        /// </summary>
        /// <param name="startDate">start date from where selection begins</param>
        /// <param name="endDate">end date from where selection ends</param>
        /// <returns>List<CalendarModel></returns>

        public List<CalendarModel> GetAssignedSchedules(DateTime startDate, DateTime endDate)
        {
            ScheduleService scheduleService = new ScheduleService();
            List<CalendarModel> result =  scheduleService.GetAssignedSchedules(startDate,endDate);
            return result;
        }

        /// <summary>
        /// Assign Engineer
        /// 
        /// </summary>
        /// <param name="selectedDate">Date on which engineer selected</param>
        /// <returns>int value</returns>

        public int AssignEngineer(DateTime selectedDate)
        {
            ScheduleService scheduleService = new ScheduleService();
            return scheduleService.AssignEngineer(selectedDate);
        }


        /// <summary>
        /// Get schedule counts for selected date 
        /// 
        /// </summary>
        /// <param name="selectedDate">Date on which engineer selected</param>
        /// <returns>int value</returns>

        public int GetNumberofScheduleForDate(DateTime selectedDate)
        {
            ScheduleService scheduleService = new ScheduleService();
            return scheduleService.GetNumberofScheduleForDate(selectedDate);
        }

        /// <summary>
        /// Delete all schedules
        /// 
        /// </summary>
         /// <returns>int value</returns>

        public int DeleteAllSchedules()
        {
            ScheduleService scheduleService = new ScheduleService();
            return scheduleService.DeleteAllSchedules();
        }
    }
}
