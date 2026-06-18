using System;
using System.Collections.Generic;
using System.Text;

namespace TouristDestinations
{
    public abstract class VisitRepository : IVisitRepository
    {
        public void Add(DestinationVisit visit)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Edit(DestinationVisit visit)
        {
            throw new NotImplementedException();
        }

        public List<DestinationVisit> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
