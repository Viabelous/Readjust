using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Dysnom")]
public class Dysnom : Enemy
{
    enum FlameTuskState
    {
        Delayed,
        SetTarget,
        Moving
    }

    [Header("Flam Tusk")]
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private float flameTuskDelay;
    [SerializeField] private float flameTuskDamage;
    [SerializeField] private float flameTuskRange;
    [SerializeField] private float flameTuskSpeed;

    // waktu untuk mengaktifkan flam tusk -----------
    private float timer, flameTuskTime;
    private FlameTuskState flameTuskState;
    private bool flameTuskActivate;

    // komponen yg digunakan ------------------------
    private MobController mobController;
    private Transform player;

    // gerakan flam tusk ----------------------------
    private Vector3 initialPos, targetPos, direction;
    private int flameTuskNumber;
    private float nowDistance, initialATK;

    public override void Spawning(GameObject gameObject)
    {
        mobController = gameObject.GetComponent<MobController>();
        player = GameObject.Find("Player").transform;
        timer = 0;

        flameTuskActivate = false;
        flameTuskNumber = 0;
        initialATK = this.atk;
        ResetflameTuskTime();

    }

    public override void OnAttacking(GameObject gameObject)
    {
        if (!flameTuskActivate)
        {
            mobController.animate.Play("dysnom_walk_frontw");

            timer += Time.deltaTime;

            if (timer >= flameTuskTime)
            {
                // Debug.Log("Siap nyeruduk kau");
                ResetflameTuskTime();
                timer = 0;

                flameTuskActivate = true;
                flameTuskState = FlameTuskState.Delayed;

                mobController.speed = mobController.enemy.GetSpeed() * 0.5f;
            }
        }
        else
        {
            switch (flameTuskState)
            {
                case FlameTuskState.Delayed:
                    ChangeAttackDamage(initialATK);

                    if (flameTuskNumber == 3)
                    {
                        mobController.animate.Play("dysnom_walk_frontw");
                        if (timer >= flameTuskDelay)
                        {
                            mobController.animate.Play("dysnom_idle");
                            mobController.speed = mobController.enemy.GetSpeed();
                            flameTuskActivate = false;
                            flameTuskNumber = 0;
                        }
                    }
                    else
                    {
                        mobController.animate.Play("dysnom_walk_frontw");
                        if (timer >= flameTuskDelay)
                        {
                            timer = 0;
                            flameTuskState = FlameTuskState.SetTarget;
                        }

                        // Debug.Log(timer);
                    }

                    timer += Time.deltaTime;

                    break;

                case FlameTuskState.SetTarget:

                    // Debug.Log("diem: " + this.atk);


                    OnPreFlameTusking(gameObject);
                    flameTuskState = FlameTuskState.Moving;

                    break;

                case FlameTuskState.Moving:
                    mobController.animate.Play("dysnom_flame_tusk_frontw");

                    ChangeAttackDamage(flameTuskDamage);
                    // Debug.Log("serudukk: " + this.atk);

                    nowDistance = Vector3.Distance(initialPos, gameObject.transform.position);

                    if (nowDistance >= flameTuskRange)
                    {
                        flameTuskState = FlameTuskState.Delayed;
                    }

                    OnFlameTusking(gameObject);
                    break;
            }
        }

    }



    private void ResetflameTuskTime()
    {
        flameTuskTime = UnityEngine.Random.Range(minTime, maxTime);
        // flameTuskTime = 5;
    }

    private void OnPreFlameTusking(GameObject gameObject)
    {
        initialPos = gameObject.transform.position;
        targetPos = new Vector3(player.position.x, player.position.y, player.position.z);
        direction = (targetPos - initialPos).normalized;

        flameTuskNumber++;
    }

    private void OnFlameTusking(GameObject gameObject)
    {
        float speedMultiplier = Mathf.Clamp(nowDistance / flameTuskRange, 1, 3);
        mobController.animate.SetFloat("FrameSpeed", speedMultiplier);
        gameObject.transform.Translate(direction * flameTuskSpeed * Time.deltaTime);
    }

    private void ChangeAttackDamage(float damage)
    {
        this.atk = damage;
    }

}