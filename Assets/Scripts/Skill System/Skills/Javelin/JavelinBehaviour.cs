using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class JavelinBehaviour : MonoBehaviour
{
    private Skill skill;
    private Transform player;

    void Start()
    {
        skill = GetComponent<SkillController>().skill;
        player = GameObject.Find("Player").transform;

        // GetNearestEnemy();

    }


}
