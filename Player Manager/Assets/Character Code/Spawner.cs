using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject SpawnObject;
    public Transform SpawnOffset;
    public GameObject clone {get; private set;}
    public float RespawnInterval = 10;
    private float timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (timer <= 0)
        {
            clone = Instantiate(SpawnObject, Vector3.zero, Quaternion.identity, transform);
            clone.transform.position = SpawnOffset.position;
            clone.transform.rotation = SpawnOffset.rotation;
            timer = RespawnInterval;
        }
        else if (clone == null)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        }
    }
}
