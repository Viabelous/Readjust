using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class SkillsSelection : Navigation
{
    public Skill skills;

    public override void IsHovered(bool state)
    {
        if(state)
        {
            Color current = ImageComponent.color;
            current.r = 1f;
            current.g = 1f;
            current.b = 1f;
            ImageComponent.color = current;
        } else
        {
            Color current = ImageComponent.color;
            current.r = 0.5f;
            current.g = 0.5f;
            current.b = 0.5f;
            ImageComponent.color = current;
        }
    }
    public override void Clicked()
    {
    }
    public override void ExclusiveKey()
    {
        
    }
}
