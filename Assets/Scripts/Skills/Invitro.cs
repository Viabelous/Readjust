using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invitro : MonoBehaviour
{
    private Skill skill;
    private GameObject player;
    private BuffSystem buffSystem;
    [SerializeField] private float shieldPersenOfMaxHP;
    [SerializeField] private float shieldPersenOfDef;


    private void Start()
    {
        skill = GetComponent<SkillController>().skill;

        player = GameObject.Find("Player");
        buffSystem = player.GetComponent<BuffSystem>();

        PlayerController playerController = player.GetComponent<PlayerController>();
        float value = shieldPersenOfMaxHP * playerController.player.maxHp + shieldPersenOfDef * playerController.player.def;

        buffSystem.ActivateBuff(
           new Buff(
                skill.Id,
                BuffType.Shield,
                value,
                skill.Timer
            )
        );
    }

    // private void Update()
    // {
    //     if (buffSystem.buffsActive.FindIndex(buff => buff.id == skill.Id) == -1)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}