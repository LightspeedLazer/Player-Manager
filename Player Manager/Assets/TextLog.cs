using UnityEngine;
using System.Collections.Generic;

public class TextLog
{
    public List<LogEntry> LogList = new List<LogEntry>();
    public string rawText {get; private set;}

    public void Log(LogEntry entry)
    {
        LogList.Add(entry);
    }

    public List<LogEntry> clear()
    {
        List<LogEntry> r = LogList;
        LogList.Clear();
        return r;
    }

    public override string ToString()
    {
        rawText = null;
        int i = 0;
        foreach (LogEntry logEntry in LogList)
        {
            if (i>0) {rawText += "\n";}
            rawText += logEntry.text;
            i++;
        }
        return rawText;
    }
}

public class LogEntry
{
    public string text;
    public float logTime;

    public LogEntry(string entry)
    {
        text = entry;
        logTime = Time.time;
    }

    public override string ToString() {return logTime.ToString() + " " + text;}
}
