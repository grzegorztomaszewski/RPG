using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed; //ustawiana w Unity zmienna szybkości

    protected Animator myAnimator; //obiekt animacji
    protected Vector2 direction; //obiekt kierunek ruchu
    private Rigidbody2D myRigidbody;
    protected bool isAttacking;
    protected Coroutine attackRoutine;
    [SerializeField]

    protected Transform hitBox;

    [SerializeField]
    protected Stat health;

    [SerializeField]
    private float initHealth = 100; //ustawienie na sztywno HP=100

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
        health.Initialize(initHealth, initHealth);


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

    public virtual void StopAttack()
    {
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);

        if (attackRoutine != null)
        {
        StopCoroutine(attackRoutine);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            myAnimator.SetTrigger("die");
        }
    }
}
