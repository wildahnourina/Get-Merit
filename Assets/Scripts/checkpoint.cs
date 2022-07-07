using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    [SerializeField] private bool checkpointSteppedOn = false;
    [SerializeField] private Vector3 checkpointPos;
    [SerializeField] private playerRespawn check;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            checkpointSteppedOn = true;
            checkpointPos = transform.position;
        }
    }
}
