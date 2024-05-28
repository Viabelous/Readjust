using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Selene")]
public class Selene : Enemy
{

    public enum SeleneState
    {
        Idle,
        SummoningHeal,
        SummoningAttack
    }

    [Header("Summon Heal Mob")]
    [SerializeField] private float healSumMaxTime;
    [SerializeField] private GameObject healMob;
    [SerializeField] private float healMaxTime;
    [SerializeField] private float healValue;
    [SerializeField] private GameObject healEffect;

    [Header("Summon Attack Mob")]
    [SerializeField] private float attSumMaxTime;
    [SerializeField] private GameObject attMob;
    [SerializeField] private int attMobNum;
    private float healSumTimer, attSumTimer, healTimer;
    private MobController mobController;
    private SeleneState state;
    private bool healActivated;

    public override void Spawning(GameObject gameObject)
    {
        state = SeleneState.Idle;
        mobController = gameObject.GetComponent<MobController>();
        healSumTimer = healSumMaxTime;
        attSumTimer = attSumMaxTime;
        healTimer = healMaxTime;

        healActivated = false;
    }

    public override void OnAttacking(GameObject gameObject)
    {
        if (state == SeleneState.Idle)
        {
            healSumTimer -= Time.deltaTime;
            attSumTimer -= Time.deltaTime;
        }

        if (healActivated)
        {

            healTimer -= Time.deltaTime;

            if (healTimer <= 0)
            {
                Instantiate(healEffect, gameObject.transform);
                Heal(Stat.HP, healValue);
                healTimer = healMaxTime;
            }

            if (GameObject.FindObjectOfType<SeleneHealMob>() == null)
            {
                healActivated = false;
                healTimer = healMaxTime;
            }
        }
        else
        {
            if (GameObject.FindObjectOfType<SeleneHealMob>() != null)
            {
                healActivated = true;
            }
        }

        if (healSumTimer <= 0)
        {
            StartSummoning(SeleneState.SummoningHeal);
        }

        if (attSumTimer <= 0)
        {
            StartSummoning(SeleneState.SummoningAttack);
        }

        Debug.Log(this.hp);

    }

    public GameObject GetHealMob()
    {
        return healMob;
    }

    public GameObject GetAttMob()
    {
        return attMob;
    }

    public int GetAttMobNum()
    {
        return this.attMobNum;
    }

    public void StartSummoning(SeleneState state)
    {
        this.state = state;
        mobController.movementEnabled = false;
        switch (state)
        {
            case SeleneState.SummoningHeal:
                healSumTimer = healSumMaxTime;
                mobController.animate.Play("selene_summoning_front2");
                break;
            case SeleneState.SummoningAttack:
                attSumTimer = attSumMaxTime;
                mobController.animate.Play("selene_summoning_front");
                break;
        }
    }

    public void EndSummoning()
    {
        mobController.movementEnabled = true;
        mobController.animate.Play("selene_idle");
        this.state = SeleneState.Idle;
    }


    public SeleneState CurrentState()
    {
        return this.state;
    }

    public float GetHealMaxTime()
    {
        return this.healMaxTime;
    }

    public float GetHealValue()
    {
        return this.healValue;
    }
}