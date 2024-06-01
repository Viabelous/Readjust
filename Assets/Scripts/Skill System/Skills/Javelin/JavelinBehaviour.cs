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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FlyingEnemy"))
        {
            Destroy(gameObject, 0.3f);
        }
    }


}
