using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Pebble Creation")]
public class PebbleCreation : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float dmgPersenOfDEF;
    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfDEFUp;
    public float dmgPersenOfDEFFinal
    {
        get { return dmgPersenOfDEF + dmgPersenOfDEFUp * (level - 1); }
    }

    public float dmgPersenOfDEFFinalPersen
    {
        get { return dmgPersenOfDEFFinal + 2.5f; }
    }

    public override string GetDescription()
    {
        string additionDmg = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfDEFFinal - dmgPersenOfDEF) + ") " : " ";

        description = "Menyerang musuh di hadapan menggunakan batuan kecil, mengakibatkan earth damage sebesar " + damage + " + " + PersentaseToInt(dmgPersenOfDEF) + "%" + additionDmg + "DEF pada satu musuh di hadapan.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return damage + dmgPersenOfDEFFinalPersen * player.GetDEF();
    }

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);
    }

}
