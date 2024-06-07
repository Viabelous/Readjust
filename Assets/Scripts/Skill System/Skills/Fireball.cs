using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Fireball")]
public class Fireball : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;
    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfATKUp;
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
        string additionATK = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfATKFinal - dmgPersenOfATK) + "%) " : " ";
        description = "Menembakkan bola api ke hadapan karakter yang akan memberikan fire damage dengan damage sebesar " + damage + " + " + PersentaseToInt(dmgPersenOfATK) + "%" + additionATK + "ATK pada satu musuh yang terkena.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return damage + dmgPersenOfATKFinalPersen * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }
}