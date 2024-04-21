using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WillOfFire : Skill
{

    private GameObject player;


    public override void Activate(GameObject gameObject)
    {

        player = GameObject.Find("Player");

    }


}
