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

    public float FOCValueFinal
    {
        get { return FOCValue + 2 * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Meningkatkan FOC sebanyak " + FOCValueFinal + " selama " + timer + " detik.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        Payment(buffSystem.transform);
        buff = new Buff(
                id,
                name,
                BuffType.FOC,
                FOCValueFinal,
                timer
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