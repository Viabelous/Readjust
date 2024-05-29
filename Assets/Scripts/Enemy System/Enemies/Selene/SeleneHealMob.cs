using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleneHealMob : MonoBehaviour
{

    Transform seleneTransform;
    Selene selene;
    float healTimer;
    Vector2 minRange, maxRange;
    Vector3 initPos, targetPos;
    MobController mobController;
    float initDistance, nowDistance;

    void Start()
    {
        seleneTransform = GameObject.FindObjectOfType<SeleneBehaviour>().transform;
        selene = seleneTransform.GetComponent<SeleneBehaviour>().selene;
        healTimer = selene.GetHealMaxTime();

        minRange = StageManager.instance.minMap;
        maxRange = StageManager.instance.maxMap;

        initPos = transform.position;

        mobController = GetComponent<MobController>();
        mobController.SetRandomMovement(true);
        UpdateTargetPos();
    }

    void Update()
    {
        healTimer -= Time.deltaTime;

        if (healTimer <= 0)
        {
            Instantiate(selene.GetHealEffect(), seleneTransform);
            selene.Heal(Stat.HP, selene.GetHealValue());
            healTimer = selene.GetHealMaxTime();
        }

        nowDistance = Vector3.Distance(initPos, transform.position);

        if (nowDistance > initDistance)
        {
            print("ganti posisi");
            UpdateTargetPos();
        }
    }

    void UpdateTargetPos()
    {
        float x, y, z;
        x = UnityEngine.Random.Range(minRange.x, maxRange.y);
        y = UnityEngine.Random.Range(minRange.y, maxRange.y);
        z = 0;

        initPos = transform.position;
        targetPos = new Vector3(x, y, z);

        initDistance = Vector3.Distance(initPos, targetPos);

        mobController.SetTargetPos(targetPos);
    }
}