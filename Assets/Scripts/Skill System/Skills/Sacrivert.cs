using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Sacrivert")]
public class Sacrivert : Skill
{
    [Header("Custom Cost")]
    [SerializeField] private float costPersenOfMaxHP;

    [Header("Buff Value")]
    [SerializeField] private float manaValue;

    [Header("Level Up Value")]
    [SerializeField] private float manaValueUp;

    public float manaValueFinal
    {
        get { return manaValue + manaValueUp * (level - 1); }
    }

    public override string GetDescription()
    {
        string additionMana = level > 1 ? " (+" + (manaValueFinal - manaValue) + ")" : "";

        description = "Menambah Mana dengan mengorbankan HP. HP akan berkurang sebanyak " + PersentaseToInt(costPersenOfMaxHP) + " dari Max HP, sebagai gantinya, menambah mana sebanyak " + manaValue + additionMana + ". Jika HP berada di bawah 10%, skill tidak dapat digunakan.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        GameObject player = GameObject.Find("Player");
        BuffSystem buffSystem = player.GetComponent<BuffSystem>();
        ItemSystem itemSystem = player.GetComponent<ItemSystem>();

        if (itemSystem.CheckItem("Badge of Honour"))
        {
            Destroy(gameObject);
            return;
        }

        float hp = buffSystem.GetComponent<PlayerController>().player.hp;
        this.cost = costPersenOfMaxHP * hp;

        if (hp >= this.cost)
        {
            Payment(buffSystem.transform);

            buffSystem.ActivateBuff(
                   new Buff(
                        this.id,
                        this.name,
                        BuffType.Mana,
                        manaValueFinal,
                        this.timer
                    )
                );

        }

    }
}
// public class Sacrivert : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;

//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;
//         // playerController = GameObject.Find("Player").GetComponent<PlayerController>();

//         player = GameObject.Find("Player");
//         BuffSystem buffSystem = player.GetComponent<BuffSystem>();

//         buffSystem.ActivateBuff(
//            new Buff(
//                 skill.Id,
//                 skill.Name,
//                 BuffType.Mana,
//                 skill.Value,
//                 skill.Timer
//             )
//         );
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }
// }