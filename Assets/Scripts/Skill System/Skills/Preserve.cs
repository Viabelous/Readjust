using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Preserve")]
public class Preserve : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float shieldPersenOfDEF;
    [Header("Level Up Value")]
    [SerializeField] private float shieldPersenOfDEFUp;
    public float shieldPersenOfDEFFinal
    {
        get { return shieldPersenOfDEF + shieldPersenOfDEFUp * (level - 1); }
    }
    public float shieldPersenOfDEFFinalPersen
    {
        get { return shieldPersenOfDEFFinal + 2.5f; }
    }

    public override string GetDescription()
    {
        string additionDEF = level > 1 ? " (+" + PersentaseToInt(shieldPersenOfDEFFinal - shieldPersenOfDEF) + "%) " : " ";
        description = "Membuat shield yang dapat menahan serangan sebesar " + PersentaseToInt(shieldPersenOfDEF) + "%" + additionDEF + "DEF saat skill digunakan, durasi shield tidak terbatas. Menggunakan skill ini akan me-refresh shield atau menggantikan shield.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);

        BuffSystem buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        PlayerController playerController = buffSystem.GetComponent<PlayerController>();
        float value = shieldPersenOfDEFFinalPersen * playerController.player.GetDEF();

        buffSystem.ActivateBuff(
               new Buff(
                    this.id,
                    this.name,
                    BuffType.Shield,
                    value,
                    this.timer
                )
            );
    }
}
