using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Damage,
    Buff
}

public class SkillBase : MonoBehaviour
{
    private float damage, cd, manaUsage;
    private SkillType type;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (type == SkillType.Damage)
        {

        }
    }




}
