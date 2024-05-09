using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class HydroPulse : MonoBehaviour
{
    Skill skill;
    PlayerController playerController;
    Animator animator;

    [HideInInspector] public List<Transform> lockedEnemies = new List<Transform>();

    [SerializeField] private GameObject hydro;

    void Start()
    {
        // hydro.SetActive(false);


        skill = GetComponent<SkillController>().skill;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();

        GetNearestEnemy();

        if (lockedEnemies.Count == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            animator.Play("hydro_pulse_awake");
            StageManager.instance.PlayerActivatesSkill(skill);
        }
    }

    private void Update()
    {
        if (lockedEnemies.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    private void GetNearestEnemy()
    {
        // skill.LockedEnemy = GameObject.Find("FlyingEnemy").GetComponent<FlyingEnemy>().children[0].transform;

        List<Collider2D> enemiesInRadius = Physics2D.OverlapCircleAll(
            playerController.transform.position,
            skill.MovementRange,
            LayerMask.GetMask("Enemy")
        ).ToList();

        Dictionary<Transform, float> distances = new Dictionary<Transform, float>();
        // List<int> flyingEnemyIndexes = new List<int>();

        // for (int i = 0; i < enemiesInRadius.Count; i++)
        foreach (Collider2D enemy in enemiesInRadius)
        {
            MobController mob = enemy.GetComponent<MobController>();

            if (mob.enemy.type != EnemyType.Ground)
            {
                // flyingEnemyIndexes.Add(i);
                continue;
            }

            float distanceToEnemy = Vector3.Distance(playerController.transform.position, enemy.transform.position);
            distances.Add(enemy.transform, distanceToEnemy);
        }

        // foreach (int i in flyingEnemyIndexes)
        // {
        //     enemiesInRadius.RemoveAt(i);
        // }

        if (distances.Count == 0)
        {
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

    private void InstantiateHydros()
    {
        GetComponent<SkillMovement>().type = SkillMovementType.Area;

        for (int i = 0; i < lockedEnemies.Count; i++)
        {
            GameObject hydroPref = Instantiate(hydro, transform);
            hydroPref.GetComponent<Hydro>().index = i;

            // activatedHydros.Add(hydroPref);
        }
        // Destroy(hydro);
    }
}