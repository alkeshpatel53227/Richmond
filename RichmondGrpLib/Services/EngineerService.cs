using RichmondGrpLib.Entities;
using RichmondGrpLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichmondGrpLib.Services
{
    public class EngineerService
    {
        private static Random rnd = new Random();

        /// <summary>
        /// Get all Engineers
        /// 
        /// </summary>
         /// <returns>List<EngineerModel></returns>

        public List<EngineerModel> GetAllEngineers()
        {
            try
            {
                using (var ctx = new DB_121539_alkeshpatelfEntities())
                {
                    return (from c in ctx.Engineers
                            select new EngineerModel()
                            {
                                EngineerId = c.EngineerId,
                                EngineerName = c.EngineerName,
                                CreateDate = c.CreateDate,
                                CreateUserId = c.CreateUserId,
                                UpdateDate = c.UpdateDate,
                                UpdateUserId = c.UpdateUserId
                            }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return new List<EngineerModel>();
        }


        /// <summary>
        /// Get single qualified engineer which meets all requirement
        /// 
        /// </summary>
        /// <param name="selectedDate">selected date on which engineer will be added</param>
        /// <returns>EngineerModel</returns>

        public EngineerModel GetQualifiedEngineer(DateTime selectedDate)
        {
            try
            {
                using (var ctx = new DB_121539_alkeshpatelfEntities())
                {
                    ScheduleService scheduleService = new ScheduleService();
                    List<EngineerModel> items = GetAllEngineers();
                    var engineerIds = items.Select(s => s.EngineerId).ToList();
                    
                    //Check for engineer which has not been assigned to schedule twice in last 13 days or future 13 days
                    DateTime pastMinTwo = FindWorkDay(selectedDate, -9, true);
                    DateTime futureMinTwo = FindWorkDay(selectedDate, 9, false);
                    int pastMinTwoTotalRows = ctx.Schedules.Where(c => c.ScheduleDate >= pastMinTwo).Count();
                    if (pastMinTwoTotalRows >= 14)
                    {
                        var lastThirteenDaysCheck = ctx.Schedules.Where(c => c.ScheduleDate >= pastMinTwo).GroupBy(i => i.ScheduleEngineer)
                    .Where(x => x.Count() > 1)
                    .Select(val => val.Key);
                        List<int> notHitIds = new List<int>();
                        for (var i = 0; i < engineerIds.Count(); i++)
                        {
                            bool itemHit = false;
                            foreach (var item in lastThirteenDaysCheck)
                            {


                                if (item.Value == engineerIds[i])
                                {
                                    itemHit = true;
                                    break;
                                }
                            }
                            if (!itemHit)
                            {
                               
                                if (!scheduleService.IsEngineerAssignedScheduleForConsecativeDays(selectedDate, engineerIds[i]))
                                {
                                    notHitIds.Add(engineerIds[i]);
                                   
                                }
                            }
                        }
                        if (notHitIds.Count > 0 )
                        {
                            EngineerModel missingEngineerModel = new EngineerModel();
                            missingEngineerModel.EngineerId = notHitIds[0];
                            return missingEngineerModel;
                        }
                    }
                    int futureMinTwoTotalRows = ctx.Schedules.Where(c => c.ScheduleDate <= futureMinTwo).Count();
                    if (futureMinTwoTotalRows >= 14)
                    {
                        var futureThirteenDaysCheck = ctx.Schedules.Where(c => c.ScheduleDate <= futureMinTwo).GroupBy(i => i.ScheduleEngineer)
                   .Where(x => x.Count() > 1)
                   .Select(val => val.Key);

                        List<int> notHitIds = new List<int>();
                        for (var i = 0; i < engineerIds.Count(); i++)
                        {
                            bool itemHit = false;
                            foreach (var item in futureThirteenDaysCheck)
                            {


                                if (item.Value == engineerIds[i])
                                {
                                    itemHit = true;
                                    break;
                                }
                            }
                            if (!itemHit)
                            {

                                if (!scheduleService.IsEngineerAssignedScheduleForConsecativeDays(selectedDate, engineerIds[i]))
                                {
                                    notHitIds.Add(engineerIds[i]);

                                }
                            }
                        }
                        if (notHitIds.Count > 0)
                        {
                            EngineerModel missingEngineerModel = new EngineerModel();
                            missingEngineerModel.EngineerId = notHitIds[0];
                            return missingEngineerModel;
                        }
                    }
                    //Check for engineer which has not been assigned to schedule atleast once in last 11 days or future 11 days
                    DateTime pastMinOne = FindWorkDay(selectedDate, -7, true);
                    DateTime futureMinOne = FindWorkDay(selectedDate, 7, false);
                    int pastMinOneTotalRows = ctx.Schedules.Where(c => c.ScheduleDate >= pastMinOne).Count();
                    if (pastMinOneTotalRows > 8 && pastMinOneTotalRows < 14 )
                    {
                        var lastElevenDaysCheck = ctx.Schedules.Where(c => c.ScheduleDate >= pastMinOne).GroupBy(i => i.ScheduleEngineer)
                    .Where(x => x.Count() > 0)
                    .Select(val => val.Key);
                        List<int> notHitIds = new List<int>();
                        for (var i = 0; i < engineerIds.Count(); i++)
                        {
                            bool itemHit = false;
                            foreach (var item in lastElevenDaysCheck)
                            {


                                if (item.Value == engineerIds[i])
                                {
                                    itemHit = true;
                                    break;
                                }
                            }
                            if (!itemHit)
                            {

                                if (!scheduleService.IsEngineerAssignedScheduleForConsecativeDays(selectedDate, engineerIds[i]))
                                {
                                    notHitIds.Add(engineerIds[i]);

                                }
                            }
                        }
                        if (notHitIds.Count > 0)
                        {
                            EngineerModel missingEngineerModel = new EngineerModel();
                            missingEngineerModel.EngineerId = notHitIds[0];
                            return missingEngineerModel;
                        }

                    }
                    int futureMinOneTotalRows = ctx.Schedules.Where(c => c.ScheduleDate <= futureMinOne).Count();
                    if (futureMinOneTotalRows > 8 && futureMinOneTotalRows < 14)
                    {
                        var futureElevenDaysCheck = ctx.Schedules.Where(c => c.ScheduleDate <= futureMinOne).GroupBy(i => i.ScheduleEngineer)
                   .Where(x => x.Count() > 0)
                   .Select(val => val.Key);

                        List<int> notHitIds = new List<int>();
                        for (var i = 0; i < engineerIds.Count(); i++)
                        {
                            bool itemHit = false;
                            foreach (var item in futureElevenDaysCheck)
                            {


                                if (item.Value == engineerIds[i])
                                {
                                    itemHit = true;
                                    break;
                                }
                            }
                            if (!itemHit)
                            {

                                if (!scheduleService.IsEngineerAssignedScheduleForConsecativeDays(selectedDate, engineerIds[i]))
                                {
                                    notHitIds.Add(engineerIds[i]);

                                }
                            }
                        }
                        if (notHitIds.Count > 0)
                        {
                            EngineerModel missingEngineerModel = new EngineerModel();
                            missingEngineerModel.EngineerId = notHitIds[0];
                            return missingEngineerModel;
                        }
                    }
                    EngineerModel selectedEngineer = null;
                   
                    while (selectedEngineer == null)
                    {
                        int r = rnd.Next(items.Count);
                        if (!scheduleService.IsEngineerAssignedScheduleForConsecativeDays(selectedDate, items[r].EngineerId))
                        {
                            selectedEngineer = items[r];
                        }
                    }
                    return selectedEngineer;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// Find Date range to match the schedules
        /// 
        /// </summary>
        /// <param name="date">current date which needs to be compared</param>
        /// <param name="days">number of days to find the range</param>
        /// <param name="past">flag to weather check date in the past or future</param>
        /// <returns>DateTime</returns>

        private DateTime FindWorkDay(DateTime date, int days, bool past)
        {
            int items = 0;
            int resultCounter = 0;
            DateTime resultDate = DateTime.Now;
            while (resultCounter != days)
            {
                if (past)
                {
                    items = items - 1;
                }
                else
                {
                    items = items + 1;
                }
                resultDate = date.AddDays(items);
                if (!IsWeekend(resultDate))
                {
                    if (past)
                    {
                        resultCounter = resultCounter - 1;
                    }
                    else
                    {
                        resultCounter = resultCounter + 1;
                    }
                }

            }

            return resultDate;
        }

        /// <summary>
        /// Comfirm if given date is weekend or not
        /// 
        /// </summary>
        /// <param name="date">Check if the given date is weekend</param>
        /// <returns>bool</returns>

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Get Qualified engineer which meet criteria
        /// 
        /// </summary>
        /// <param name="selectedDate">Current date which will be used for matching engineer</param>
        /// <param name="engineerModel">Engineer which will be compared</param>
        /// <returns>EngineerModel</returns>

        public EngineerModel GetQualifiedEngineerFromRandom(DateTime selectedDate, EngineerModel engineerModel)
        {
            try
            {
                ScheduleService scheduleService = new ScheduleService();
                if (!scheduleService.IsEngineerAssignedScheduleForConsecativeDays(selectedDate, engineerModel.EngineerId) && scheduleService.GetEngineerAssignedScheduleForTwoWeeks(selectedDate, engineerModel.EngineerId) < 2)
                {
                    return engineerModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
