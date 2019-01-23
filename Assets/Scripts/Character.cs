using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed; //ustawiana w Unity zmienna szybkości

    private Animator animator; //obiekt animacji

    protected Vector2 direction; //obiekt kierunek ruchu


    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>(); //referencja komponentu animacji
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move(); //wywołanie funkcji ruchu + animacji
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime); //rozpoczęcie ruchu postaci

        AnimateMovement(direction); //rozpoczęcie animacji postaci
    }

    public void AnimateMovement(Vector2 direction) //animacja postaci playera
    {
        animator.SetFloat("x", direction.x); //parametr x stworzony w animatorze
        animator.SetFloat("y", direction.y); //parametr y stworzony w animatorze
    }
}
