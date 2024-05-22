using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Invitro")]
public class Invitro : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float shieldPersenOfMaxHP;
    [SerializeField] private float shieldPersenOfDEF;
    [SerializeField] public float hpPersenOfDmg;

    [Header("Level Up Value")]
    [SerializeField] private float shieldPersenOfDEFUp;
    private PlayerController playerController;
    private float shield;
    private GameObject gameObject;


    private BuffSystem buffSystem;

    public float shieldPersenOfDEFFinal
    {
        get { return shieldPersenOfDEF + shieldPersenOfDEFUp * (level - 1); }
    }

    public override string GetDescription()
    {
        string additionDEF = level > 1 ? " (+" + PersentaseToInt(shieldPersenOfDEFFinal - shieldPersenOfDEF) + "%) " : " ";

        description = "Membuat shield yang dapat menahan serangan sebesar 20% Max HP + " + PersentaseToInt(shieldPersenOfDEF) + "%" + additionDEF + "DEF saat skill digunakan, durasi shield tidak terbatas. Memberikan status {Gaia's Protection} pada karakter yang akan mengisi HP karakter sebanyak 30% dari damage yang diterima. Status tersebut akan terus aktif selama shield dari skill {Invitro} masih ada. Efek tidak dapat ditumpuk dan akan hilang ketika shield di-refresh. Menggunakan skill ini akan me-refresh shield dan status.";
        return description;
    }
    public override void Activate(GameObject gameObject)
    {
        this.gameObject = gameObject;

        GameObject player = GameObject.Find("Player");
        Payment(player.transform);

        buffSystem = player.GetComponent<BuffSystem>();

        playerController = player.GetComponent<PlayerController>();
        shield = shieldPersenOfMaxHP * playerController.player.GetMaxHP() + shieldPersenOfDEFFinal * playerController.player.GetDEF();
        buffSystem.ActivateBuff(
           new Buff(
                this.id,
                this.name,
                BuffType.Shield,
                shield,
                this.timer
            )
        );
    }

}

// public class Invitro : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;
//     private BuffSystem buffSystem;
//     [SerializeField] private float shieldPersenOfMaxHP;
//     [SerializeField] private float shieldPersenOfDef;


//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;

//         player = GameObject.Find("Player");
//         buffSystem = player.GetComponent<BuffSystem>();

//         PlayerController playerController = player.GetComponent<PlayerController>();
//         float value = shieldPersenOfMaxHP * playerController.player.maxHp + shieldPersenOfDef * playerController.player.def;

//         buffSystem.ActivateBuff(
//            new Buff(
//                 skill.Id,
//                 skill.Name,
//                 BuffType.Shield,
//                 value,
//                 skill.Timer
//             )
//         );
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }

//     // private void Update()
//     // {
//     //     if (buffSystem.buffsActive.FindIndex(buff => buff.id == skill.Id) == -1)
//     //     {
//     //         Destroy(gameObject);
//     //     }
//     // }
// }