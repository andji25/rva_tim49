using TouristDestinations_Component2.Interfaces;

namespace TouristDestinations_Component2.Services
{
    public class StatisticsService
    {
        private IStatisticsStrategy strategy;
        private IVisitData visitData;
        private IWriter writer;

        public StatisticsService(IVisitData visitData, IWriter writer)
        {
            this.visitData = visitData;
            this.writer = writer;
        }

        public void SetStrategy(IStatisticsStrategy strategy)
        {
            this.strategy = strategy;
        }

        public string Calculate()
        {
            var data = visitData.GetData();
            return strategy.Calculate(data);
        }

        public void WriteResults(string results)
        {
            writer.Write(results);
        }
    }
}