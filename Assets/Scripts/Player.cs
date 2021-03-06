﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character //dziedziczenie po klasie character
{

    [SerializeField]
    private Stat mana;  //stworzenie w playerze obiektu, który jest już działającym paskiem many w skrypcie Stat

    private float initMana = 50; //ustawienie na sztywno MANA=50


    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform[] exitPoints; //zmienna ustawiająca pozycje pocisku

    private int exitIndex = 2; //index(liczba), która przypisana jest do danej pozycji exitPoints (Element 0, Element 1)

    private SpellBook spellBook; //zmienna referencyjna do skryptu "SpellBook", który podpięty jest pod playera w unity

    private Vector3 min, max;

    public Transform MyTarget { get; set; }

    protected override void Start()                                                             //START
    {
        spellBook = GetComponent<SpellBook>();
        mana.Initialize(initMana, initMana);       //zainicjowanie currentValue i MaxValue jako initHealth, które obie wartości ustawia na 50

        base.Start();
    }

    protected override void Update()                                                            //UPDATE
    {
        GetInput(); //wywołanie metody pobierania wartości z klawiatury (ruch)

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x),
            Mathf.Clamp(transform.position.y, min.y, max.y),
            transform.position.z);

        base.Update();
    }


    private void GetInput() //pobieranie wartości z klawiatury (ruch postaci)
    {
        ////TEST na działanie paska HP/MANA
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }

        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;  //wybiera Element 0 z pośród exit points (do ustawiania pozycji spella)
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }
    }

    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = spellBook.CastSpell(spellIndex);

        isAttacking = true;

        myAnimator.SetBool("attack", isAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        if(currentTarget != null && InLineOfSight())
        { 
           SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();  //fireball
           s.Initialize(currentTarget, newSpell.MyDamage);
        }

        StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        if (!isAttacking && !IsMoving && InLineOfSight())
        {
            Block();

            attackRoutine = StartCoroutine(Attack(spellIndex));
        }
    }

    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
        Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position),256);

        if(hit.collider == null)
            {
            return true;
            }
        }
        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }
        blocks[exitIndex].Activate();
    }

    public override void StopAttack()
    {
        spellBook.StopCasting();

        base.StopAttack();
    }
}
