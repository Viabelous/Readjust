using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Pebble Creation")]
public class PebbleCreation : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float dmgPersenOfDEF;
    public float dmgPersenOfDEFFinal
    {
        get { return dmgPersenOfDEF + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Menyerang musuh di hadapan menggunakan batuan kecil, mengakibatkan earth damage sebesar 60 + " + dmgPersenOfDEFFinal * 100 + "% DEF pada satu musuh di hadapan.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return damage + dmgPersenOfDEFFinal * player.GetDEF();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }

}
// public class PebbleCreation : MonoBehaviour
// {
//     private Skill skill;
//     [SerializeField] private float dmgPersenOfDef;
//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;
//         skill.Damage += dmgPersenOfDef * GameObject.Find("Player").GetComponent<PlayerController>().player.def;
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }


// }