using System;
using System.Collections.Generic;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Interfaces
{
    public interface IVisitRepository
    {
        void Add(DestinationVisit visit);

        void Edit(DestinationVisit visit);

        void Delete(Guid destinationId, DateTime dateOfVisit);

        List<DestinationVisit> GetAll();

        void Load();

        void Save();
    }
}
