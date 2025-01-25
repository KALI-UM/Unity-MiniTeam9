using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public static class KALLogger
{
    private static readonly string methodLogFormat = "[{0}.{1}]: {2}";
    private static readonly string objectLogFormat = "[{0}(Id: {1}).{2}]: {3}";
    private static readonly string valueFormat = "{0}={1}";
    private static readonly string unknownClassName = "UnknownClass";

    private static bool fileWrite;
    private static int fileWriteCount = 5;

    //public static Action<object, string, string> loglog;

    private static Queue<string> logs;
    private static Queue<string> Logs
    {
        get => logs;
        set
        {
            logs = value;
            if (logs.Count >= fileWriteCount)
            {
                LogFileWrite();
            }
        }
    }

    //private static string logFilePath = Path.Combine(Application.persistentDataPath, "kallog.txt");
    //private static readonly string fileLogFormat = "[{0:yyyy-MM-dd HH:mm:ss} {1}.{2}]: {3}";

    static KALLogger()
    {
        if (fileWrite)
        {
            //loglog = Log;
        }
    }

    private static void LogFileWrite()
    {

    }

    public static class ValueLogger
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log(string str)
        {
            Debug.Log(str);
        }
    }


    //에디터 상에서만 동작
    [System.Diagnostics.Conditional("UNITY_EDITOR")]

    public static void Log(string message, object obj,
        [CallerMemberName] string memberName = null,
        [CallerFilePath] string callerFilePath = null)
    {
        string className = unknownClassName;
        if (callerFilePath != null)
        {
            className = GetClassNameByFileName(callerFilePath);
        }

        string logMessage = string.Format(objectLogFormat, className, obj?.GetHashCode() ?? -1, memberName, message);
        UnityEngine.Debug.Log(logMessage);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object message,
        [CallerMemberName] string memberName = null,
        [CallerFilePath] string callerFilePath = null)
    {
        string className = unknownClassName;
        if (callerFilePath != null)
        {
            className = GetClassNameByFileName(callerFilePath);
        }

        string logMessage = string.Format(methodLogFormat, className, memberName, message);
        UnityEngine.Debug.Log(logMessage);
    }

    public static string GetValueFormatString<T>(string valueName, T value)
    {
        foreach (var field in valueName.GetType().GetFields())
        {
            if (field.GetValue(null).GetHashCode() == value.GetHashCode())
            {
                string name = field.Name;
            }
        }
        return string.Format(valueFormat, valueName, value);
    }

    private static string GetClassNameByFileName(string callerFilePath)
    {
        var fileName = System.IO.Path.GetFileNameWithoutExtension(callerFilePath);
        return fileName;
    }
}
