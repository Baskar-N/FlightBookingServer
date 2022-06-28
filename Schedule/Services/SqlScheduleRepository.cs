using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApiService.Services
{
    public class SqlScheduleRepository : IScheduleRepositroy
    {
        private readonly AppDbContext _appDbContext;

        public SqlScheduleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Schedule AddInventory(Schedule schedule)
        {
            var scheduleItems = _appDbContext.Schedule;
            scheduleItems.Add(schedule);

            _appDbContext.SaveChanges();

            return schedule;
        }

        public IList<Schedule> GetAllSchedule()
        {
            var scheduleItems = from s in _appDbContext.Schedule
                                join mt in _appDbContext.Meal on s.MealTypeRecId equals mt.MealTypeRecId
                                join st in _appDbContext.ScheduledDaysType on s.ScheduledDaysRecId equals st.ScheduledDaysRecId
                                select new Schedule
                                {
                                    ScheduleRecId = s.ScheduleRecId,
                                    AirlineId = s.AirlineId,
                                    FlightNumber = s.FlightNumber,
                                    FromPlace = s.FromPlace,
                                    ToPlace = s.ToPlace,
                                    StartDateTime = s.StartDateTime,
                                    EndDateTime = s.EndDateTime,
                                    ScheduledDaysRecId = s.ScheduledDaysRecId,
                                    InstrumentUsed = s.InstrumentUsed,
                                    Bcs = s.Bcs,
                                    NonBcs = s.NonBcs,
                                    TicketCost = s.TicketCost,
                                    MealTypeRecId = s.MealTypeRecId,
                                    MealType = mt.MealType,
                                    ScheduleType = st.ScheduledType,
                                    Airline = s.Airline,
                                    IsActive = s.IsActive
                                };

            return scheduleItems.ToList();
        }

        public IList<Schedule> Search(Schedule schedule)
        {
            //var scheduleItem = _appDbContext.Schedule.Where(item =>
            //                            item.FromPlace == schedule.FromPlace && item.ToPlace == schedule.ToPlace
            //                            && item.StartDateTime == schedule.StartDateTime && item.IsActive);

            var scheduleItems = from s in _appDbContext.Schedule
                                join a in _appDbContext.Airline on s.AirlineId equals a.AirlineId into si
                                from al in si.DefaultIfEmpty()
                                where
                                    s.FromPlace == schedule.FromPlace && s.ToPlace == schedule.ToPlace
                                        && s.StartDateTime >= schedule.StartDateTime
                                        && s.IsActive
                                        && al.IsActive
                                        // we can use this for without updating IsActive flag in schedules while blocking
                                        // the airlines
                                select new Schedule
                                {
                                    ScheduleRecId = s.ScheduleRecId,
                                    AirlineId = s.AirlineId,
                                    FlightNumber = s.FlightNumber,
                                    FromPlace = s.FromPlace,
                                    ToPlace = s.ToPlace,
                                    StartDateTime = s.StartDateTime,
                                    EndDateTime = s.EndDateTime,
                                    ScheduledDaysRecId = s.ScheduledDaysRecId,
                                    InstrumentUsed = s.InstrumentUsed,
                                    Bcs = s.Bcs,
                                    NonBcs = s.NonBcs,
                                    TicketCost = s.TicketCost,
                                    MealType =s.MealType,
                                    Airline = s.Airline,
                                    IsActive = s.IsActive
                                };

            return scheduleItems.ToList();
        }

        public void SetTicketActiveFlag(int airlineRecId, bool isActive)
        {
            var scheduleItems = _appDbContext.Schedule.Where(item => item.AirlineId == airlineRecId);

            foreach(var item in scheduleItems)
            {
                item.IsActive = isActive;
            }

            _appDbContext.SaveChanges();
        }

        public void ReduceTicketCount(int scheduleRecId, bool isBcs, int noOfSeats)
        {
            var scheduleItems = _appDbContext.Schedule.Where(item => item.ScheduleRecId == scheduleRecId);

            foreach (var item in scheduleItems)
            {
                if(isBcs)
                {
                    item.Bcs -= noOfSeats;
                }
                else
                {
                    item.NonBcs -= noOfSeats;
                }
            }

            _appDbContext.SaveChanges();
        }

        public void IncreaseTicketCount(int scheduleRecId, bool isBcs, int noOfSeats)
        {
            var scheduleItems = _appDbContext.Schedule.Where(item => item.ScheduleRecId == scheduleRecId);

            foreach (var item in scheduleItems)
            {
                if (isBcs)
                {
                    item.Bcs += noOfSeats;
                }
                else
                {
                    item.NonBcs += noOfSeats;
                }
            }

            _appDbContext.SaveChanges();
        }

        public Discount AddDiscount(Discount discount)
        {
            var discountItems = _appDbContext.Discount;
            discountItems.Add(discount);

            _appDbContext.SaveChanges();

            return discount;
        }

        public Discount GetDiscount(string discountCode)
        {
            return _appDbContext.Discount.FirstOrDefault(item => item.DiscountCode == discountCode);
        }

        public List<Discount> GetAllDiscount()
        {
            return _appDbContext.Discount.ToList();
        }

        public List<Meal> GetMealTypes()
        {
            return _appDbContext.Meal.ToList();
        }

        public List<ScheduledDays> GetScheduledTypes()
        {
            return _appDbContext.ScheduledDaysType.ToList();
        }

        public List<Location> GetPlaces()
        {
            return _appDbContext.Location.ToList();
        }
    }
}
