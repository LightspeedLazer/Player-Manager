using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public bool pressed;
    private bool prevpressed;
    public UnityEvent OnPress = new UnityEvent();
    public UnityEvent OnRelease = new UnityEvent();

    void Update()
    {
        if (pressed != prevpressed)
        {
            if (pressed)
            {
                OnPress.Invoke();
            }
            else
            {
                OnRelease.Invoke();
            }
        }
        prevpressed = pressed;
    }
}
