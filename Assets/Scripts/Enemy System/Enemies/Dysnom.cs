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
    private float distance, nowDistance;

    public override void Spawning(GameObject gameObject)
    {
        mobController = gameObject.GetComponent<MobController>();
        player = GameObject.Find("Player").transform;
        timer = 0;

        flameTuskActivate = false;
        flameTuskNumber = 0;
        ResetflameTuskTime();

    }

    public override void OnAttacking(GameObject gameObject)
    {
        if (!flameTuskActivate)
        {
            mobController.animate.Play("dysnom_idle");

            timer += Time.deltaTime;

            if (timer >= flameTuskTime)
            {
                // Debug.Log("Siap nyeruduk kau");
                ResetflameTuskTime();
                timer = 0;

                flameTuskActivate = true;
                flameTuskState = FlameTuskState.Delayed;

                mobController.speed = mobController.enemy.speed / 2;
            }
        }
        else
        {
            switch (flameTuskState)
            {
                case FlameTuskState.Delayed:
                    // mobController.animate.Play("dysnom_idle");

                    if (flameTuskNumber == 3)
                    {
                        if (timer >= flameTuskDelay)
                        {
                            mobController.speed = mobController.enemy.speed;
                            flameTuskActivate = false;
                            flameTuskNumber = 0;

                        }
                    }
                    else
                    {
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
                    mobController.animate.Play("dysnom_idle");

                    OnPreFlameTusking(gameObject);
                    flameTuskState = FlameTuskState.Moving;

                    break;

                case FlameTuskState.Moving:
                    mobController.animate.Play("dysnom_flame_tusk_frontw");

                    nowDistance = Vector3.Distance(initialPos, gameObject.transform.position);

                    if (nowDistance >= distance)
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
        // flameTuskTime = UnityEngine.Random.Range(minTime, maxTime);
        flameTuskTime = 5;
    }

    private void OnPreFlameTusking(GameObject gameObject)
    {
        initialPos = gameObject.transform.position;
        targetPos = new Vector3(player.position.x, player.position.y, player.position.z);
        direction = (targetPos - initialPos).normalized;
        distance = Vector3.Distance(initialPos, targetPos);

        flameTuskNumber++;
    }

    private void OnFlameTusking(GameObject gameObject)
    {
        float speedMultiplier = Mathf.Clamp(nowDistance / distance, 0.5f, 3);
        mobController.animate.SetFloat("FrameSpeed", speedMultiplier);
        gameObject.transform.Translate(direction * flameTuskSpeed * Time.deltaTime);
    }



    // private IEnumerator flameTuskCoroutine(GameObject gameObject)
    // {
    //     PreflameTusking(gameObject);
    //     yield return new WaitForSeconds(1f);
    //     flameTusking(gameObject);
    // }

}