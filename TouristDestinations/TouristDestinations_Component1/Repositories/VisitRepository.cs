using System;
using System.Collections.Generic;
using System.Text;
using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Repositories
{
    public abstract class VisitRepository : IVisitRepository
    {
        protected List<DestinationVisit> visits;

        public VisitRepository()
        {
            visits = new List<DestinationVisit>();
        }

        public void Add(DestinationVisit visit)
        {
            visits.Add(visit);
            Save();
        }

        public void Delete(Guid destinationId, DateTime dateOfVisit)
        {
            visits.RemoveAll(v => v.DestinationId == destinationId && v.DateOfVisit == dateOfVisit);
            Save();
        }

        public void Edit(DestinationVisit oldVisit, DestinationVisit newVisit)
        {
            int index = visits.FindIndex(v => v.DestinationId == oldVisit.DestinationId && v.DateOfVisit == oldVisit.DateOfVisit);
            if (index != -1)
            {
                visits[index] = newVisit;
                Save();
            }
        }

        public List<DestinationVisit> GetAll()
        {
            return visits;
        }

        public abstract void Load();
        public abstract void Save();
    }
}
