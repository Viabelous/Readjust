using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleneHealMob : MonoBehaviour
{

    Transform seleneTransform;
    Selene selene;
    float healTimer;

    void Start()
    {
        seleneTransform = GameObject.FindObjectOfType<SeleneBehaviour>().transform;
        selene = seleneTransform.GetComponent<SeleneBehaviour>().selene;
        healTimer = selene.GetHealMaxTime();
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
    }
}