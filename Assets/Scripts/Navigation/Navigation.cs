using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public abstract class Navigation : MonoBehaviour
{
    public Image ImageComponent;
    public windowsController WindowsController;
    public Sprite BasicSprite;
    public Sprite HoverSprite;
    public GameObject Left;
    public GameObject Up;
    public GameObject Right;
    public GameObject Down;

    public void isHovered(bool state)
    {
        if(state)
        {
            GetComponent<Image>().sprite = HoverSprite;
        } else
        {
            GetComponent<Image>().sprite = BasicSprite;
        }
    }

    public abstract void Clicked();
    public abstract void ExclusiveKey();
}
