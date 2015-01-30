namespace Monblank
{
    using System;

    public class MonblankConst
    {
        public static readonly int MonitorFlushIntervalMinutes = 1;
        public static readonly int MonitorResetIntervalMinutes = 1;

        public static readonly String MonitoringToken = "monitoring";
        public static readonly String EventDelimiter = "/";
        public static readonly String UnitDelimiter = " ";

        public static readonly String InstanceFeature = "$instance";
        public static readonly String TokenFeature = "$token";
        public static readonly String AvgFeature = "$avg";
        public static readonly String StddevFeature = "$stdDev";
        public static readonly String HitsFeature = "$hits";
        public static readonly String TotalFeature = "$total";
        public static readonly String MinFeature = "$min";
        public static readonly String MaxFeature = "$max";
        public static readonly String SquaresSumFeatures = "$sumOfSquares";


        public static readonly String Bytes = "bytes";
        public static readonly String Count = "count";
        public static readonly String Ms = "ms";

        public static readonly String Delimiter = "@";
        public static readonly String All = "any";

        public static readonly String Hits = "Hits";
        public static readonly String Avg = "Avg";
        public static readonly String Total = "Total";
        public static readonly String StdDev = "StdDev";
        public static readonly String LastValue = "LastValue";
        public static readonly String Min = "Min";
        public static readonly String Max = "Max";
        public static readonly String Active = "Active";
        public static readonly String AvgActive = "AvgActive";
        public static readonly String MaxActive = "MaxActive";
        public static readonly String SquaresSum = "sumOfSquares";

        public static readonly string[] Indicators = {Hits, Total, Min, Max};
    }
}