using UnityEngine;

public class ABreezeBeingToldDamage : MonoBehaviour
{

    private Transform enemy;

    void Start()
    {
        if (enemy.GetComponent<MobController>().enemy.type == EnemyType.Ground)
        {
            transform.position = enemy.position;
        }
        else
        {
            transform.position = enemy.GetComponent<FlyingEnemyShadow>().flyingEnemy.transform.position;
        }
    }

    void Update()
    {
        if (enemy != null)
        {
            if (enemy.GetComponent<MobController>().enemy.type == EnemyType.Ground)
            {
                transform.position = enemy.position;
            }
            else
            {
                transform.position = enemy.GetComponent<FlyingEnemyShadow>().flyingEnemy.transform.position;
            }
        }
        else
        {
            return;
        }
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    public void SetEnemy(Transform enemy)
    {
        this.enemy = enemy;
    }
}
