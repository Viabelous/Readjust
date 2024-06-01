using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class HydroPulseBehaviour : MonoBehaviour
{
    private Skill skill;
    private Transform player;
    [SerializeField] private GameObject hydro;
    [HideInInspector] private List<Transform> lockedEnemies = new List<Transform>();
    [HideInInspector] Animator animator;
    // Vector3 finalPos;


    void Start()
    {
        skill = GetComponent<SkillController>().skill;
        player = GameObject.Find("Player").transform;

        animator = gameObject.GetComponent<Animator>();

        GetNearestEnemy();

        if (lockedEnemies.Count == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            animator.Play("hydro_pulse_awake");
            skill.Payment(player);
        }

    }

    void Update()
    {
        // transform.position = player.position;
        if (AllEnemiesKilled())
        {
            Destroy(gameObject);
        }
    }

    private void InstantiateHydros()
    {
        // finalPos = transform.position;
        GetComponent<SkillMovement>().type = SkillMovementType.Area;

        for (int i = 0; i < lockedEnemies.Count; i++)
        {
            GameObject hydroPref = Instantiate(hydro, transform);
            SkillController hydroController = hydroPref.GetComponent<SkillController>();

            // hydroController.skill = skill.Clone();
            hydroController.skill.LockedEnemy = lockedEnemies[i];
        }

        Destroy(hydro);
    }

    private void GetNearestEnemy()
    {
        // skill.LockedEnemy = GameObject.Find("FlyingEnemy").GetComponent<FlyingEnemy>().children[0].transform;

        List<Collider2D> enemiesInRadius = Physics2D.OverlapCircleAll(
            player.transform.position,
            ((HydroPulse)skill).radius,
            LayerMask.GetMask("Enemy")
        ).ToList();

        Dictionary<Transform, float> distances = new Dictionary<Transform, float>();

        foreach (Collider2D enemy in enemiesInRadius)
        {
            MobController mob = enemy.GetComponent<MobController>();

            if (mob.enemy.type != EnemyType.Ground)
            {
                // flyingEnemyIndexes.Add(i);
                continue;
            }

            float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
            distances.Add(enemy.transform, distanceToEnemy);
        }

        if (distances.Count == 0)
        {
            // lockedEnemies.Clear();
            return;
        }

        distances.OrderBy(dict => dict.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

        int index = 0;
        foreach (Transform key in distances.Keys)
        {
            if (index < 5)
            {
                lockedEnemies.Add(key);
            }
            index++;
        }

    }

    public bool AllEnemiesKilled()
    {
        int killedEnemy = 0;
        for (int i = 0; i < lockedEnemies.Count; i++)
        {
            if (lockedEnemies[i] == null)
            {
                killedEnemy += 1;
            }
        }

        return lockedEnemies.Count == 0 || killedEnemy == lockedEnemies.Count;

    }

    public void KillLockedEnemy(Transform enemy)
    {
        lockedEnemies.Remove(enemy);
    }


}