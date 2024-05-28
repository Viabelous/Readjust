using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABreezeBeingToldHeal : MonoBehaviour
{

    Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        // enemy = GameObject.FindObjectOfType<NexusBehaviour>().skill.LockedEnemy;
        transform.position = player.position + new Vector3(0, -0.29f, 0);
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + new Vector3(0, -0.29f, 0);
        }
        else
        {
            return;
        }
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

}
