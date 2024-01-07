using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float speed;


    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -1f);
        transform.position = Vector3.Slerp(transform.position, newPos, speed* Time.deltaTime);
    }
}
