using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Eris")]
public class Eris : Enemy
{
    public enum ErisState
    {
        Eris,
        Xena
    }

    [Header("Transform From Eris To Xena")]
    [SerializeField] private float HPPersenOfMaxHP;

    [Header("Eris")]
    [SerializeField] private GameObject erisProjectile;


    [Header("Xena")]
    [SerializeField] private GameObject xenaProjectile;

    MobController mobController;

    ErisState state;

    private float projectileTimer;

    public override void Spawning(GameObject gameObject)
    {
        projectileTimer = erisProjectile.GetComponent<FlyingEnemyProjectile>().GetProjectile().GetTimeInterval();

        state = ErisState.Eris;
        mobController = gameObject.GetComponent<MobController>();
    }

    public override void OnAttacking(GameObject gameObject)
    {
        projectileTimer -= Time.deltaTime;

        if (this.hp < HPPersenOfMaxHP * maxHp)
        {
            state = ErisState.Xena;
            mobController.animate.SetTrigger("Unseal");
        }

        switch (state)
        {
            case ErisState.Eris:
                if (projectileTimer <= 0)
                {
                    projectileTimer = erisProjectile.GetComponent<FlyingEnemyProjectile>()
                                    .GetProjectile().GetTimeInterval();
                    AttackEris(gameObject);
                }
                break;

            case ErisState.Xena:
                if (projectileTimer <= 0)
                {
                    projectileTimer = xenaProjectile.GetComponent<FlyingEnemyProjectile>()
                                    .GetProjectile().GetTimeInterval();
                    AttackXena(gameObject);
                }
                break;
        }
    }

    public void AttackEris(GameObject gameObject)
    {
        GameObject projectile = Instantiate(erisProjectile, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<EnemySkillController>().SetEnemy(gameObject);

    }

    public void AttackXena(GameObject gameObject)
    {
        GameObject projectile = Instantiate(xenaProjectile, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<EnemySkillController>().SetEnemy(gameObject);

    }

}