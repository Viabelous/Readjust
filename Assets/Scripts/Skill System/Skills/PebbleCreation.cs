using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Pebble Creation")]
public class PebbleCreation : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float dmgPersenOfDEF;

    public override float GetDamage(Character character)
    {
        return damage += dmgPersenOfDEF * character.def;
    }

    public override void Activate(GameObject gameObject)
    {
        StageManager.instance.PlayerActivatesSkill(this);
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