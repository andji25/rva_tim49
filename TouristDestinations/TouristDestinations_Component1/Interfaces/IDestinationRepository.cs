using System;
using System.Collections.Generic;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Interfaces
{
    public interface IDestinationRepository
    {
        void Add(TouristDestination destination);

        void Edit(TouristDestination destination);

        void Delete(Guid id);

        List<TouristDestination> GetAll();

        void Load();

        void Save();
    }
}
