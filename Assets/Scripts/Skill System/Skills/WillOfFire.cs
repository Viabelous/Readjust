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

    public override void Activate(GameObject gameObject)
    {

        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        buff = new Buff(
                this.id,
                this.name,
                BuffType.ATK,
                ATKValue,
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
// public class WillOfFire : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;
//     private BuffSystem buffSystem;
//     private Buff buff;

//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;

//         player = GameObject.Find("Player");
//         buffSystem = player.GetComponent<BuffSystem>();
//         buff = new Buff(
//                 skill.Id,
//                 skill.Name,
//                 BuffType.ATK,
//                 skill.Value,
//                 skill.Timer
//             );

//         buffSystem.ActivateBuff(buff);
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }

//     private void Update()
//     {
//         if (!buffSystem.CheckBuff(buff))
//         {
//             Destroy(gameObject);
//         }
//     }
// }