using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStab : MonoBehaviour
{
    private Skill skill;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        skill.Damage = GetComponent<PlayerController>().player.atk;
    }
}

// [CreateAssetMenu]
// public class BasicStab : Skill
// {

//     private GameObject player;

//     public override void Activate(GameObject gameObject)
//     {

//         player = GameObject.FindWithTag("Player");
//         this.damage = player.GetComponent<PlayerController>().player.atk;


//     }


// }
