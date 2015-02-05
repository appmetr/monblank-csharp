namespace Monblank.S2S
{
    #region using directives

    using System;
    using System.Collections.Generic;
    using Bridge;

    #endregion

    public class MonitoringS2SImpl : IMonitoring
    {
        private readonly object _resetLock = new object();
        private readonly object _createCounterLock = new object();

        private readonly IDictionary<MonitorKey, Counter> _monitors = new Dictionary<MonitorKey, Counter>();

        #region Implement IMonitoring

        public StopWatch Start(string @group, string monitorName, IDictionary<string, string> properties)
        {
            return Create(new MonitorKey(BuildMonitorName(group, monitorName, MonblankConst.Ms), properties)).Start();
        }

        public StopWatch Start(string @group, string monitorName)
        {
            return Create(new MonitorKey(BuildMonitorName(group, monitorName, MonblankConst.Ms))).Start();
        }

        public StopWatch Start(MonitorKey key)
        {
            return Create(key).Start();
        }

        public void Add(string @group, string monitorName, string units, double value,
            IDictionary<string, string> properties)
        {
            UpdateCounter(new MonitorKey(BuildMonitorName(group, monitorName, units), properties), value);
        }

        public void Add(string @group, string monitorName, string units, double value)
        {
            UpdateCounter(new MonitorKey(BuildMonitorName(group, monitorName, units)), value);
        }

        public void Add(MonitorKey key, double value)
        {
            UpdateCounter(key, value);
        }

        public void Set(string @group, string monitorName, string units, double value, IDictionary<string, string> properties)
        {
            SetCounter(new MonitorKey(BuildMonitorName(group, monitorName, units), properties), value);
        }

        public void Set(string @group, string monitorName, string units, double value)
        {
            SetCounter(new MonitorKey(BuildMonitorName(group, monitorName, units)), value);
        }

        public void Set(MonitorKey key, double value)
        {
            SetCounter(key, value);
        }

        public void Inc(string @group, string monitorName, IDictionary<string, string> properties)
        {
            UpdateCounter(new MonitorKey(BuildMonitorName(group, monitorName, MonblankConst.Count), properties), 1.0);
        }

        public void Inc(string @group, string monitorName)
        {
            UpdateCounter(new MonitorKey(BuildMonitorName(group, monitorName, MonblankConst.Count)), 1.0);
        }

        public void Inc(MonitorKey key)
        {
            UpdateCounter(key, 1.0);
        }

        public List<Counter> Reset()
        {
            List<Counter> counters;
            lock (_resetLock)
            {
                counters = new List<Counter>(_monitors.Values);
                _monitors.Clear();
            }

            return counters;
        }

        #endregion

        #region Private Methods

        private StopWatch Create(MonitorKey key)
        {
            return new PersistenceStopWatch(this, key);
        }

        private void UpdateCounter(MonitorKey key, double value)
        {
            Counter counter = GetOrCreateCounter(key);
            counter.Update(value);
        }

        private void SetCounter(MonitorKey key, double value)
        {
            Counter counter = GetOrCreateCounter(key);
            counter.Set(value);
        }

        //Try to get counter without lock
        private Counter GetOrCreateCounter(MonitorKey key)
        {
            Counter counter;
            if (!_monitors.TryGetValue(key, out counter))
            {
                counter = SafeCreateCounter(key);
            }

            return counter;
        }

        private Counter SafeCreateCounter(MonitorKey key)
        {
            Counter counter;
            lock (_createCounterLock)
            {
                if (!_monitors.TryGetValue(key, out counter))
                {
                    counter = new Counter(key);
                    _monitors.Add(key, counter);
                }
            }

            return counter;
        }

        private String BuildMonitorName(String group, String monitorName, String units)
        {
            return group + MonblankConst.EventDelimiter + monitorName + WrapIfSet(units);
        }

        private static String WrapIfSet(String units)
        {
            if (string.IsNullOrEmpty(units))
            {
                return "";
            }
            return MonblankConst.UnitDelimiter + "(" + units + ")";
        }

        #endregion
    }
}