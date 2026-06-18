using System;
using System.Collections.Generic;
using System.Text;

namespace TouristDestinations
{
    public abstract class DestinationRepository : IDestinationRepository
    {
        public void Add(TouristDestination destination)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Edit(TouristDestination destination)
        {
            throw new NotImplementedException();
        }

        public List<TouristDestination> GetAll()
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
