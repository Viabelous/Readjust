using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Hydro : MonoBehaviour
{
    [HideInInspector] public Skill skill;
    [HideInInspector] public int index;
    void Start()
    {
        // gameObject.SetActive(true);
        skill = transform.parent.GetComponent<SkillController>().skill.Clone();
        skill.LockedEnemy = transform.parent.GetComponent<HydroPulse>().lockedEnemies[index];
        GetComponent<SkillController>().skill = skill;

        if (skill.LockedEnemy == null)
        {
            Destroy(gameObject);
        }
        // print(skill.LockedEnemy.name);
    }

    void OnDestroy()
    {
        if (skill != null)
        {
            transform.parent.GetComponent<HydroPulse>().lockedEnemies.Remove(skill.LockedEnemy);
        }
    }


}