using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Heka")]
public class Heka : Enemy
{
    public enum HekaState
    {
        Idle,
        SummoningSwords
    }

    [Header("Skill Effect")]
    [SerializeField] GameObject sword;
    [SerializeField] List<Vector3> swordsOffset;
    [SerializeField] float swordSpeed;
    [SerializeField] float swordLifeTime;
    [SerializeField] float summonMaxTime;
    float timer;
    HekaState state;
    MobController mobController;

    public override void Spawning(GameObject gameObject)
    {
        timer = summonMaxTime;
        state = HekaState.Idle;
        mobController = gameObject.GetComponent<MobController>();
    }

    public override void OnAttacking(GameObject gameObject)
    {
        switch (state)
        {
            case HekaState.Idle:
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    StartSummoning();
                    timer = summonMaxTime;
                }
                break;
        }
    }

    public void StartSummoning()
    {
        this.state = HekaState.SummoningSwords;
        mobController.animate.SetTrigger("Swords Rain");
        mobController.movementEnabled = false;
    }

    public void EndSummoning()
    {
        this.state = HekaState.Idle;
        mobController.animate.SetTrigger("Swords Rain");
        // Debug.Log(mobController == null);
        mobController.movementEnabled = true;
    }

    public GameObject GetSword()
    {
        return sword;
    }


    public List<Vector3> GetOffsets()
    {
        return this.swordsOffset;
    }

    public float GetSwordSpeed()
    {
        return this.swordSpeed;
    }
    public float GetSwordLifeTime()
    {
        return this.swordLifeTime;
    }



}