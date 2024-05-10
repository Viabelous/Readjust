using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;


public class Hydro : MonoBehaviour
{
    [HideInInspector] private Skill skill;
    // [HideInInspector] public int index;
    void Start()
    {
        skill = GetComponent<SkillController>().skill;
        // ambil skill dari parent nya
        // skill = transform.parent.GetComponent<SkillController>().skill.Clone();

        // ambil locked enemy dari parent yg sudah carikan musuh utk dilock
        // skill.LockedEnemy = ((HydroPulse)transform.parent.GetComponent<HydroPulseBehaviour>().skill).lockedEnemies[index];

        // ubah skill di skill controller dengan skill yg sudah diisi locked enemynya
        // agar bisa dipake sama component skill lainnya


        // if (skill.LockedEnemy == null)
        // {
        //     Destroy(gameObject);
        // }
    }

    void Update()
    {
        // kalau musuh sudah mati, tapi skill belum sampai ke musuh
        // if (skill.LockedEnemy.IsDestroyed())
        // {
        //     Destroy(gameObject);
        // }
    }

    void OnDestroy()
    {
        if (skill != null)
        {
            transform.parent.GetComponent<HydroPulseBehaviour>().KillLockedEnemy(skill.LockedEnemy);
        }
    }

}