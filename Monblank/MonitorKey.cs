namespace Monblank
{
    #region using directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class MonitorKey
    {
        public String Name { get; protected set; }
        public Dictionary<String, String> Properties { get; protected set; }

        private readonly int _precalcHash;

        public MonitorKey(String name) : this(name, null)
        {
        }

        public MonitorKey(String name, Dictionary<String, String> properties)
        {
            Name = name;
            Properties = properties ?? new Dictionary<String, String>();
            _precalcHash = PrecalcHash();
        }

        public bool TryGet(String name, out string value)
        {
            return Properties.TryGetValue(name, out value);
        }

        private int PrecalcHash()
        {
            int result = Name.GetHashCode();
            return 31*result + Properties.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            MonitorKey that = obj as MonitorKey;
            if (ReferenceEquals(that, null)) return false;

            if (!Name.Equals(that.Name)) return false;
            if (!Properties.Equals(that.Properties)) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return _precalcHash;
        }
    }
}