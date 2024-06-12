using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Light Step")]
public class LightStep : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float AGIValue;

    [Header("Level Up Value")]
    [SerializeField] private float AGIValueUp;
    private BuffSystem buffSystem;
    private Buff buff;

    public float AGIValueFinal
    {
        get { return AGIValue + AGIValueUp * (level - 1); }
    }

    public override string GetDescription()
    {
        string additionAGI = level > 1 ? " (+" + (AGIValueFinal - AGIValue) + ") " : " ";

        description = "Meningkatkan AGI sebanyak " + AGIValue + additionAGI + "selama " + timer + " detik.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
        Payment(buffSystem.transform);
        buff = new Buff(
                id,
                name,
                BuffType.AGI,
                AGIValueFinal,
                Timer
            );
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