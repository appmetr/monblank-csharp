namespace Monblank
{
    #region using directives

    using System;

    #endregion

    public enum StopWatchStatus
    {
        NOT_STARTED = 0,
        RUNNING = 1,
        PAUSED = 2,
        STOPPED = 10
    }

    /// <summary>
    /// Note. This class is NOT thread-safe!
    /// However, most likely you will be using it as local scope variable in methods.
    /// </summary>
    public class StopWatch
    {
        /// <summary>
        /// <para>If stopwatch is <see cref="StopWatchStatus.RUNNING"/> - returns time elapsed from start to current moment</para>
        /// <para>If stopwatch was <see cref="StopWatchStatus.PAUSED"/> or <see cref="StopWatchStatus.STOPPED"/> - measured time from start until was paused or stopped.</para>
        /// </summary>
        public long ElapsedTime
        {
            get
            {
                if (State == StopWatchStatus.RUNNING)
                {
                    return Utils.GetTimestampInMilliseconds() - StartTime;
                }
                return _elapsedTime;
            }
            protected set { _elapsedTime = value; }
        }

        private long _elapsedTime;

        public long StartTime { get; protected set; }

        public long LapTime { get; protected set; }
        public long LapStartTime { get; protected set; }

        public StopWatchStatus State { get; protected set; }


        public StopWatch()
        {
            State = StopWatchStatus.NOT_STARTED;
        }

        /// <summary>
        /// Starts timer if it wasn't started yet.
        /// </summary>
        /// <returns>Started timer.</returns>
        public StopWatch Start()
        {
            if (State == StopWatchStatus.NOT_STARTED)
            {
                State = StopWatchStatus.RUNNING;
                this.StartTime = Utils.GetTimestampInMilliseconds();
                this._elapsedTime = 0L;
            }
            return this;
        }

        /// <summary>
        /// Reset's all timer's counters.
        /// </summary>
        /// <returns>Current timer.</returns>
        public StopWatch Reset()
        {
            _elapsedTime = 0;
            LapTime = 0;
            LapStartTime = 0;
            StartTime = 0;
            State = StopWatchStatus.NOT_STARTED;
            return this;
        }

        /// <summary>
        /// Resumes paused timer and lap time. Does nothing if wasn't paused.
        /// </summary>
        /// <returns>Elapsed time at pause moment.</returns>
        public void Resume()
        {
            if (State == StopWatchStatus.PAUSED)
            {
                StartTime = LapStartTime = Utils.GetTimestampInMilliseconds();
                State = StopWatchStatus.RUNNING;
            }
        }

        /// <summary>
        /// Pause whole timer including lap time. Does nothing if wasn't running.
        /// </summary>
        /// <returns>Elapsed time at pause moment.</returns>
        public long Pause()
        {
            if (State == StopWatchStatus.RUNNING)
            {
                long currentTimestamp = Utils.GetTimestampInMilliseconds();
                _elapsedTime += currentTimestamp - StartTime;
                if (LapStartTime > 0)
                {
                    LapTime += currentTimestamp - LapStartTime;
                }
                State = StopWatchStatus.PAUSED;
            }
            return _elapsedTime;
        }

        /// <summary>
        /// Starts new lap timer, main timer not affected.
        /// </summary>
        /// <returns>Last lap time. If no lap were measured - returns 0.</returns>
        public long Lap()
        {
            long currentMillis = Utils.GetTimestampInMilliseconds();

            LapTime += LapStartTime > 0 ? currentMillis - LapStartTime : 0;
            long existedLap = LapTime;
            LapTime = 0;

            LapStartTime = currentMillis;

            return existedLap;
        }

        /// <summary>
        /// Stops timer, cannot be resumed\started after.
        /// </summary>
        /// <returns>Elapsed time</returns>
        public long Stop()
        {
            if (State != StopWatchStatus.STOPPED)
            {
                long elapsedTime = Pause();
                State = StopWatchStatus.STOPPED;
                return elapsedTime;
            }
            return _elapsedTime;
        }

        public override string ToString()
        {
            long eTime = ElapsedTime;

            return String.Format("StopWatch elapsedTime = {0:D2} m {1:D2} s {2:D3} ms",
                eTime/(60*1000),
                (eTime%(1000*60))/(1000),
                (eTime%(1000*60))%1000);
        }

        /// <summary>
        /// Thus you must use it by calling <see cref="Resume()"/> method, not <see cref="Start()"/>.
        /// </summary>
        /// <returns>StopWatch object in paused state.</returns>
        public static StopWatch Paused()
        {
            StopWatch stopWatch = new StopWatch();
            stopWatch.State = StopWatchStatus.PAUSED;
            return stopWatch;
        }

        /// <returns>Started StopWatch object. Repeated call of <see cref="Start()"/> wouldn't have any effects.</returns>
        public static StopWatch Started()
        {
            return new StopWatch().Start();
        }
    }
}