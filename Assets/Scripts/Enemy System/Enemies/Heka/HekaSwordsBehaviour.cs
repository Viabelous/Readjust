using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HekaSwordsBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> swords = new List<GameObject>();

    void Update()
    {
        if (swords.All(all => all == null))
        {
            Destroy(gameObject);
        }
    }

    public void StartAttacking(GameObject heka, float damage, float speed, float lifetime, float delay)
    {
        for (int i = 0; i < swords.Count; i++)
        {
            swords[i].GetComponent<HekaSwordBehaviour>()
            .AttackNow(heka, damage, speed, lifetime, i * delay);
        }
    }

}