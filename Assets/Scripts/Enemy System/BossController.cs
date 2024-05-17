using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Enemy boss;

    void Start()
    {
        boss.Spawning(gameObject);
    }

    void Update()
    {
        boss.OnAttacking(gameObject);
    }

    public void AnimationEvent()
    {
        boss.AnimationEvent();
    }
}