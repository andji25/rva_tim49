using System;
using System.Collections.Generic;
using System.Text;
using TouristDestinations_Component1.Interfaces;
using TouristDestinations_Component1.Models;

namespace TouristDestinations_Component1.Services
{
	public class VisitTracker
	{
		private List<IObserver> observers;
		private List<DestinationVisit> visits;
		private IVisitRepository repository;

        public VisitTracker(IVisitRepository repository)
        {
            this.repository = repository;
            observers = new List<IObserver>();
            visits = new List<DestinationVisit>();
        }

        public void Register(IObserver observer)
		{
			observers.Add(observer);
		}

		public void Unregister(IObserver observer)
		{
			observers.Remove(observer);
		}

		public void NotifyObservers()
		{
            foreach (IObserver observer in observers)
                observer.Update(visits[visits.Count - 1]);
        }

		public void AddVisit(DestinationVisit visit)
		{
            visits.Add(visit);
            NotifyObservers();
        }
	}
}
