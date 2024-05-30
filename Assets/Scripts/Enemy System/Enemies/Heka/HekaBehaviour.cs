using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HekaBehaviour : MonoBehaviour
{
    Heka heka;
    List<GameObject> swords = new List<GameObject>();

    private void SummoningSwords()
    {
        heka = (Heka)GetComponent<MobController>().enemy;

        for (int i = 0; i < heka.GetOffsets().Count; i++)
        {
            GameObject sword = Instantiate(
                    heka.GetSword(),
                    transform.position + heka.GetOffsets()[i],
                    Quaternion.identity
                );
            swords.Add(sword);
        }
    }

    private void EndSummoning()
    {
        heka.EndSummoning();
    }

    private void StartAttack()
    {
        for (int i = 0; i < heka.GetOffsets().Count; i++)
        {
            StartCoroutine(Attacking(swords[i], i + 0.5f));
        }
    }

    IEnumerator Attacking(GameObject sword, float sec)
    {
        yield return new WaitForSeconds(sec);
        sword.GetComponent<HekaSwordBehaviour>().AttackNow(heka.GetSwordSpeed(), heka.GetSwordLifeTime());
    }
}