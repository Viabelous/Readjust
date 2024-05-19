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

    public float DEFValueFinal
    {
        get { return DEFValue + 5 * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Meningkatkan DEF sebanyak " + DEFValueFinal + " untuk selama " + timer + "detik.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        Payment(buffSystem.transform);
        buff = new Buff(
                id,
                name,
                BuffType.DEF,
                DEFValueFinal,
                Timer
            );
        buffSystem.ActivateBuff(buff);
    }

    public override void OnActivated(GameObject gameObject)
    {
        if (!buffSystem.CheckBuff(buff))
        {
            Destroy(gameObject);
        }
    }
}