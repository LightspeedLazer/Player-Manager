using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Console : MonoBehaviour
{
    public TMP_InputField commandInput;
    public TMP_Text logDisplay;
    [HideInInspector]
    public TextLog consoleLog = new TextLog();
    public UnityEvent runCommand = new UnityEvent();
    public string command;
    public Dictionary<KeyCode,List<string>> binds = new Dictionary<KeyCode, List<string>>();

    void Start()
    {
        commandInput.onEndEdit.AddListener(delegate {EnterCommand(commandInput.text); commandInput.text = "";});
    }

    void Update()
    {
        logDisplay.text = consoleLog.ToString();
        foreach (KeyCode key in binds.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                foreach (string Command in binds[key])
                {
                    EnterCommand(Command);
                }
            }
        }
    }

    public void EnterCommand(string input)
    {
        consoleLog.Log(new LogEntry("] " + input));
        string[] arguments = input.Split(' ');
        switch (arguments[0])
        {
            case "clear":
                consoleLog.clear();
            break;
            case "bind":
                bind(arguments);
            break;
            default:
                command = input;
                runCommand.Invoke();
                command = null;
            break;
        }
    }

    void bind(string[] arguments)   //(KeyCode)System.Enum.Parse(typeof(KeyCode), stringCode)
    {
        KeyCode key;
        if (arguments[1] == "null") {key = KeyCode.None;}
        else
        {
            string stringCode = arguments[1];
            stringCode = stringCode[0].ToString().ToUpper() + stringCode.Substring(1);
            key = (KeyCode)System.Enum.Parse(typeof(KeyCode), stringCode);
        }
        switch (arguments[2])
        {
            case "+":
                if (key != KeyCode.None)
                {
                    bool contains = false;
                    foreach (KeyCode index in binds.Keys)
                    {
                        if (index == key)
                        {
                            contains = true;
                        }
                    }
                    if (!contains)
                    {
                        binds.Add(key, new List<string>());
                    }
                    binds[key].Add(arguments[3]);
                }
                else {consoleLog.Log(new LogEntry("Cannot add a bind with a null key"));}
            break;
            case "-":
                if (key != KeyCode.None)
                {
                    binds.Remove(key);
                }
                else
                {
                    if (arguments[3] != null)
                    {
                        foreach (KeyCode index in binds.Keys)
                        {
                            int i = 0;
                            foreach (string Command in binds[index])
                            {
                                if (Command == arguments[3])
                                {
                                    binds[index].RemoveAt(i);
                                }
                                i++;
                            }
                        }
                    }
                    else
                    {
                        binds.Clear();
                    }
                }
            break;
        }
    }

    public void Log(LogEntry entry) {consoleLog.Log(entry);}
}
