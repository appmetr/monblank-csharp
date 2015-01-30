namespace Monblank
{
    #region using directives

    using System;
    using System.Collections.Generic;

    #endregion

    public interface IMonitoring
    {

        /// <summary>
        /// Creates and starts monitor and returns it instance
        /// </summary>
        /// <param name="group">group name</param>
        /// <param name="monitorName">name of monitor.<para>Do not use character '@' in group or monitor name!</para></param>
        /// <param name="properties">monitor properties</param>
        /// <returns></returns>
        StopWatch Start(String group, String monitorName, Dictionary<String, String> properties);
        StopWatch Start(String group, String monitorName);
        StopWatch Start(MonitorKey key);

        /// <summary>
        /// Add value to monitor.
        /// </summary>
        /// <param name="group">group name</param>
        /// <param name="monitorName">name of monitor.<para>Do not use character '@' in group or monitor name!</para></param>
        /// <param name="units">measuring units (should be one of BYTES\COUNT\MS constant from MonitoringService)</param>
        /// <param name="value">value to add</param>
        /// <param name="properties">monitor properties</param>
        void Add(String group, String monitorName, String units, double value, Dictionary<String, String> properties);
        void Add(String group, String monitorName, String units, double value);
        void Add(MonitorKey key, double value);

        /// <summary>
        /// Increment monitor's value by 1
        /// </summary>
        /// <param name="group">group name</param>
        /// <param name="monitorName">name of monitor.<para>Do not use character '@' in group or monitor name!</para></param>
        /// <param name="properties">monitor properties</param>
        void Inc(String group, String monitorName, Dictionary<String, String> properties);
        void Inc(String group, String monitorName);
        void Inc(MonitorKey key);

        List<Counter> Reset();
    }
}