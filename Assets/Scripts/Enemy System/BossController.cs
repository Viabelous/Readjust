using UnityEngine;

public class BossController : MonoBehaviour
{
    private Enemy boss;
    private bool isInstantiate = true;

    void Update()
    {
        if (isInstantiate)
        {
            boss = GetComponent<MobController>().enemy;
            boss.Spawning(gameObject);

            isInstantiate = false;
        }

        boss.OnAttacking(gameObject);
    }

    public void AnimationEvent()
    {
        boss.AnimationEvent();
    }

}