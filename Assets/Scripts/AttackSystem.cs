using UnityEngine;

// dikasih ke skill
public class AttackSystem : MonoBehaviour
{

    public CharacterType type;
    // public GameObject characterObj;

    [HideInInspector]

    private float totalDamage, damage;
    // private bool isInstantiate = true;
    private Character attacker;
    private BuffSystem buffSystem;

    void Start()
    {
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();
    }

    public float DealDamage()
    {

        // ditaruh di sini karena dipakenya di skill bukan di player yg mana skill munculnya sbenetar ae
        // kalo ditaruh di update kadang error, 
        // mungkin malah bisa jadi deal damage dipanggil duluan dari pada update (?)

        SetAttacker();

        if (UnityEngine.Random.Range(0f, 100f) <= attacker.GetFOC() && attacker.GetFOC() != 0)
        {
            totalDamage = damage * 2.5f;
            // totalDamage = damage;
        }
        else
        {
            totalDamage = damage;
        }

        // kalau player pakai skill A Breeze Being Told, 
        // maka total damage akan bertambah sebanyak buff value persen dari total
        if (buffSystem.CheckBuff(BuffType.Breezewheel))
        {
            totalDamage += totalDamage * buffSystem.GetBuffValues(BuffType.Breezewheel);
        }

        totalDamage += totalDamage * DamageBooster();

        return totalDamage;
    }

    // peningkatan damage dari item
    private float DamageBooster()
    {
        if (type != CharacterType.Player)
        {
            return 0;
        }
        Skill skill = GetComponent<SkillController>().playerSkill;
        buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        float boosterDmg = 0;

        switch (GameManager.selectedMap)
        {
            case Map.Stage1:
                if (skill.Element == Element.Fire)
                {
                    boosterDmg += 0.08f;
                }
                break;
            case Map.Stage2:
                if (skill.Element == Element.Earth)
                {
                    boosterDmg += 0.08f;
                }
                break;
            case Map.Stage3:
                if (skill.Element == Element.Water)
                {
                    boosterDmg += 0.08f;
                }
                break;
            case Map.Stage4:
                if (skill.Element == Element.Air)
                {
                    boosterDmg += 0.08f;
                }
                break;
        }

        BuffType buffType = BuffType.Custom;
        switch (skill.Element)
        {
            case Element.Fire:
                buffType = BuffType.Fire;
                break;
            case Element.Earth:
                buffType = BuffType.Earth;
                break;
            case Element.Water:
                buffType = BuffType.Water;
                break;
            case Element.Air:
                buffType = BuffType.Air;
                break;

        }
        boosterDmg += buffSystem.GetAllBuffValues(buffType);
        return boosterDmg;
    }

    private void SetAttacker()
    {
        switch (type)
        {
            case CharacterType.Player:
                attacker = GameObject.FindWithTag("Player").GetComponent<PlayerController>().player;
                damage = GetComponent<SkillController>().playerSkill.GetDamage((Player)attacker);
                break;

            case CharacterType.Enemy:
                // attack system di musuh
                if (GetComponent<MobController>() != null)
                {
                    attacker = GetComponent<MobController>().enemy;
                    damage = attacker.GetATK();
                }

                break;

            case CharacterType.FlyingEnemy:
                if (transform.parent.GetComponent<MobController>() != null)
                {
                    // attack system di skill musuh terbang
                    attacker = transform.parent.GetComponent<MobController>().enemy;
                    damage = GetComponent<EnemySkillController>().GetDamage();

                }
                else
                {
                    // attack system di skill musuh darat (HEKA)
                    attacker = transform.parent.transform.parent.GetComponent<MobController>().enemy;
                    damage = GetComponent<EnemySkillController>().GetDamage();
                }
                break;
        }
    }
}