using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Wind Slash")]
public class WindSlash : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfAGI;
    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfAGIUp;

    public float dmgPersenOfAGIFinal
    {
        get { return dmgPersenOfAGI + dmgPersenOfAGIUp * (level - 1); }
    }


    public float dmgPersenOfAGIFinalPersen
    {
        get { return dmgPersenOfAGIFinal + 2.5f; }
    }


    public override string GetDescription()
    {
        string additionAGI = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfAGIFinal - dmgPersenOfAGI) + "%) " : " ";

        description = "Menyerang semua musuh di hadapan sepanjang garis lurus dengan damage sebesar " + PersentaseToInt(dmgPersenOfAGI) + "%" + additionAGI + " AGI. Dapat menyerang musuh yang terbang.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return this.damage + dmgPersenOfAGIFinalPersen * player.GetAGI();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }

}

// public class WindSlash : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;
//     private SpriteRenderer spriteRenderer;
//     [SerializeField] private float dmgPersenOfAgi;
//     private ChrDirection direction;
//     private Vector3 targetPos;
//     private float fadeDistance;

//     private void Start()
//     {
//         // sesuaikan damage basic attack dengan atk player
//         skill = GetComponent<SkillController>().skill;
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         player = GameObject.Find("Player");
//         PlayerController playerController = player.GetComponent<PlayerController>();

//         skill.Damage = playerController.player.agi;

//         fadeDistance = skill.MovementRange - 1;
//         direction = player.GetComponent<PlayerController>().direction;

//         switch (direction)
//         {
//             case ChrDirection.Left:
//                 targetPos = new Vector3(-skill.MovementRange, 0, 0);
//                 break;
//             case ChrDirection.Right:
//                 targetPos = new Vector3(skill.MovementRange, 0, 0);
//                 break;
//             case ChrDirection.Front:
//                 targetPos = new Vector3(0, -skill.MovementRange, 0);
//                 break;
//             case ChrDirection.Back:
//                 targetPos = new Vector3(0, skill.MovementRange, 0);
//                 break;
//         }

//         StageManager.instance.PlayerActivatesSkill(skill);
//     }

//     private void Update()
//     {

//         // float distance = Vector3.Distance(transform.position, targetPos);
//         // float newAlpha = Mathf.Lerp(0, 255, distance);

//         // // Atur alpha pada SpriteRenderer
//         // Color newColor = spriteRenderer.color;
//         // newColor.a = newAlpha;
//         // spriteRenderer.color = newColor;
//     }
// }
