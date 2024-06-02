using UnityEngine;


public class Hydro : MonoBehaviour
{
    [HideInInspector] private Skill skill;
    // [HideInInspector] public int index;
    void Start()
    {
        skill = GetComponent<SkillController>().skill;
    }

    void OnDestroy()
    {
        if (skill != null)
        {
            transform.parent.GetComponent<HydroPulseBehaviour>().KillLockedEnemy(skill.LockedEnemy);
        }
    }

}