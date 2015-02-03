namespace Monblank.S2S.Bridge
{
    public class PersistenceStopWatch : StopWatch
    {
        private readonly IMonitoring _monitoring;
        private readonly MonitorKey _key;

        public PersistenceStopWatch(IMonitoring monitoring, MonitorKey key)
        {
            _monitoring = monitoring;
            _key = key;
        }

        public override long Stop()
        {
            long elapsedTime = base.Stop();
            _monitoring.Add(_key, elapsedTime);

            return elapsedTime;
        }
    }
}