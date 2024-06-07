using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stalactite Shoot")]
public class StalactiteShoot : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfDEF;
    [SerializeField] private float dmgPersenOfATK;

    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfDEFUp;
    [SerializeField] private float dmgPersenOfATKUp;

    public float dmgPersenOfDEFFinal
    {
        get { return dmgPersenOfDEF + dmgPersenOfDEFUp * (level - 1); }
    }

    public float dmgPersenOfDEFFinalPersen
    {
        get { return dmgPersenOfDEFFinal + 2.5f; }
    }

    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + dmgPersenOfATKUp * (level - 1); }
    }

    public float dmgPersenOfATKFinalPersen
    {
        get { return dmgPersenOfATKFinal + 1; }
    }

    public override string GetDescription()
    {
        string additionDEF = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfDEFFinal - dmgPersenOfDEF) + "%) " : " ";
        string additionATK = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfATKFinal - dmgPersenOfATK) + "%) " : " ";

        description = "Menembakkan tanah buatan runcing ke arah depan dan belakang yang akan mengakibatkan earth damage sebesar " + damage + " + " + PersentaseToInt(dmgPersenOfDEF) + "%" + additionDEF + "DEF + " + PersentaseToInt(dmgPersenOfATK) + "%" + additionATK + "ATK pada musuh yang terkena serangan.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return damage += dmgPersenOfDEFFinalPersen * player.GetDEF() + dmgPersenOfATKFinalPersen * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }
}