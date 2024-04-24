using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WillOfFire : Skill
{

    private GameObject player;
    [SerializeField]
    private float buffAtk;


    public override void Activate(GameObject gameObject)
    {

        player = GameObject.Find("Player");
        BuffSystem buffSystem = player.GetComponent<BuffSystem>();

        buffSystem.ActivateBuff(
           new Buff(
                BuffType.ATK,
                buffAtk,
                timer
            )
        );


    }


}
