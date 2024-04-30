using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class CloseButton : Navigation
{
    public int windows_id;
    
    void Start()
    {
        Down = WindowsController.WindowsButtonStartPointNavigation[windows_id];
    }

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
    }
    public override void ExclusiveKey()
    {
        
    }
}
