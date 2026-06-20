using System;
using System.Collections.Generic;
using System.Text;
using TouristDestinations_Component2.Interfaces;

namespace TouristDestinations_Component2.Adapter
{
	public class VisitAdapter : IVisitData
	{
		List<DestinationVisit> visits;

		public VisitAdapter()
		{
			throw new NotImplementedException();
		}

		public Dictionary<string, List<DestinationVisit>> GetData()
		{
			throw new NotImplementedException();
		}
	}
}
