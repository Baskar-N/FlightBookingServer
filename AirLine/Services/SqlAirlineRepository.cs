using AirlineApiService.Services;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineApiService.Services
{
    public class SqlAirlineRepository : IAirlineRepository
    {
        private AppDbContext _appDbContext;

        public SqlAirlineRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // get all airline details
        public IEnumerable<Airline> GetAllAirline(bool IsShowInactiveAlso)
        {
            if(IsShowInactiveAlso)
            {
                return _appDbContext.Airline;
            }

            return _appDbContext.Airline.Where(item => item.IsActive);
        }

        // register new airline
        public Airline Register(Airline airline)
        {
            airline.IsActive = true;
            _appDbContext.Airline.Add(airline);
            _appDbContext.SaveChanges();

            return airline;
        }

        // block the airline details by airline id
        public Airline BlockAirline(int airlineRecId)
        {
            var airline = _appDbContext.Airline.FirstOrDefault(item => item.AirlineId == airlineRecId);

            if(airline != null)
            {
                airline.IsActive = !airline.IsActive;

                // call schedules micro service(consumer).
                QueueProducer.SetTicketActiveFlag(airlineRecId, airline.IsActive);

                _appDbContext.SaveChanges();
            }

            return airline;
        }
    }
}
