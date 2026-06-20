using System;
using System.Collections.Generic;
using System.Text;

namespace TouristDestinations_Component2.Interfaces
{
	public interface IVisitData
	{
		Dictionary<string, List<DestinationVisit>> GetData();
	}
}
