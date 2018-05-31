using RichmondGrpLib.Entities;
using RichmondGrpLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondGrpLib.Services
{
    public class ScheduleService
    {




        /// <summary>
        /// Find if engineer assigned scheduled for yesterday or tomorrow
        /// 
        /// </summary>
        /// <param name="selectedDay">Day in which selection is going to made</param>
        /// <param name="engineerId">Engineer id</param>
        /// <returns>boolean value</returns>

        public bool IsEngineerAssignedScheduleForConsecativeDays(DateTime selectedDay, int engineerId)
        {
            bool result = false;
            try
            {
                using (var ctx = new DB_121539_alkeshpatelfEntities())
                {
                    DateTime pastDay = selectedDay.AddDays(-1);
                    DateTime futureDay = selectedDay.AddDays(1);
                    if (selectedDay.DayOfWeek == DayOfWeek.Monday)
                    {
                        pastDay = selectedDay.AddDays(-3);
                    }

                    if (selectedDay.DayOfWeek == DayOfWeek.Friday)
                    {
                        futureDay = selectedDay.AddDays(3);
                    }
                    var scheduleItem = ctx.Schedules.Where(s => s.ScheduleEngineer != null && s.ScheduleEngineer.Value == engineerId && s.ScheduleDate >= pastDay && s.ScheduleDate <= futureDay).FirstOrDefault();
                    if (scheduleItem != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Find if engineer assigned number of scheduled for two weeks span
        /// 
        /// </summary>
        /// <param name="selectedDay">Day in which selection is going to made</param>
        /// <param name="engineerId">Engineer id</param>
        /// <returns>int value</returns>

        public int GetEngineerAssignedScheduleForTwoWeeks(DateTime selectedDay, int engineerId)
        {
            int result = 0;
            try
            {
                using (var ctx = new DB_121539_alkeshpatelfEntities())
                {
                    DateTime pastDay = selectedDay.AddDays(-7);
                    DateTime futureDay = selectedDay.AddDays(7);
                    var scheduleItems = ctx.Schedules.Where(s => s.ScheduleEngineer != null && s.ScheduleEngineer.Value == engineerId && s.ScheduleDate >= pastDay && s.ScheduleDate <= futureDay);
                    if (scheduleItems.Any())
                    {
                        return scheduleItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }


        /// <summary>
        /// Find number of schedule for selected date
        /// 
        /// </summary>
        /// <param name="selectedDay">Day in which selection is going to made</param>
        /// <returns>int value</returns>

        public int GetNumberofScheduleForDate(DateTime selectedDay)
        {
            int result = 0;
            try
            {
                using (var ctx = new DB_121539_alkeshpatelfEntities())
                {
                    var scheduleItems = ctx.Schedules.Where(s => s.ScheduleDate == selectedDay);
                    if (scheduleItems.Any())
                    {
                        return scheduleItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Get all schedules for selected dates
        /// 
        /// </summary>
        /// <param name="startDate">start date from where selection begins</param>
        /// <param name="endDate">end date from where selection ends</param>
        /// <returns>List<CalendarModel></returns>

        public List<CalendarModel> GetAssignedSchedules(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var ctx = new DB_121539_alkeshpatelfEntities())
                {
                    var query = (from s in ctx.Schedules
                                 join e in ctx.Engineers on s.ScheduleEngineer equals e.EngineerId
                                 where s.ScheduleDate >= startDate && s.ScheduleDate <= endDate
                                 select new CalendarModel()
                                 {
                                     scheduleDate = s.ScheduleDate,
                                     engineerId = s.ScheduleEngineer,
                                     title = e.EngineerName,
                                     period = s.SchedulePeriod,
                                     createDate = s.CreateDate
                                 });
                    if (query.Any())
                    {
                        List<CalendarModel> result = query.OrderBy(s => s.createDate).ToList();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        /// <summary>
        /// Assign Engineer
        /// 
        /// </summary>
        /// <param name="selectedDate">Date on which engineer selected</param>
        /// <returns>int value</returns>

        public int AssignEngineer(DateTime selectedDate)
        {
            try
            {
                EngineerService engineerService = new EngineerService();
                EngineerModel engineerModel = engineerService.GetQualifiedEngineer(selectedDate);

                if (engineerModel != null)
                {
                    using (var ctx = new DB_121539_alkeshpatelfEntities())
                    {
                        var scheduleItems = ctx.Schedules.Where(s => s.ScheduleDate == selectedDate);
                        if (scheduleItems.Count() < 2)
                        {
                            var period = scheduleItems.Select(s => s.SchedulePeriod).FirstOrDefault();
                            int engineerPeriod = 1;
                            if (period == 1)
                            {
                                engineerPeriod = 2;
                            }
                            Schedule schedule = new Schedule();
                            schedule.SchedulePeriod = engineerPeriod;
                            schedule.ScheduleEngineer = engineerModel.EngineerId;
                            schedule.CreateDate = DateTime.Now;
                            schedule.UpdateDate = DateTime.Now;
                            schedule.ScheduleDate = selectedDate;
                            ctx.Schedules.Add(schedule);
                            ctx.SaveChanges();
                            return schedule.ScheduleId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return 0;
        }


        /// <summary>
        /// Delete all schedules
        /// 
        /// </summary>
        /// <returns>int value</returns>

        public int DeleteAllSchedules()
        {
            try
            {
                using (var ctx = new DB_121539_alkeshpatelfEntities())
                {
                    var scheduleItems = ctx.Schedules.Where(s => s.ScheduleId > 0);
                    ctx.Schedules.RemoveRange(scheduleItems);
                    ctx.SaveChanges();
                    return 1;
                }

            }
            catch (Exception ex)
            {

            }
            return 0;
        }
    }
}
