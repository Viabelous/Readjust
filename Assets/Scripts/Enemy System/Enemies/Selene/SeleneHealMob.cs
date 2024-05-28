using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleneHealMob : MonoBehaviour
{

    Selene selene;
    float healTimer;

    void Start()
    {
        selene = GameObject.FindObjectOfType<SeleneBehaviour>().selene;
        healTimer = selene.GetHealMaxTime();
    }

    void Update()
    {

    }
}