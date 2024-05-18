using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Basic Stab")]
public class BasicStab : Skill
{
    public override float GetDamage(Player player)
    {
        return player.GetATK();
    }
}