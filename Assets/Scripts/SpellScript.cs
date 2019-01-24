using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float speed;

    private Transform target;

    public Transform MyTarget { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
        Vector2 direction = MyTarget.position - transform.position;

        myRigidbody.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //obrót pocisku w stosunku do przeciwnika

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);  //obrót pocisku w stosunku do przeciwnika
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox" && collision.transform ==MyTarget)
        {
            GetComponent<Animator>().SetTrigger("impact");
            myRigidbody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}
