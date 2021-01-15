namespace Monblank
{
    using System.Collections.Generic;

    public class MonitoringStub : IMonitoring
    {
        public StopWatch Start(string @group, string monitorName, Dictionary<string, string> properties)
        {
            return new StopWatch().Start();
        }

        public StopWatch Start(string @group, string monitorName, IDictionary<string, string> properties)
        {
	        return new StopWatch().Start();
        }

        public StopWatch Start(string @group, string monitorName)
        {
            return new StopWatch().Start();
        }

        public StopWatch Start(MonitorKey key)
        {
            return new StopWatch().Start();
        }

        public void Add(string @group, string monitorName, string units, double value, IDictionary<string, string> properties)
        {
	        //NOP
        }

        public void Add(string @group, string monitorName, string units, double value, Dictionary<string, string> properties)
        {
            //NOP
        }

        public void Add(string @group, string monitorName, string units, double value)
        {
            //NOP
        }

        public void Add(MonitorKey key, double value)
        {
            //NOP
        }

        public void Set(string @group, string monitorName, string units, double value, IDictionary<string, string> properties)
        {
	        //NOP
        }

        public void Set(string @group, string monitorName, string units, double value)
        {
	        //NOP
        }

        public void Set(MonitorKey key, double value)
        {
	        //NOP
        }

        public void Inc(string @group, string monitorName, IDictionary<string, string> properties)
        {
	        //NOP
        }

        public void Inc(string @group, string monitorName, Dictionary<string, string> properties)
        {
            //NOP
        }

        public void Inc(string @group, string monitorName)
        {
            //NOP
        }

        public void Inc(MonitorKey key)
        {
            //NOP
        }

        public List<Counter> Reset()
        {
            return null;
        }
    }
}