using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    [SerializeField] private Vector3 checkpos;
    [SerializeField] private bool checkin;


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("checkpoint")){
            Debug.Log("Masukcheck");
            checkpos = other.transform.position;
            checkin = true;
        }
    }
}
