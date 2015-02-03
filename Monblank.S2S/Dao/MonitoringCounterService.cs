namespace Monblank.S2S.Dao
{
    #region using directives

    using System;
    using System.Collections.Generic;
    using AppmetrS2S;
    using AppmetrS2S.Actions;
    using log4net;

    #endregion

    public class MonitoringCounterService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MonitoringDataAccess));

        private readonly AppMetr _appMetr;

        public MonitoringCounterService(AppMetr appMetr)
        {
            _appMetr = appMetr;
        }

        public void PersistMonitors(List<Counter> activeMonitors, long timestamp)
        {
            foreach (var monitor in activeMonitors)
            {
                try
                {
                    lock (monitor)
                    {
                        _appMetr.Track(ConvertMonitorToEvent(monitor, timestamp));
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Unexpected exception while persisting monitors.", e);
                }
            }

            if (Log.IsInfoEnabled)
            {
                Log.InfoFormat("Persisted monitors: {0}", activeMonitors.Count);
            }
        }

        private static AppMetrAction ConvertMonitorToEvent(Counter counter, long timestamp)
        {
            var monitorKey = counter.Key;
            IDictionary<String, Object> properties = new Dictionary<String, Object>();
            foreach (var property in monitorKey.Properties)
            {
                properties.Add(property.Key, property.Value);
            }

            properties.Add(MonblankConst.HitsFeature, counter.Hits);
            properties.Add(MonblankConst.TotalFeature, counter.Total);
            properties.Add(MonblankConst.SquaresSumFeatures, counter.SumOfSquares);
            properties.Add(MonblankConst.MinFeature, counter.Min);
            properties.Add(MonblankConst.MaxFeature, counter.Max);

            return new Event(monitorKey.Name).SetTimestamp(timestamp).SetProperties(properties);
        } 
    }
}