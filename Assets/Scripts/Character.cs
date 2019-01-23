using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed; //ustawiana w Unity zmienna szybkości

    private Animator myAnimator; //obiekt animacji

    protected Vector2 direction; //obiekt kierunek ruchu

    private Rigidbody2D myRigidbody;

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
        if(IsMoving)
        {
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x); //parametr x stworzony w animatorze
            myAnimator.SetFloat("y", direction.y); //parametr y stworzony w animatorze
        }
        else
        {
            ActivateLayer("IdleLayer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++ )
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }
}
