using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABreezeBeingToldDamage : MonoBehaviour
{

    private Transform enemy;

    void Start()
    {
        // enemy = GameObject.FindObjectOfType<NexusBehaviour>().skill.LockedEnemy;
        transform.position = enemy.position;
    }

    void Update()
    {
        if (enemy != null)
        {
            transform.position = enemy.position;
        }
        else
        {
            return;
        }
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    public void SetEnemy(Transform enemy)
    {
        this.enemy = enemy;
    }
}
