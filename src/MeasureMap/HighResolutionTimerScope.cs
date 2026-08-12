using System;
using System.Runtime.InteropServices;

namespace MeasureMap
{
    /// <summary>
    /// Raises the Windows system timer resolution for the duration of the scope.
    /// On Windows the default timer resolution is ~15.6ms which causes short
    /// <see cref="System.Threading.Thread.Sleep(int)"/> and timer based waits to be
    /// rounded up. This mostly affects the first benchmark in a run, making it appear
    /// slower than subsequent ones. Increasing the resolution to 1ms removes that skew.
    /// On non Windows platforms this scope is a no-op.
    /// </summary>
    internal sealed class HighResolutionTimerScope : IDisposable
    {
        private const uint TargetPeriod = 1;

        private readonly bool _applied;

        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        private static extern uint TimeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        private static extern uint TimeEndPeriod(uint uMilliseconds);

        public HighResolutionTimerScope()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return;
            }

            try
            {
                _applied = TimeBeginPeriod(TargetPeriod) == 0;
            }
            catch (DllNotFoundException)
            {
                _applied = false;
            }
            catch (EntryPointNotFoundException)
            {
                _applied = false;
            }
        }

        public void Dispose()
        {
            if (!_applied)
            {
                return;
            }

            try
            {
                TimeEndPeriod(TargetPeriod);
            }
            catch (DllNotFoundException)
            {
            }
            catch (EntryPointNotFoundException)
            {
            }
        }
    }
}
