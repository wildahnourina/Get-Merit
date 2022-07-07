using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    public float speed;
    //public float stoppingDistance;
    //private bool facingLeft = true;
    private Transform target;
    private Vector2 currentPos;
    [SerializeField] AudioSource bird;

    protected override void Start(){
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
    }

    private void Update(){
        if (Vector2.Distance(transform.position, target.position) > 3 && Vector2.Distance(transform.position, target.position) < 4){
            anim.SetBool("attacking", false);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (target.gameObject.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1,1);
            }else
            {
                transform.localScale = new Vector2(1,1);
            }
        }
        else if(Vector2.Distance(transform.position, target.position) > 4){
            // transform.position = Vector2.MoveTowards(transform.position, -target.position, speed * Time.deltaTime);
        }
        else {
            bird.Play();
            anim.SetBool("attacking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        // } else  else{
        //     transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);
        // }
        
    }
}

