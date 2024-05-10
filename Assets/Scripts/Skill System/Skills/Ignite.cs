using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Ignite")]
public class Ignite : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;

    public override float GetDamage(Character character)
    {
        return this.damage += dmgPersenOfATK * character.atk;
    }

    public override void Activate(GameObject gameObject)
    {
        StageManager.instance.PlayerActivatesSkill(this);
    }

}

// public class Ignite : MonoBehaviour
// {
//     private Skill skill;
//     [SerializeField] private float dmgPersenOfAtk;

//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;
//         skill.Damage += dmgPersenOfAtk * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;
//         // print("Damage + atk" + skill.Damage);
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }
// }