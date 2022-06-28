using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineApiService.Services
{
    public interface IAirlineRepository
    {
        public Airline Register(Airline airline);

        public IEnumerable<Airline> GetAllAirline(bool IsShowInactiveAlso);

        public Airline BlockAirline(int airlineRecId);
    }
}
