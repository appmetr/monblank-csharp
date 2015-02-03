namespace Monblank
{
    #region using directives

    using System;

    #endregion

    public class Utils
    {
        public static long GetNowUnixTimestamp()
        {
            return (long) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }
    }
}