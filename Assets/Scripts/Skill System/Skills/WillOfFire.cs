using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Will Of Fire")]
public class WillOfFire : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float ATKValue;
    private BuffSystem buffSystem;
    private Buff buff;

    public float ATKValueFinal
    {
        get { return ATKValue + 5 * (level - 1); }
    }

    public override string GetDescription()
    {

        description = "Meningkatkan ATK sebanyak " + ATKValueFinal + " selama " + timer + " detik.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        buff = new Buff(
                this.id,
                this.name,
                BuffType.ATK,
                ATKValueFinal,
                this.timer
            );

        Payment(buffSystem.transform);
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