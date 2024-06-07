using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Ignite")]
public class Ignite : Skill
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
        description = "Melakukan serangan tebasan dengan area luas yang dapat mengakibatkan damage tinggi sebesar " + damage + " + " + PersentaseToInt(dmgPersenOfATK) + "%" + additionATK + "ATK ke musuh yang terkena serangan.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return this.damage + dmgPersenOfATKFinalPersen * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }

}