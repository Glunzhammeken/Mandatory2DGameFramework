using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.Logger
{
    public class MyLogger : IMyLogger
    {
        private static MyLogger instance = new MyLogger();
        public static MyLogger Instance { get { return instance; } }
        private MyLogger()
        {
            _ts = new TraceSource("GameLog", SourceLevels.All);
            _ts.Switch = new SourceSwitch("Gamelog", SourceLevels.All.ToString());
        }


        private TraceSource _ts;
        private TraceListener? _debugListener = null;


        
        public void AddListener(TraceListener listener)
        {
            _ts.Listeners.Add(listener);
        }
        public void RemoveListener(TraceListener listener)
        {
            _ts.Listeners.Remove(listener);
        }
        public void SetDefaultLevel(SourceLevels level)
        {
            _ts.Switch.Level = level;
        }

        public void SetDebugLogging()
        {
            if (_debugListener == null)
            {
                _debugListener = new ConsoleTraceListener();
                _ts.Listeners.Add(_debugListener);
            }

        }
        public void RemoveDebugLogging()
        {
            if (_debugListener != null)
            {
                _ts.Listeners.Remove(_debugListener);
                _debugListener = null;
            }
        }

        public void Stop()
        {
            _ts.Close();
        }

        /*
         * logging
         */
        public void LogInfo(string message)
        {
            _ts.TraceEvent(TraceEventType.Information, 2, message);
        }

        public void LogWarning(string message)
        {
            _ts.TraceEvent(TraceEventType.Warning, 2, message);
        }

        public void LogError(string message)
        {
            _ts.TraceEvent(TraceEventType.Error, 2, message);
        }

        public void LogCritical(string message)
        {
            _ts.TraceEvent(TraceEventType.Critical, 2, message);
        }

    }
}
