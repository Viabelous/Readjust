using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Lenire")]
public class Lenire : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float manaValue;
    [SerializeField] private float manaPersenOfFOC;
    [Header("Level Up Value")]
    [SerializeField] private float manaPersenOfFOCUp;

    public float manaPersenOfFOCFinal
    {
        get { return manaPersenOfFOC + manaPersenOfFOCUp * (level - 1); }
    }
    public float manaPersenOfFOCFinalPersen
    {
        get { return manaPersenOfFOCFinal + 2.5f; }
    }

    public override string GetDescription()
    {
        string additionMana = level > 1 ? " (+" + PersentaseToInt(manaPersenOfFOCFinal - manaPersenOfFOC) + "%) " : " ";

        description = "Meningkatkan Mana sebanyak " + manaValue + " + " + PersentaseToInt(manaPersenOfFOC) + "%" + additionMana + "FOC.";
        return description;
    }


    public override void Activate(GameObject gameObject)
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Payment(playerController.transform);

        BuffSystem buffSystem = playerController.GetComponent<BuffSystem>();
        float value = manaValue + manaPersenOfFOCFinalPersen * playerController.player.GetFOC();

        buffSystem.ActivateBuff(
           new Buff(
                this.id,
                this.name,
                BuffType.Mana,
                value,
                this.timer
            )
        );
    }
}
