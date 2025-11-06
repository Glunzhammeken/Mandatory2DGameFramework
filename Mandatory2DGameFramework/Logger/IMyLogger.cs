using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Mandatory2DGameFramework.Logger
{
    public interface IMyLogger
    {
        void AddListener(TraceListener listener);
        void RemoveListener(TraceListener listener);
        void SetDefaultLevel(SourceLevels level);
        void SetDebugLogging();
        void RemoveDebugLogging();
        void Stop();
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogCritical(string message);
    }
}