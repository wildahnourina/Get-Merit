using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startingPoint;
    public Vector3 endingPoint;

    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startingPoint, endingPoint, Mathf.PingPong(Time.time * speed, 1.1f));
    }
    // private void OnCollisionEnter(Collision other) {
    //     other.transform.SetParent(transform);
    // }
    // private void OnCollisionExit(Collision other) {
    //     // other.trans
    // }
}
