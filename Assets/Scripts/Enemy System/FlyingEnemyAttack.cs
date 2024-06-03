using UnityEngine;


public class FlyingEnemyAttack : MonoBehaviour
{
    private float timer;
    EnemyProjectile projectile;

    void Start()
    {
        projectile = GetComponent<MobController>().enemy.GetProjectile()
                    .GetComponent<FlyingEnemyProjectile>().GetProjectile();
        timer = projectile.GetTimeInterval();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject obj = Instantiate(
                GetComponent<MobController>().enemy.GetProjectile(),
                transform
            );
            obj.GetComponent<EnemySkillController>().SetEnemy(gameObject);
            timer = projectile.GetTimeInterval();
        }
    }
}