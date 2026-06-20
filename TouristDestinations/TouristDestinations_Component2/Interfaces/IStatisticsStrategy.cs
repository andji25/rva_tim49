using System;
using System.Collections.Generic;
using System.Text;

namespace TouristDestinations_Component2.Interfaces
{
	public interface IStatisticsStrategy
	{
		string Calculate(Dictionary<string, List<DestinationVisit>> data);
	}
}
