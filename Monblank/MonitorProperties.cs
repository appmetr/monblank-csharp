namespace Monblank
{
    #region using directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class MonitorProperties
    {
        private IDictionary<String, String> _properties = new Dictionary<string, string>();

        public static MonitorProperties Create()
        {
            return new MonitorProperties();
        }

        public static MonitorProperties Create(String name, String value)
        {
            return Create().Add(name, value);
        }

        public static MonitorProperties Create(IDictionary<String, String> propertires)
        {
            var monitorProperties = new MonitorProperties {_properties = new Dictionary<String, String>(propertires)};

            return monitorProperties;
        }

        public MonitorProperties Add(String name, String value)
        {
            _properties.Add(name, value);

            return this;
        }

        public IDictionary<String, String> AsDictionary()
        {
            return _properties;
        }
    }
}