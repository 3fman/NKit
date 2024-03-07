using System;
using System.Threading;

namespace NKit.CSharp
{
    public class TimerX : IDisposable
    {
        private TimeSpan _periodTime;

        private readonly System.Threading.Timer _timer;

        public TimerX(TimerCallback callback)
        {
            _timer = new Timer((state) =>
            {
                callback?.Invoke(state);
                _timer?.Change(_periodTime, Timeout.InfiniteTimeSpan);
            });
        }

        public void Change(TimeSpan period)
        {
            if (period == Timeout.InfiniteTimeSpan)
            {
                WaitForDispose(_timer, TimeSpan.MaxValue);
            }
            else
            {
                _periodTime = period;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private bool WaitForDispose(Timer timer, TimeSpan timeout)
        {
            timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan); // 暂停派送新任务
            if (timeout != TimeSpan.Zero)
            {
                ManualResetEvent waitHandle = new ManualResetEvent(false);
                if (timer.Dispose(waitHandle) && !waitHandle.WaitOne((int)timeout.TotalMilliseconds))
                {
                    return false;
                }
                waitHandle.Close();
            }
            else
            {
                timer.Dispose();
            }
            return true;
        }
    }
}