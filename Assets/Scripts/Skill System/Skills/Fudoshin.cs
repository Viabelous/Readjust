using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Fudoshin")]
public class Fudoshin : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float DEFValue;
    private BuffSystem buffSystem;
    private Buff buff;

    public override void Activate(GameObject gameObject)
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        buff = new Buff(
                id,
                name,
                BuffType.DEF,
                DEFValue,
                Timer
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