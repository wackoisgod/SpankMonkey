using Common.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityLogTarget : LogTarget
{
    public UnityLogTarget(Common.Logging.Logger.Level minLevel, Common.Logging.Logger.Level maxLevel, bool includeTimeStamps)
    {
        MinimumLevel = minLevel;
        MaximumLevel = maxLevel;
        IncludeTimeStamps = includeTimeStamps;
    }

    public void Log(string message)
    {
        LogMessage(Common.Logging.Logger.Level.Debug, "Default", message);
    }

    public override void LogMessage(Common.Logging.Logger.Level level, string logger, string message)
    {
        if (true)
        {
            string timeStamp = IncludeTimeStamps ? "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") + "] " : "";
            string logMessage = $"{timeStamp}[{level.ToString().PadLeft(5)}] [{logger}]: {message}";

            switch (level)
            {
                case Common.Logging.Logger.Level.Debug:
                case Common.Logging.Logger.Level.Trace:
                case Common.Logging.Logger.Level.Info:
                    UnityEngine.Debug.Log(logMessage);
                    break;
                case Common.Logging.Logger.Level.Warn:
                    UnityEngine.Debug.LogWarning(logMessage);
                    break;
                case Common.Logging.Logger.Level.Error:
                case Common.Logging.Logger.Level.Fatal:
                    UnityEngine.Debug.LogError(logMessage);
                    break;
            }
        }
    }
}
