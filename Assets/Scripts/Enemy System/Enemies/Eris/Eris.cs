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
    [SerializeField] private float erisProjectileDmg;
    [SerializeField] private float erisProjectileSpeed;
    [SerializeField] private float erisProjectileTimer;


    [Header("Xena")]
    [SerializeField] private GameObject xenaProjectile;
    [SerializeField] private float xenaProjectileDmg;
    [SerializeField] private float xenaProjectileSpeed;
    [SerializeField] private float xenaProjectileTimer;

    MobController mobController;

    ErisState state;

    private float projectileTimer;

    public override void Spawning(GameObject gameObject)
    {
        projectileTimer = erisProjectileTimer;

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
                    projectileTimer = erisProjectileTimer;
                    AttackEris(gameObject);
                }
                break;

            case ErisState.Xena:
                if (projectileTimer <= 0)
                {
                    projectileTimer = xenaProjectileTimer;
                    AttackXena(gameObject);
                }
                break;
        }
    }

    public void AttackEris(GameObject gameObject)
    {
        // Vector3 startPos = gameObject.GetComponent<FlyingEnemyShadow>().children[0].transform.position;
        GameObject projectile = Instantiate(erisProjectile);
        projectile.GetComponent<EnemySkillController>().SetEnemy(gameObject);
        projectile.GetComponent<EnemySkillController>().SetDamage(erisProjectileDmg);
        projectile.GetComponent<EnemySkillController>().SetSpeed(erisProjectileSpeed);
    }

    public void AttackXena(GameObject gameObject)
    {
        // Vector3 startPos = gameObject.GetComponent<FlyingEnemyShadow>().children[0].transform.position;
        GameObject projectile = Instantiate(xenaProjectile);
        projectile.GetComponent<EnemySkillController>().SetEnemy(gameObject);
        projectile.GetComponent<EnemySkillController>().SetDamage(xenaProjectileDmg);
        projectile.GetComponent<EnemySkillController>().SetSpeed(xenaProjectileSpeed);
    }

    public GameObject GetErisProjectile()
    {
        return erisProjectile;
    }
    public float GetErisProjectileDmg()
    {
        return erisProjectileDmg;
    }
    public float GetErisProjectileSpeed()
    {
        return erisProjectileSpeed;
    }

    public GameObject GetXenaProjectile()
    {
        return xenaProjectile;
    }
    public float GetXenaProjectileDmg()
    {
        return xenaProjectileDmg;
    }

    public float GetXenaProjectileSpeed()
    {
        return xenaProjectileSpeed;
    }
}