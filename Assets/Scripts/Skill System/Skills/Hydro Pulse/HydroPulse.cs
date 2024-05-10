using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Hydro Pulse")]
public class HydroPulse : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;
    [Header("Skill Range")]
    [SerializeField] public float radius;
    // [HideInInspector] public Transform ;

    public override float GetDamage(Character character)
    {
        return damage += dmgPersenOfATK * character.atk;
    }

    public override void Activate(GameObject gameObject)
    {
        //  = GameObject.Find("Player").transform;


    }

    public override void OnActivated(GameObject gameObject)
    {
    }

    // private void GetNearestEnemy()
    // {
    //     // skill.LockedEnemy = GameObject.Find("FlyingEnemy").GetComponent<FlyingEnemy>().children[0].transform;

    //     List<Collider2D> enemiesInRadius = Physics2D.OverlapCircleAll(
    //         player.transform.position,
    //         ((HydroPulse)skill).radius,
    //         LayerMask.GetMask("Enemy")
    //     ).ToList();

    //     Dictionary<Transform, float> distances = new Dictionary<Transform, float>();

    //     foreach (Collider2D enemy in enemiesInRadius)
    //     {
    //         MobController mob = enemy.GetComponent<MobController>();

    //         if (mob.enemy.type != EnemyType.Ground)
    //         {
    //             // flyingEnemyIndexes.Add(i);
    //             continue;
    //         }

    //         float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
    //         distances.Add(enemy.transform, distanceToEnemy);
    //     }

    //     if (distances.Count == 0)
    //     {
    //         lockedEnemies.Clear();
    //         return;
    //     }
    //     distances.OrderBy(dict => dict.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

    //     int index = 0;
    //     foreach (Transform key in distances.Keys)
    //     {
    //         if (index < 5)
    //         {
    //             lockedEnemies.Add(key);
    //         }
    //         index++;
    //     }

    // }



}