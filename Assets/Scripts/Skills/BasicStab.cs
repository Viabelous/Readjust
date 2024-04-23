using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasicStab : Skill
{

    private GameObject player;

    public override void Activate(GameObject gameObject)
    {

        player = GameObject.FindWithTag("Player");
        this.damage = player.GetComponent<PlayerController>().player.atk;


    }


}