using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : Enemy
{
    public float speed;
    public float stoppingDistance;
    private Transform target;

    protected override void Start(){
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update(){
        if (Vector2.Distance(transform.position, target.position) > 3){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void Move(){
        
    }
}

