using System;
using System.Collections.Generic;
using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Repositories
{
    public abstract class DestinationRepository : IDestinationRepository
    {
        protected List<TouristDestination> destinations;

        public DestinationRepository()
        {
            destinations = new List<TouristDestination>();
        }

        public void Add(TouristDestination destination)
        {
            destinations.Add(destination);
            Save();
        }

        public void Delete(Guid id)
        {
            destinations.RemoveAll(d => d.Id == id);
            Save();
        }

        public void Edit(TouristDestination destination)
        {
            int index = destinations.FindIndex(d => d.Id == destination.Id);
            if (index != -1)
            {
                destinations[index] = destination;
                Save();
            }
        }

        public List<TouristDestination> GetAll()
        {
            return destinations;
        }

        public abstract void Load();
        public abstract void Save();
    }
}
