using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform ClosedTransform;
    public Transform OpenTransform;
    public Transform door;
    public int status;

    void Update()
    {
        if(status == 1)
        {
            door.position = OpenTransform.position;
        }
        else
        {
            door.position = ClosedTransform.position;
        }
    }

    public void ChangeStatus(int status)
    {
        this.status = status;
    }

    void OnDrawGizmos()
    {
        if(ClosedTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(ClosedTransform.position,ClosedTransform.localScale);
        }
        if(OpenTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(OpenTransform.position,OpenTransform.localScale);
        }
    }
}
