using UnityEngine;

public class NexusBehaviour : MonoBehaviour
{
    [HideInInspector] public Skill skill;


    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
    }

    // private void Update()
    // {

    // }


}