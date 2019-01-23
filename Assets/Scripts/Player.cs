using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character //dziedziczenie po klasie character
{
    [SerializeField]
    private Stat health; //stworzenie w playerze obiektu, który jest już działającym paskiem życia w skrypcie Stat

    [SerializeField]
    private Stat mana;  //stworzenie w playerze obiektu, który jest już działającym paskiem many w skrypcie Stat


    private float initHealth = 100; //ustawienie na sztywno HP=100

    private float initMana = 50; //ustawienie na sztywno MANA=50

    protected override void Start()
    {
        health.Initialize(initHealth, initHealth); //zainicjowanie currentValue i MaxValue jako initHealth, które obie wartości ustawia na 100
        mana.Initialize(initMana, initMana);       //zainicjowanie currentValue i MaxValue jako initHealth, które obie wartości ustawia na 50
        base.Start();
    }

    protected override void Update()
    {
        GetInput(); //wywołanie metody pobierania wartości z klawiatury (ruch)
        base.Update();
    }


    private void GetInput() //pobieranie wartości z klawiatury (ruch postaci)
    {
        ////TEST na działanie paska HP/MANA
        //if(Input.GetKeyDown(KeyCode.I))
        //{
        //    health.MyCurrentValue -= 10;
        //    mana.MyCurrentValue -= 10;
        //}
        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    health.MyCurrentValue += 10;
        //    mana.MyCurrentValue += 10;
        //}

        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}
