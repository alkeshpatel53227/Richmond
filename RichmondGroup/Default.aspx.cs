using RichmondGrpLib.Controllers;
using RichmondGrpLib.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RichmondGroup
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Get all schedules for selected dates
        /// 
        /// </summary>
        /// <param name="startDate">start date from where selection begins</param>
        /// <param name="endDate">end date from where selection ends</param>
        /// <returns>List<CalendarModel></returns>

        [WebMethod]
        public static List<CalendarModel> GetAssignedSchedules()
        {
            string fromString = DateTime.Now.ToString("MM/dd/yyyy");
            string toString = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["fromString"]))
            {
                fromString = HttpContext.Current.Request.QueryString["fromString"].ToString();
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["toString"]))
            {
                toString = HttpContext.Current.Request.QueryString["toString"].ToString();
            }
            ScheduleController controller = new ScheduleController();
            DateTime fromDate = DateTime.Parse(fromString);
            DateTime toDate = DateTime.Parse(toString);
            return controller.GetAssignedSchedules(fromDate, toDate);
        }


        /// <summary>
        /// Get all Engineers assigned for the date
        /// 
        /// </summary>
        /// <param name="selectedDate">selected date on which the engineer are assigned</param>
        /// <returns>int</returns>

        [WebMethod]
        public static int AssignEngineer()
        {
            string selectedDayString = DateTime.Now.ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["selectedDayString"]))
            {
                selectedDayString = HttpContext.Current.Request.QueryString["selectedDayString"].ToString();
            }
            ScheduleController controller = new ScheduleController();
            DateTime selectedDay = DateTime.Parse(selectedDayString);
            return controller.AssignEngineer(selectedDay);
        }


        /// <summary>
        /// Get all schedules assigned for the date
        /// 
        /// </summary>
        /// <param name="selectedDate">selected date on which the engineer are assigned</param>
        /// <returns>int</returns>

        [WebMethod]
        public static int GetNumberofScheduleForDate()
        {
            string selectedDayString = DateTime.Now.ToString("MM/dd/yyyy");
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["selectedDayString"]))
            {
                selectedDayString = HttpContext.Current.Request.QueryString["selectedDayString"].ToString();
            }
            ScheduleController controller = new ScheduleController();
            DateTime selectedDay = DateTime.Parse(selectedDayString);
            return controller.GetNumberofScheduleForDate(selectedDay);
        }


        /// <summary>
        /// Delete all schedules
        /// 
        /// </summary>
        /// <returns>int</returns>

        [WebMethod]
        public static int DeleteAllSchedules()
        {
            ScheduleController controller = new ScheduleController();
            return controller.DeleteAllSchedules();
        }
    }
}