using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABreezeBeingToldBehaviour : MonoBehaviour
{
    private Skill skill;
    [SerializeField] private Transform windwheel;
    Player player;
    private float maxTimer = 1, timer = 0;

    void Start()
    {
        skill = GetComponent<SkillController>().skill;
        player = GameObject.Find("Player").GetComponent<PlayerController>().player;
    }

    void Update()
    {
        windwheel.transform.Rotate(0, 0, -10);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            if (timer <= 0)
            {
                timer = maxTimer;
                float dealDamage = other.GetComponent<AttackSystem>().DealDamage();
                float finalDamage = ((ABreezeBeingTold)skill).dmgPersenOfTotalDmgFinal * dealDamage;

                MobController[] mobs = FindObjectsOfType<MobController>();
                foreach (MobController mob in mobs)
                {
                    mob.Effected("breezewheel");
                    mob.GetComponent<DefenseSystem>().TakeDamage(finalDamage);
                }
                player.Heal(Stat.HP, ((ABreezeBeingTold)skill).HPPersenOfAGI);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            timer = 0;
        }

    }
}