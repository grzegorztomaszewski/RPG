﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed; //ustawiana w Unity zmienna szybkości

    protected Animator myAnimator; //obiekt animacji
    protected Vector2 direction; //obiekt kierunek ruchu
    private Rigidbody2D myRigidbody;
    protected bool isAttacking;
    protected Coroutine attackRoutine;


    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>(); //referencja komponentu animacji
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        myRigidbody.velocity = direction.normalized * speed;
    }

    public void HandleLayers()
    {
        if(IsMoving) //jeśli się ruszamy, aktywna jest ta warstwa
        {
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x); //parametr x stworzony w animatorze
            myAnimator.SetFloat("y", direction.y); //parametr y stworzony w animatorze

            StopAttack();
        }
        else if (isAttacking) //jeśli atakujemy, aktywuje się ta warstwa
        {
            ActivateLayer("AttackLayer");
        }
        else //jeśli nic nie robimy, aktywuje się ta warstwa
        {
            ActivateLayer("IdleLayer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);

        if (attackRoutine != null)
        {
        StopCoroutine(attackRoutine);
        }
    }
}
