using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed; //ustawiana w Unity zmienna szybkości

    private Animator animator; //obiekt animacji

    protected Vector2 direction; //obiekt kierunek ruchu

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); //referencja komponentu animacji
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
        myRigidbody.velocity = direction * speed;

        AnimateMovement(direction); //rozpoczęcie animacji postaci
    }

    public void HandleLayers()
    {
        if(direction.x != 0 || direction.y != 0)
        {
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    public void AnimateMovement(Vector2 direction) //animacja postaci playera
    {
        animator.SetFloat("x", direction.x); //parametr x stworzony w animatorze
        animator.SetFloat("y", direction.y); //parametr y stworzony w animatorze
    }
}
