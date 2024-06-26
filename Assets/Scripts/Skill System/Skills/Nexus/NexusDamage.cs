using UnityEngine;

public class NexusDamage : MonoBehaviour
{

    Transform enemy;

    void Start()
    {
        enemy = GameObject.FindObjectOfType<NexusBehaviour>().skill.LockedEnemy;
        transform.position = enemy.position;
    }

    void Update()
    {
        if (enemy != null)
        {

            transform.position = enemy.position;
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
}
