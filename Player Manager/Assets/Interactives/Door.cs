using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Door : MonoBehaviour
{
    public Transform door;
    public bool open;
    public bool invertDirection = false;
    public float openTime = 0.5f;
    public enum LockStates{Locked,Unlocked,InspectionBroken,VisiblyBroken}
    public LockStates lockStatus = LockStates.Locked;
    private float targetAngle;
    private float angle;

    void Update()
    {
        if(open)
        {
            if(invertDirection) {targetAngle = -90;}
            else {targetAngle = 90;}
        }
        else {targetAngle = 0;}
        angle += Mathf.Min(Mathf.Max(targetAngle-angle,-90/openTime*Time.deltaTime),90/openTime*Time.deltaTime);
        angle = Mathf.Min(Mathf.Max(angle,-90),90);
        door.localPosition = EvaluateAngle(angle);
        door.eulerAngles = new Vector3(0,-angle,0);
    }

    public void Open() {if(lockStatus != LockStates.Locked){open = true;}}
    public void Close() {open = false;}
    public void Lock() {if(lockStatus == LockStates.Unlocked){lockStatus = LockStates.Locked;}}
    public void Unlock() {lockStatus = LockStates.Locked;}
    public void InspectionBreak() {lockStatus = LockStates.InspectionBroken;}
    public void VisiblyBreak() {lockStatus = LockStates.VisiblyBroken;}

    Vector3 EvaluateAngle(float angle)
    {
        Vector3 r = new Vector3();
        r.x = (Mathf.Cos(angle*Mathf.Deg2Rad)*door.localScale.x-door.localScale.x)/2;
        r.z = (Mathf.Sin(angle*Mathf.Deg2Rad)*door.localScale.x)/2;
        return r;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,door.localScale);
        Gizmos.color = Color.green;
        if(invertDirection)
        {
            Gizmos.DrawWireCube(EvaluateAngle(-90)+transform.position,new Vector3(door.localScale.z,door.localScale.y,door.localScale.x));
        }
        else
        {
            Gizmos.DrawWireCube(EvaluateAngle(90)+transform.position,new Vector3(door.localScale.z,door.localScale.y,door.localScale.x));
        }
    }
}
