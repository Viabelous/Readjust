using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Pebble Creation")]
public class PebbleCreation : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float dmgPersenOfDEF;

    public override float GetDamage(Player player)
    {
        return damage += dmgPersenOfDEF * player.GetDEF();
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