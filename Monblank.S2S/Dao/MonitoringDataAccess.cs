namespace Monblank.S2S.Dao
{
    #region using directives

    using System;
    using System.Threading;
    using AppmetrS2S;
    using log4net;

    #endregion

    public class MonitoringDataAccess
    {
        private static readonly ILog Log = LogUtils.GetLogger(typeof (MonitoringDataAccess));

        private readonly AppMetr _appMetr;
        private readonly AppMetrTimer _flushTimer;
        private readonly IMonitoring _monitoring;
        private readonly MonitoringCounterService _monitoringCounterService;

        private readonly object _flushLock = new object();
        private readonly Thread _flushThread;

        private const int MillisPerMinute = 1000*60;

        public MonitoringDataAccess(IMonitoring monitoring, AppMetr appMetr)
        {
            _monitoring = monitoring;
            _appMetr = appMetr;
            _monitoringCounterService = new MonitoringCounterService(appMetr);

            _flushTimer = new AppMetrTimer(
                MonblankConst.MonitorFlushIntervalMinutes*MillisPerMinute,
                Execute,
                "MonitorFlushJob");
            _flushThread = new Thread(_flushTimer.Start);
            _flushThread.Start();
        }

        public void Execute()
        {
            lock (_flushLock)
            {
                var startMillis = Utils.GetNowUnixTimestamp();
                try
                {
                    //shift timestamp backward, 'cause we need to store events "in past"
                    var runTime = Utils.GetNowUnixTimestamp() -
                                  (1*MonblankConst.MonitorFlushIntervalMinutes*MillisPerMinute);

                    SaveAndReset(runTime);
                }
                catch (Exception e)
                {
                    Log.Error("Exception while persisting monitors", e);
                }
                finally
                {
                    var persistEnd = Utils.GetNowUnixTimestamp();

                    if (Log.IsInfoEnabled)
                    {
                        Log.Info("Monitor scheduler persist millis took: " + (persistEnd - startMillis));
                    }
                }
            }
        }

        public void SaveAndReset(long time)
        {
            var swMethod = new StopWatch().Start();

            var monitors = _monitoring.Reset();
            _monitoringCounterService.PersistMonitors(monitors, time);

            swMethod.Stop();

            if (Log.IsInfoEnabled)
            {
                Log.InfoFormat("Getting active monitors. Method execution time {0}", swMethod);
            }
        }

        public void Stop()
        {
            lock (_flushLock)
            {
                _flushTimer.Stop();
                _appMetr.Stop();
            }
        }
    }
}