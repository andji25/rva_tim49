using System;
using System.Collections.Generic;
using System.Text;
using TouristDestinations_Component2.Interfaces;

namespace TouristDestinations_Component2.Services
{
	public class StatisticsService
	{
		IStatisticsStrategy strategy;
		IVisitData visitData;
		IWriter writer;

		public void SetStrategy(IStatisticsStrategy strategy)
		{
			throw new NotImplementedException();
		}

		public string Calculate(Guid destinationId, DateTime from, DateTime to)
		{
			throw new NotImplementedException();
		}

		public void WriteResults(string results)
		{
			throw new NotImplementedException();
		}
	}
}
