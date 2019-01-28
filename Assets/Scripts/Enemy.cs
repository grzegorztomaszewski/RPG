using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    private Transform target;

    public Transform Target { get => target; set => target = value; }

    public override Transform Select()
    {
        healthGroup.alpha = 1;

        return base.Select();
    }

    public override void DeSelect() //wygasza pasek życia enemy
    {
        healthGroup.alpha = 0;

        base.DeSelect();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        OnHealthChanged(health.MyCurrentValue);
    }
}
