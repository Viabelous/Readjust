using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Hydro Pulse")]
public class HydroPulse : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;

    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfATKUp;

    [Header("Skill Range")]
    [SerializeField] public float radius;
    // [HideInInspector] public Transform ;

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

        description = "Menembakkan masing-masing satu peluru air menuju lima musuh terdekat yang akan mengakibatkan water damage sebesar " + damage + " + " + PersentaseToInt(dmgPersenOfATK) + "%" + additionATK + "ATK.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return damage + dmgPersenOfATKFinalPersen * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        //  = GameObject.Find("Player").transform;


    }

    public override void OnActivated(GameObject gameObject)
    {
    }



}