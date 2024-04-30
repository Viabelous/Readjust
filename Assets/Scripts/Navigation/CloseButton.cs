using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

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
        WindowsController.toogleWindow(windows_id, false);
        if(windows_id >= 2 && windows_id <= 5)
        {
            WindowsController.toogleWindow(1, true);
            WindowsController.CloseSkillTree();
        }
        
    }
    public override void ExclusiveKey()
    {
        
    }
}
