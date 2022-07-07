using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 1f;
    [SerializeField] private float jumpHeight = 1f;

    [SerializeField] private LayerMask ground;

    private Rigidbody2D rb;
    private Collider2D coll;

    private bool facingLeft = true;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update(){
        Move(); 
    }
    
    private void Move(){
        if (facingLeft){
            if (transform.position.x > leftCap){
                if (transform.localScale.x != 1){
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground)){
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                }
            } else{
                facingLeft = false;
            }
        } else{
            if (transform.position.x < rightCap){
                if (transform.localScale.x != -1){
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground)){
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                }
            } else{
                facingLeft = true;
            }
        }
    }
}
