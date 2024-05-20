using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Light Step")]
public class LightStep : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float AGIValue;
    private BuffSystem buffSystem;
    private Buff buff;

    public float AGIValueFinal
    {
        get { return AGIValue + 5 * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Meningkatkan AGI sebanyak " + AGIValueFinal + " selama " + timer + "detik.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        Payment(buffSystem.transform);
        buff = new Buff(
                id,
                name,
                BuffType.AGI,
                AGIValueFinal,
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