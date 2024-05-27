using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System;

public class CloseButton : Navigation
{
    public int windows_id;

    public override void IsHovered(bool state)
    {
        if(state)
        {
            GetComponent<Image>().sprite = HoverSprite;
        } else
        {
            GetComponent<Image>().sprite = BasicSprite;
        }
    }
    public override void Clicked()
    {
        
        if(windows_id >= 2 && windows_id <= 5)
        {
            StartCoroutine(WindowsController.TransitionWindows(2, 1));
            WindowsController.CloseSkillTree();
        }
        
        else
        {
            StartCoroutine(WindowsController.ToogleWindow(windows_id, false));
        }
        
    }
    public override void ExclusiveKey()
    {
        
    }
}
