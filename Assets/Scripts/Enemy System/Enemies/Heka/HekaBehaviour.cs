using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HekaBehaviour : MonoBehaviour
{
    Heka heka;
    GameObject swordsObj;

    private void SummoningSwords()
    {
        heka = (Heka)GetComponent<MobController>().enemy;
        swordsObj = Instantiate(heka.GetSwords(), transform);
    }

    private void StartAttack()
    {
        swordsObj
        .GetComponent<HekaSwordsBehaviour>()
        .StartAttacking(
            gameObject,
            heka.GetSwordDamage(),
            heka.GetSwordSpeed(),
            heka.GetSwordLifeTime(),
            heka.GetSwordsDelay()
        );
    }

    private void EndSummoning()
    {
        heka.EndSummoning();
    }


}