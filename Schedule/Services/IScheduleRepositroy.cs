using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels.Models;

namespace ScheduleApiService.Services
{
    public interface IScheduleRepositroy
    {
        Schedule AddInventory(Schedule schedule);

        IList<Schedule> Search(Schedule schedule);

        void SetTicketActiveFlag(int airlineRecId, bool isActive);

        void ReduceTicketCount(int scheduleRecId, bool isBcs, int noOfSeats);

        void IncreaseTicketCount(int scheduleRecId, bool isBcs, int noOfSeats);

        Discount AddDiscount(Discount discount);

        Discount GetDiscount(string discountCode);

        List<Discount> GetAllDiscount();

        IList<Schedule> GetAllSchedule();

        List<Meal> GetMealTypes();

        List<ScheduledDays> GetScheduledTypes();

        List<Location> GetPlaces();
    }
}
