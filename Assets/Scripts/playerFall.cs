using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFall : MonoBehaviour
{
    // Start is called before the first frame update    
    [SerializeField]public Vector3 respawnPoint;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }
        else if (other.gameObject.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
    }
}
