namespace Monblank
{
    #region using directives

    using System;
    using System.Collections.Generic;

    #endregion

    public class Counter
    {
        //copy of counter's key
        public MonitorKey Key { get; private set; }

        /** the total for all values */
        public double Total { get; protected set; }
        /** The minimum of all values */
        public double Min { get; protected set; }
        /** The maximum of all values */
        public double Max { get; protected set; }
        /** The total number of occurrences/calls to this object */
        public long Hits { get; protected set; }
        /** Intermediate value used to calculate std dev */
        public double SumOfSquares { get; protected set; }

        /** The first time this object was accessed */
        public long FirstAccess { get; protected set; }
        /** The last time this object was accessed */
        public long LastAccess { get; protected set; }

        public Counter(MonitorKey key)
        {
            Key = key;
            Reset();
        }

        public Counter(String name, Dictionary<String, String> properties) : this(new MonitorKey(name, properties))
        {
        }

        public Counter(String name) : this(new MonitorKey(name))
        {
        }

        public Counter Update(double value)
        {
            lock (this)
            {
                Total += value;
                Hits++;
                if (value < Min) Min = value;
                if (value > Max) Max = value;
                SumOfSquares += value*value;
                if (FirstAccess == 0) FirstAccess = Utils.GetNowUnixTimestamp();
                LastAccess = Utils.GetNowUnixTimestamp();
            }

            return this;
        }

        public void Reset()
        {
            lock (this)
            {
                Total = 0.0;
                Min = Double.MaxValue;
                Max = Double.MinValue;
                Hits = 0;
                SumOfSquares = 0.0;
                FirstAccess = 0;
                LastAccess = 0;
            }
        }

        public override string ToString()
        {
            return
                String.Format(
                    "Counter{{key={0}, total={1}, min={2}, max={3}, hits={4}, sumOfSquares={5}, firstAccess={6}, lastAccess={7}}}",
                    Key, Total, Min, Max, Hits, SumOfSquares, FirstAccess, LastAccess);
        }
    }
}