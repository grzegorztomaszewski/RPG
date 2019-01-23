﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float speed;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        //debug & testing
        target = GameObject.Find("Target").transform;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = target.position - transform.position;

        myRigidbody.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //obrót pocisku w stosunku do przeciwnika

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);  //obrót pocisku w stosunku do przeciwnika
    }
}
