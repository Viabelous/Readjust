using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleneBehaviour : MonoBehaviour
{
    [HideInInspector] public Selene selene;
    [SerializeField] Transform summoner;

    // void Start()
    // {

    // }

    void Summoning()
    {
        selene = (Selene)GetComponent<MobController>().enemy;

        switch (selene.CurrentState())
        {
            case Selene.SeleneState.SummoningHeal:
                // float yOffset = 1.5f;
                // Vector3 offsetPos = summoner.position + new Vector3(0, yOffset, 0);
                Instantiate(
                    selene.GetHealMob(),
                    summoner.position,
                    Quaternion.identity
                );
                break;
            case Selene.SeleneState.SummoningAttack:
                for (int i = 0; i < selene.GetAttMobNum(); i++)
                {
                    float x = UnityEngine.Random.Range(-0.8f, 0.8f);
                    float y = UnityEngine.Random.Range(-0.8f, 0.8f);
                    Vector3 randomPos = summoner.position + new Vector3(x, y, 0);
                    Instantiate(
                        selene.GetAttMob(),
                        randomPos,
                        Quaternion.identity
                    );
                }
                break;
        }
    }

    void EndSummoning()
    {
        selene.EndSummoning();
    }
}