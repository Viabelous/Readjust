using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Calm")]
public class Calm : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float FOCValue;
    [HideInInspector] public BuffSystem buffSystem;
    [HideInInspector] public Buff buff;

    public override void Activate(GameObject gameObject)
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        buff = new Buff(
                id,
                name,
                BuffType.FOC,
                FOCValue,
                timer
            );
        buffSystem.ActivateBuff(buff);
        StageManager.instance.PlayerActivatesSkill(this);
    }

    public override void OnActivated(GameObject gameObject)
    {
        if (!buffSystem.CheckBuff(buff))
        {
            Destroy(gameObject);
        }
    }



}