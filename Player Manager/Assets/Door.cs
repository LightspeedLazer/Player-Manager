using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 ClosedPosition;
    public Vector3 OpenPosition;
    public int status;

    void Update()
    {
        if (status == 1)
        {
            transform.position = OpenPosition;
        }
        else
        {
            transform.position = ClosedPosition;
        }
    }

    public void ChangeStatus(int status)
    {
        this.status = status;
    }
}
