namespace Monblank
{
    using System;

    internal class Utils
    {
        public const long TicksPerMillisecond = 10000;

        public static long GetTimestampInMilliseconds()
        {
            return DateTime.UtcNow.Ticks/TicksPerMillisecond;
        }   
    }
}